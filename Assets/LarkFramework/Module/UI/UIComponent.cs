using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.UI
{
    /// <summary>
    /// 界面组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Lark Framework/UI Component")]
    public class UIComponent : SingletonMono<UIComponent>, IUIComponent
    {
        public const string LOG_TAG = "[UIComponent]";

        /// <summary>
        /// 多Canvas管理
        /// </summary>
        public CanvasGroup[] canvasGroup;

        /// <summary>
        /// 从UIRoot下通过类型寻找一个组件对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Find<T>() where T : MonoBehaviour
        {
            string name = typeof(T).Name;
            GameObject obj = Find(name);
            if (obj != null)
            {
                return obj.GetComponent<T>();
            }

            return null;
        }

        /// <summary>
        /// 从UIRoot下通过名字和类型寻找一个组件对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Find<T>(string name) where T : MonoBehaviour
        {
            GameObject obj = Find(name);
            if (obj != null)
            {
                return obj.GetComponent<T>();
            }

            return null;
        }

        /// <summary>
        /// 在UIRoot下通过名字寻找一个GameObject对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject Find(string name)
        {
            Transform obj = null;

            foreach(var group in canvasGroup)
            {
                //TODO:因路径名导致找不到物体，传过来的名字为Menu/MenuPage,Find查找规则为(子节点/孙节点)，此时会找不到
                //var a = root.transform.Find("MenuPage(Clone)");
                //obj = root.transform.Find(name);

                for (int i = 0; i < group.canvas.gameObject.transform.childCount; i++)
                {
                    if (group.canvas.gameObject.transform.GetChild(i).name.Equals(name))
                    {
                        obj = group.canvas.gameObject.transform.GetChild(i);
                        break;
                    }
                }
            }

            if (obj != null)
            {
                return obj.gameObject;
            }

            return null;
        }

        /// <summary>
        /// 通过名字获取UI的Canvas组
        /// </summary>
        /// <returns></returns>
        public CanvasGroup GetCanvasGroup(string canvasName)
        {
            if (canvasGroup.Length == 0)
            {
                Debuger.LogError(LOG_TAG, "GetCanvasGroup() CanvasGroup.Length==0!!!");
                return null;
            }

            if (string.IsNullOrEmpty(canvasName))
            {
                Debuger.LogError(LOG_TAG, "GetCanvasGroup() canvasName:{"+ canvasName + "} Is Null!!!");
                return canvasGroup[0];
            }

            foreach (var group in canvasGroup)
            {
                if (group.canvasName.Equals(canvasName))
                {
                    return group;
                }
            }

            Debuger.LogError(LOG_TAG, "GetCanvasGroup() canvasName:{" + canvasName + "} Is Not Exist!!!");
            return null;
        }

        /// <summary>
        /// 当一个UIPage/UIWindow/UIWidget添加到UIRoot下面
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(UIPanel child)
        {
            Canvas root = GetCanvasGroup(child.CanvasGroup).canvas;
            if (root == null || child == null)
            {
                return;
            }

            child.transform.SetParent(root.transform, false);
            return;
        }
    }

    /// <summary>
    /// Canvas组
    /// </summary>
    [Serializable]
    public class CanvasGroup
    {
        public string canvasName;
        public Canvas canvas;
    }
}