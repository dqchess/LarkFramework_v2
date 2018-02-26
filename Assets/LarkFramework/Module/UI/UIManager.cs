using LarkFramework.Module;
using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.UI
{
    public class UIManager : ServiceModule<UIManager>, IUIManager
    {
        public const string LOG_TAG = "[UIManager]";
        public string MainPage = string.Empty;

        class UIPageTrack
        {
            public string name;                                 //页面名
            public UIPanel uiPanel;                             //页面实体
        }

        private Stack<UIPageTrack> m_pageTrackStack;            //页面堆栈
        private UIPageTrack m_currentPage;                      //当前页面
        private List<UIPanel> m_listLoadedPanel;                //所有加载过的页面

        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="uiResRoot">UI资源的根目录，默认为"UI/"</param>
        public void Init(string uiResRoot = "UI/")
        {
            CheckSingleton();
            UIRes.UIResRoot = uiResRoot;

            m_pageTrackStack = new Stack<UIPageTrack>();
            m_listLoadedPanel = new List<UIPanel>();

            SingletonMono<UIComponent>.Create();
        }

        #region Open
        private void OpenPageWorker(string form, object arg)
        {
            //Debuger.Log(LOG_TAG, "OpenPageWorker() scene:,page{1},arg:{2}",page, arg);

            m_currentPage = new UIPageTrack();
            m_currentPage.name = form;

            //关闭当前Page时打开的所有UI
            CloseAllLoadedPanels();

            Open<UIPanel>(form, arg);
        }

        public T Open<T>(string name, object arg = null) where T : UIPanel
        {
            T ui = Load<T>(name);
            if (ui != null)
            {
                ui.Open(arg);
                m_currentPage.uiPanel = ui;
            }
            else
            {
                this.LogError(LOG_TAG+"Open() Failed! Name:" + name);
            }
            return ui;
        }

        /// <summary>
        /// 加载UI，如果UIComponent下已经存在，则直接取UIComponent下的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private T Load<T>(string name) where T : UIPanel
        {
            //TODO:此处有坑，隐藏的对象不能别Find到，需要从堆栈中取
            T ui = SingletonMono<UIComponent>.Instance.Find<T>(name);

            if (ui == null)
            {
                GameObject original = UIRes.LoadPrefab(name);
                if (original != null)
                {
                    GameObject go = GameObject.Instantiate(original);
                    ui = go.GetComponent<T>();

                    if (ui != null)
                    {
                        go.name = name;
                        SingletonMono<UIComponent>.Instance.AddChild(ui);
                    }
                    else
                    {
                        this.LogError("Load() Prefab没有增加对应组件：" + name);
                    }
                }
                else
                {
                    this.LogError("Load() Res Not Found:" + name);
                }
            }

            if (m_listLoadedPanel.IndexOf(ui) < 0)
            {
                m_listLoadedPanel.Add(ui);
            }

            return ui;
        }
        #endregion

        #region Close
        /// <summary>
        /// 关闭所有打开的UI
        /// </summary>
        protected void CloseAllLoadedPanels()
        {
            foreach (var item in m_listLoadedPanel)
            {
                if (item.IsOpen)
                {
                    item.Close();
                }
            }
        }
        #endregion


        //------------------------ UIPage管理 --------------------------------
        #region UIPage管理
        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="form"></param>
        /// <param name="arg"></param>
        public void OpenPage(string form, object arg = null)
        {
            if (m_currentPage != null)
            {
                //如果是打开当前UI
                if (m_currentPage.name.Equals(form))
                {
                    m_currentPage.uiPanel.Open();
                    return;
                }

                m_pageTrackStack.Push(m_currentPage);
            }

            OpenPageWorker(form, arg);
        }

        public void ClosePage()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 返回上一个Page
        /// </summary>
        public void GoBackPage()
        {
            Debuger.Log(LOG_TAG, "GoBackPage()");
            if (m_pageTrackStack.Count > 0)
            {
                var track = m_pageTrackStack.Pop();
                OpenPageWorker(track.name, null);
            }
            else if (m_pageTrackStack.Count == 0)
            {
                EnterMainPage();
            }
        }

        /// <summary>
        /// 进入主Page
        /// 会清空Page堆栈
        /// </summary>
        public void EnterMainPage()
        {
            m_pageTrackStack.Clear();
            OpenPageWorker(MainPage, null);
        }

        #endregion

        //------------------------ UIWindow管理 --------------------------------

        #region UIWindow管理
        public UIWindow OpenWindow(string name, object arg = null)
        {
            UIWindow ui = Open<UIWindow>(name, arg);
            return ui;
        }

        public T OpenWindow<T>(object arg = null) where T : UIWindow
        {
            T ui = Open<T>(typeof(T).Name, arg);
            return ui;
        }
        #endregion

        //------------------------ Widget管理 --------------------------------

        #region Widget管理
        public UIWidget OpenWidget(string name, object arg = null)
        {
            UIWidget ui = Open<UIWidget>(name, arg);
            return ui;
        }

        public T OpenWidget<T>(object arg = null) where T : UIWidget
        {
            T ui = Open<T>(typeof(T).Name, arg);
            return ui;
        }
        #endregion
    }
}
