using VT_Api.Core.Behaviour;

namespace VTProget_X
{
    public class IntercomBehaviour : RepeatingBehaviour
    {
        public IntercomBehaviour()
        {
            this.RefreshTime = 1000;
        }

        protected override void Start()
        {
            Synapse.Api.Logger.Get.Info($"IntercomBehaviour Start");
            base.Start();
        }

        private int _timer = 0;
        protected override void BehaviourAction()
        {
            ScreenType screen;
            _timer++;
            if (((Plugin)Plugin.Instance).CustomScreen)
                screen = ScreenType.Custom;
            else if (Plugin.Instance.Config.IntercomInfomationtime > _timer)
                screen = ScreenType.GeneralInfo;
            else if (Plugin.Instance.Config.IntercomInfomationtime <= _timer)
                screen = ScreenType.ListScp;
            else
                screen = ScreenType.Defaux;

            Plugin.Instance.SetIntercomScreen(screen);

            if (Plugin.Instance.Config.IntercomInfomationtime * 2 < _timer)
                _timer = 0;
        }
    }
}
