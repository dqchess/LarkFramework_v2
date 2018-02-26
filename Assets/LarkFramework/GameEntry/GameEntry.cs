using System;
using LarkFramework.Module;
using LarkFramework.UI;
using UnityEngine;

namespace LarkFramework.GameEntry
{
    [DisallowMultipleComponent]
    [AddComponentMenu("LarkFramework/GameEntry")]
    public partial class GameEntry : MonoBehaviour
    {
        public LaunchType lanuchType = LaunchType.Debug;
        public string startScene;
        public string startUI;
        public string startAudio;

        public enum LaunchType
        {
            Debug = 1,
            Release = 2,
        }

        // Use this for initialization
        void Awake()
        {
            Init();
        }

        public void Init()
        {
            InitBuiltinComponents();
            InitCustomComponents();

            switch (lanuchType)
            {
                case LaunchType.Debug:
                    DebugLaunch();
                    break;

                case LaunchType.Release:
                    ReleaseLaunch();
                    break;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void DebugLaunch()
        {
            Debuger.EnableLog = true;

            UIManager.Instance.OpenPage(UIDef.HomePage);
        }

        private void ReleaseLaunch()
        {
            Debuger.EnableLog = false;
        }

        private void InitBuiltinComponents()
        {
            ModuleManager.Instance.Init("Project");

            //SingletonMono<ResourcesMgr>.Create();
            UIManager.Instance.Init();
        }
    }
}
