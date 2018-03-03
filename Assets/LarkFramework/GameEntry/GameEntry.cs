using System;
using LarkFramework.Module;
using LarkFramework.UI;
using UnityEngine;
using LarkFramework.FSM;
using LarkFramework.Tick;
using LarkFramework.Procedure;
using LarkFramework.Scenes;

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

        private void Start()
        {
            switch (lanuchType)
            {
                case LaunchType.Debug:
                    DebugLaunch();
                    break;

                case LaunchType.Release:
                    ReleaseLaunch();
                    break;
            }
        }

        public void Init()
        {
            InitBuiltinComponents();
            InitCustomComponents();

            DontDestroyOnLoad(gameObject);
        }

        private void DebugLaunch()
        {
            Debuger.EnableLog = true;
            UIManager.Instance.OpenPage(startUI);
        }

        private void ReleaseLaunch()
        {
            Debuger.EnableLog = false;
        }

        private void InitBuiltinComponents()
        {
            ModuleManager.Instance.Init("Project");

            TickManager.Instance.Init();

            FSMManager.Instance.Init();
            //ProcedureManager.Instance.Init();
            ScenesManager.Instance.Init();

            //SingletonMono<ResourcesMgr>.Create();
            UIManager.Instance.Init();
        }
    }
}
