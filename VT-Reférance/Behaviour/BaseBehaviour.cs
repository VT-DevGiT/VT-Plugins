using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Behaviour
{
    public abstract class BaseRepeatingBehaviour : NetworkBehaviour
    {
        protected bool _Started = true;
        /// <summary>
        /// Time in millisecond between behaviour execution
        /// </summary>
        public virtual int RefreshTime { get; set; }
        public BaseRepeatingBehaviour()
        {
            RefreshTime = 1000;
        }
        /// <summary>
        /// Stop the repeating Action and destroy the behaviour
        /// </summary>
        public void Kill()
        {
            CancelInvoke("BehaviourAction");
            Destroy(this);
        }

        /// <summary>
        /// BehaviourAction is the action that will be repeated every RefreshTime milisecond
        /// </summary>
        protected abstract void BehaviourAction();

        void OnEnable()
        {
            ActionExecute();
        }

        void OnDisable()
        {
            ActionStop();
        }

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
