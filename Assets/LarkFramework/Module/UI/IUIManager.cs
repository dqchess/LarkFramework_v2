using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LarkFramework.UI
{
    public interface IUIManager
    {
        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="uiResRoot">UI资源的根目录，默认为"UI/"</param>
        void Init(string uiResRoot);

        #region Page相关 Page是UI页面
        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="form"></param>
        /// <param name="arg"></param>
        void OpenPage(string page, object arg = null);
        #endregion

        #region Window相关 window是各种弹窗
        /// <summary>
        /// 通过UI名打开窗口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        UIWindow OpenWindow(string name, object arg = null);

        /// <summary>
        /// 通过UI类型打开窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <returns></returns>
        T OpenWindow<T>(object arg = null) where T : UIWindow;
        #endregion

        #region Widget相关 widget是依附于Page的各种挂件
        /// <summary>
        /// 通过UI名打开页面挂件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        UIWidget OpenWidget(string name, object arg = null);

        /// <summary>
        /// 通过类型打开页面挂件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <returns></returns>
        T OpenWidget<T>(object arg = null) where T : UIWidget;
        #endregion
    }
}
