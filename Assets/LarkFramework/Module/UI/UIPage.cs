using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LarkFramework.UI
{
    public class UIPage:UIPanel
    {
        /// <summary>
        /// 返回按钮,大部分Page都有返回按钮
        /// </summary>
        [SerializeField]
        private Button m_btnGoBack;

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
            if (m_btnGoBack != null)
            {
                m_btnGoBack.onClick.AddListener(OnBtnGoBack);
            }
        }

        private void OnDisable()
        {
            if (m_btnGoBack != null)
            {
                m_btnGoBack.onClick.RemoveAllListeners();
            }
        }

        private void OnBtnGoBack()
        {
            UIManager.Instance.GoBackPage();
        }
    }
}
