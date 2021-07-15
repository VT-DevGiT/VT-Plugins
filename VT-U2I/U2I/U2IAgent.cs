using Synapse;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VT_U2I
{
    public class U2IAgent : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)TeamID.U2I;

        protected override int RoleId => (int)RoleID.U2IAgent;

        protected override string RoleName => Plugin.ConfigU2IAgent.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigU2IAgent;

        List<SerializedMapPoint> SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("Root_*&*Outside Cams", 187.5507f, -8.07251f, -6.48763f),
                new SerializedMapPoint("Root_*&*Outside Cams", 185.9299f, -8.444763f, -1.784706f),
                new SerializedMapPoint("Root_*&*Outside Cams", 183.4525f, -8.931152f, -1.332999f),
                new SerializedMapPoint("Root_*&*Outside Cams", 180.3424f, -9.680847f, -1.697027f),
                new SerializedMapPoint("Root_*&*Outside Cams", 177.6069f, -10.34033f, -1.783141f),
                new SerializedMapPoint("Root_*&*Outside Cams", 175.2793f, -10.65308f, -1.589358f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.6662f, -11.2254f, 2.163314f),
                new SerializedMapPoint("Root_*&*Outside Cams", 172.6244f, -12.77948f, 9.865286f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.8324f, -12.73206f, 7.958255f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.9952f, -12.05273f, 5.345505f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.7792f, -11.40039f, 2.836424f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.6662f, -11.2254f, 2.163314f),
            };

        protected override void AditionalInit()
        {
            int rnd = UnityEngine.Random.Range(0, 12);
            Player.Position = SpawnPoints[rnd].Parse().Position;
        }
    } 
}
