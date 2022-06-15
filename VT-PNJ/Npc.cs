using UnityEngine;
using UnityEngine.AI;
using VT_Api.Extension;

namespace VT_PNJ
{
    public class Npc : MonoBehaviour
    {
        #region Properties & Variable
        public NavMeshAgent Agent { get; set; }
        public Vector3 Destination { get => Agent.destination; set => Agent.destination = value; }
            
        #endregion

        #region Constructor & Destructor
        public Npc()
        {

        }

        #endregion

        #region Unity
        void Start()
        {

        }

        void Awake()
        {
            Agent = this.gameObject.GetOrAddComponent<NavMeshAgent>();
        }

        void Update()
        {
            
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        #endregion
    }
}
