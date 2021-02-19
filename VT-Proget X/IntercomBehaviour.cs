using Mirror;

namespace VTProget_X
{
    public class IntercomBehaviour : NetworkBehaviour
    {
        void Start()
        {
            Synapse.Api.Logger.Get.Info($"IntercomBehaviour Start");
            InvokeRepeating("IntercomInfoRoutine", 1, 1);
        }

        private int _timer = 0;
        public void IntercomInfoRoutine()
        {

            screenEnum screen;
            _timer++;
            if (Plugin.Instance.CustomScreen)
                screen = screenEnum.Custom;
            else if ((Plugin.Config.IntercomInfomationtime / 2) > _timer)
                screen = screenEnum.GeneralInfo;
            else if ((Plugin.Config.IntercomInfomationtime / 2) <= _timer)
                screen = screenEnum.ListScp;
            else
                screen = screenEnum.Defaux;

            Methode.SendInterComInfoGeneral(screen);
            if (Plugin.Config.IntercomInfomationtime < _timer)
                _timer = 0;
        }

    }
}
