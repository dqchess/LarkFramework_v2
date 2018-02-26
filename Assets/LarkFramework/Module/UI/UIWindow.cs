using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LarkFramework.UI
{
    public class UIWindow : UIPanel
    {
        /// <summary>
        /// 返回按钮,大部分Window都有关闭按钮
        /// </summary>
        [SerializeField]
        private Button m_btnClose;

        public override void Open(object arg = null)
        {
            base.Open(arg);

            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
        }

        public override void Close(object arg = null)
        {
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            base.Close(arg);
        }

        private void OnEnable()
        {
            if (m_btnClose != null)
            {
                m_btnClose.onClick.AddListener(OnBtnGoBack);
            }
        }

        private void OnDisable()
        {
            if (m_btnClose != null)
            {
                m_btnClose.onClick.RemoveAllListeners();
            }
        }

        private void OnBtnGoBack()
        {
            Close();
        }
    }
}
