using Mirror;
using Synapse.Api;

namespace VT_Referance.Behaviour
{
    [API]
    public abstract class BaseRepeatingBehaviour : NetworkBehaviour
    {
        protected bool _Started = true;

        /// <summary>
        /// Time in millisecond between behaviour execution
        /// </summary>
        [API]
        public virtual int RefreshTime { get; set; }


        public BaseRepeatingBehaviour()
        {
            RefreshTime = 1000;
        }


        /// <summary>
        /// Stop the repeating Action and destroy the behaviour
        /// </summary>
        [Unstable]
        public void Kill()
        {
            CancelInvoke("BehaviourAction");
            Destroy(this);
        }

        /// <summary>
        /// BehaviourAction is the action that will be repeated every RefreshTime milisecond
        /// </summary>
        [API]
        protected abstract void BehaviourAction();
        
        [API]
        protected virtual void OnEnable()
        {
            ActionExecute();
        }
        
        [API]
        protected virtual void OnDisable()
        {
            ActionStop();
        }

        [API]
        protected virtual void Start()
        {
           _Started = false;
            ActionExecute();
        }

        /// <summary>
        /// laucnh the reapting action if it is not started
        /// </summary>
        private void ActionExecute()
        {
            if (!_Started)
            {
                _Started = true;
                float delay = ((float)RefreshTime) / 1000;
                InvokeRepeating("BehaviourAction", delay, delay);
            }
        }
        /// <summary>
        /// Stop the reapting action
        /// </summary>
        private void ActionStop()
        {
            _Started = false;
            CancelInvoke("BehaviourAction");
        }
    }
}
