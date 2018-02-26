using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LarkFramework.UI
{
    public static class UIRes
    {
        public static string UIResRoot = "UI/";

        /// <summary>
        /// 加载UI的Prefab
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject LoadPrefab(string name)
        {
            Debug.Log("[UIRes]Load Asset:" + UIResRoot + name);
            GameObject asset = (GameObject)Resources.Load(UIResRoot + name);
            return asset;
        }
    }
}
