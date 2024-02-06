using System;

namespace EC.Core.Common
{
    public class Transition
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Func<bool> _condition = default;

        /// <summary>
        /// 
        /// </summary>
        private readonly State _targetState = default;

        /// <summary>
        /// 
        /// </summary>
        public Func<bool> Condition => _condition;

        /// <summary>
        /// 
        /// </summary>
        public State TargetState => _targetState;

        /// <summary>
        /// 
        /// </summary>
        private Transition()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetState"></param>
        /// <param name="condition"></param>
        public Transition(State targetState, Func<bool> condition)
        {
            _targetState = targetState;
            _condition = condition;
        }
    }
}
