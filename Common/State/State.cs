using System;
using System.Collections.Generic;

namespace EC.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly StateMachine _stateMachine = default;

        /// <summary>
        /// 
        /// </summary>
        protected readonly List<Transition> _transitions = default;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Transition> Transitions => _transitions;
        
        /// <summary>
        /// 
        /// </summary>
        public event Action OnEntered;

        /// <summary>
        /// 
        /// </summary>
        public event Action OnExited;

        /// <summary>
        /// 
        /// </summary>
        private State()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateMachine"></param>
        protected State(StateMachine stateMachine)
        {
            _transitions = new List<Transition>();
            this._stateMachine = stateMachine;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transition"></param>
        public void AddTransition(Transition transition) => _transitions.Add(transition);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transition"></param>
        public void RemoveTransition(Transition transition) => _transitions.Remove(transition);
        
        /// <summary>
        /// 
        /// </summary>
        public void Enter()
        {
            OnEnter();
            OnEntered?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Exit()
        {
            OnExit();
            OnExited?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update() => OnUpdate();

        /// <summary>
        /// 
        /// </summary>
        public void LateUpdate() => OnLateUpdate();

        /// <summary>
        /// 
        /// </summary>
        public void FixedUpdate() => OnFixedUpdate();

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnUpdate()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnFixedUpdate()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnLateUpdate()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnEnter();
        
        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnExit();
    }
}
