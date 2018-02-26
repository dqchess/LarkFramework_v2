using UnityEngine;

namespace LarkFramework.Module.Example
{
    /// <summary>
    /// 全局事件
    /// 有些事件不确定应该是由谁发出
    /// 就可以通过全局事件来收和发
    /// </summary>
    public static class GloableEvent
    {
        /// <summary>
        /// true:登录成功，false：登录失败，或者掉线
        /// </summary>
        public static ModuleEvent<bool> onLogin = new ModuleEvent<bool>();
    }

    public class Module_Example : MonoBehaviour
    {

        void Start()
        {
            Init();
        }

        public void Init()
        {
            //Debuger.EnableLog = true;

            ModuleC.Instance.Init();
            ModuleManager.Instance.Init("LarkFramework.Module.Example");

            ModuleManager.Instance.CreateModule("ModuleA");
            ModuleManager.Instance.CreateModule("ModuleB");

            //业务层模块之间，通过Message进行通讯
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_1", 1, 2, 3);
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_2", "abc", 123);

            //业务层模块之间，通过Event进行通讯 
            ModuleManager.Instance.Event("ModuleB", "onModuleEventB").AddListener(OnModuleEventB);

            //业务层调用服务层，通过事件监听回调
            ModuleC.Instance.onEvent.AddListener(OnModuleEventC);
            ModuleC.Instance.DoSomething();

            //全局事件
            GloableEvent.onLogin.AddListener(OnLogin);
        }

        private void OnModuleEventC(object args)
        {
            Debug.Log("OnModuleEventC() args:{"+ args + "}");
        }

        private void OnModuleEventB(object args)
        {
            Debug.Log("OnModuleEventB() args:{" + args + "}");
        }

        private void OnLogin(bool args)
        {
            Debug.Log("OnLogin() args:{" + args + " }");
        }
    }

    public class ModuleA : BusinessModule
    {
        public override void Create(object args = null)
        {
            base.Create(args);

            //业务层模块之间，通过message进行通讯
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_1", 1, 2, 3);
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_2", "abc", 123);

            //业务层模块之间，通过Event进行通讯
            ModuleManager.Instance.Event("ModuleB", "onModuleEventB").AddListener(OnModlueEventB);

            //业务层调用服务层，通过事件监听回调
            ModuleC.Instance.onEvent.AddListener(OnModlueEventC);
            ModuleC.Instance.DoSomething();

            //全局事件
            GloableEvent.onLogin.AddListener(OnLogin);
        }

        private void OnModlueEventB(object args)
        {
            Debug.Log("OnModuleEventB() args{" + args + "}");
        }
        private void OnModlueEventC(object args)
        {
            Debug.Log("OnModlueEventC() args{" + args + "}");
        }

        private void OnLogin(bool args)
        {
            Debug.Log("OnLogin() args:{" + args + "}");
        }
    }

    public class ModuleB : BusinessModule
    {
        public ModuleEvent onModlueEventB
        {
            get
            {
                return Event("onModuleEventB");
            }
        }

        public override void Create(object args = null)
        {
            base.Create(args);
            onModlueEventB.Invoke("aaaaaaaaaaaaaaaaa");
        }

        protected void MessageFromA_2(string args1, int args2)
        {
            Debug.Log("MessageFromA_2() args:{" + args1 + "},{" + args2 + "}");
        }

        protected override void OnModlueMessage(string msg, object[] args)
        {
            base.OnModlueMessage(msg, args);
            Debug.Log("OnModlueMessage() msg:{" + msg + "},args:{" + args[0] + "},{" + args[1] + "},{" + args[2] + "}");
        }
    }

    public class ModuleC : ServiceModule<ModuleC>
    {
        public ModuleEvent onEvent = new ModuleEvent();

        public void Init()
        {

        }

        public void DoSomething()
        {
            onEvent.Invoke(null);
        }
    }

}