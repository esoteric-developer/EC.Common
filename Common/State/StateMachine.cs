using System;
using System.Linq;
using System.Collections.Generic;

namespace EC.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        /// 
        /// </summary>
        protected State _currentState = default;

        /// <summary>
        /// 
        /// </summary>
        protected readonly List<Transition> _anyTransitions = default;

        /// <summary>
        /// 
        /// </summary>
        public State CurrentState => _currentState;

        /// <summary>
        /// 
        /// </summary>
        public event Action<State, State> OnStateChanged;

        /// <summary>
        /// 
        /// </summary>
        public StateMachine()
        {
            _anyTransitions = new List<Transition>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize(State initialState)
        {
            _currentState = initialState;
            _currentState?.Enter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(State state)
        {
            State previousState = _currentState;
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
            OnStateChanged?.Invoke(previousState, _currentState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transition"></param>
        public void AddAnyTransition(Transition transition) => _anyTransitions.Add(transition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transition"></param>
        public void RemoveAnyTransition(Transition transition) => _anyTransitions.Remove(transition);

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            Transition transition = GetTransition();
            if (transition != null)
                ChangeState(transition.TargetState);
            _currentState?.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LateUpdate() => _currentState?.FixedUpdate();

        /// <summary>
        /// 
        /// </summary>
        public void FixedUpdate() => _currentState?.LateUpdate();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Transition GetTransition() => _anyTransitions
                                                  .FirstOrDefault(transition => transition.Condition()) ?? 
                                              _currentState?.Transitions
                                                  .FirstOrDefault(transition => transition.Condition != null && transition.Condition());
    }
}
