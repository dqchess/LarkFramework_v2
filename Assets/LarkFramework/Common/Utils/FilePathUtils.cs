using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LarkFramework.Common
{
    public static class FilePathUtils
    {
        /// <summary>
        /// PersistentDataPath
        /// </summary>
        /// <returns></returns>
        public static string PersistentDataPath()
        {
            string path = string.Empty;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            path = Application.persistentDataPath;

#elif UNITY_IPHONE
            path = Application.persistentDataPath;

#elif UNITY_ANDROID
            path = Application.persistentDataPath;
#endif
            return path;
        }

        /// <summary>
        /// StreamingAssetPath
        /// </summary>
        /// <returns></returns>
        public static string StreamingAssetPath()
        {
            string path = string.Empty;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            path = Application.streamingAssetsPath;

#elif UNITY_IPHONE
            path = Application.dataPath +"/Raw";

#elif UNITY_ANDROID
            path = Application.streamingAssetsPath;
#endif
            return path;
        }
    }
}
