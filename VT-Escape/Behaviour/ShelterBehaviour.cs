using Mirror;
using Synapse.Api;
using UnityEngine;

namespace VTEscape
{
    public class ShelterBehaviour : NetworkBehaviour
    {
        private Player player;
        public bool Enabled = true;
        private float _timer;
        //private Vector3 _ShelterDoor;

        private void Awake()
        {
            player = gameObject.GetPlayer();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (Enabled && _timer > 1f)
            {

            }

            if (_timer > 1f)
                _timer = 0f;
        }

        public void Destroy()
        {
            Enabled = false;
            DestroyImmediate(this, true);
        }
    }
}
