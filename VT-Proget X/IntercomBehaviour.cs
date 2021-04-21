using Mirror;
using Synapse;
using VT_Referance.Behaviour;

namespace VTProget_X
{
    public class IntercomBehaviour : BaseRepeatingBehaviour
    {
        public IntercomBehaviour()
        {
            this.RefreshTime = 500;
        }

        protected override void Start()
        {
            Synapse.Api.Logger.Get.Info($"IntercomBehaviour Start");
            base.Start();
        }

        private int _timer = 0;
        protected override void BehaviourAction()
        {
            screenEnum screen;
            _timer++;
            if (Plugin.Instance.CustomScreen)
                screen = screenEnum.Custom;
            else if (Plugin.Config.IntercomInfomationtime > _timer)
                screen = screenEnum.GeneralInfo;
            else if (Plugin.Config.IntercomInfomationtime <= _timer)
                screen = screenEnum.ListScp;
            else
                screen = screenEnum.Defaux;

            Methode.SendInterComInfoGeneral(screen);
            if (Plugin.Config.IntercomInfomationtime*2 < _timer)
                _timer = 0;
        }

    }
}
