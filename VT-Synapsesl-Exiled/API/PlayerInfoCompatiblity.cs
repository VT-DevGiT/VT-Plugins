using UnityEngine;

using ExiledPlayer = Exiled.API.Features.Player;
using SynapsePlayer = Synapse.Api.Player;
using QurrePlayer = Qurre.API.Player;

namespace VT_MultieLoder.API
{
    public class PlayerInfoCompatiblity : MonoBehaviour
    {
        internal PlayerInfoCompatiblity()
        {
            SynapsePlayer = gameObject.GetPlayer();
            ExiledPlayer = new ExiledPlayer(gameObject);
            QurrePlayer = new QurrePlayer(gameObject);

            ExiledPlayer.Dictionary.Add(gameObject, ExiledPlayer);
            QurrePlayer.Dictionary.Add(gameObject, QurrePlayer);
            QurrePlayer.UserIDPlayers.Add(QurrePlayer.UserId, QurrePlayer);
        }


        private QurrePlayer _qurrePlayer;
        private ExiledPlayer _exiledPlayer;
        private SynapsePlayer _synapsePlayer;

        public QurrePlayer QurrePlayer
        {
            get { return _qurrePlayer; }
            set { _qurrePlayer = value; }
        }


        public ExiledPlayer ExiledPlayer
        {
            get { return _exiledPlayer; }
            set { _exiledPlayer = value; }
        }


        public SynapsePlayer SynapsePlayer
        {
            get { return _synapsePlayer; }
            set { _synapsePlayer = value; }
        }
    }
}
