using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.UI
{
    public abstract partial class UIPanel : MonoBehaviour, IUIPanel
    {
        /// <summary>
        /// 当前UI是否打开
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return this.gameObject.activeSelf;
            }
        }

        /// <summary>
        /// UI所属Canvas组
        /// </summary>
        public string CanvasGroup;

        /// <summary>
        /// UI打开
        /// </summary>
        /// <param name="arg"></param>
        public virtual void Open(object arg = null)
        {
            GUIAniOpen();
        }

        /// <summary>
        /// UI关闭
        /// </summary>
        public virtual void Close(object arg = null)
        {
            Close(0, arg);
        }

        /// <summary>
        /// UI销毁
        /// </summary>
        public virtual void Destroy()
        {

        }

        /// <summary>
        /// 设置UI深度
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetDepth(int value)
        {

        }

        /// <summary>
        /// UI置顶
        /// </summary>
        public virtual void BringTop()
        {

        }

    }
}
