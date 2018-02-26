using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LarkFramework.UI
{
    public class UIWidget : UIPanel
    {

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
    }
}
