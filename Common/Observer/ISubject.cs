using System.Collections.Generic;

namespace EC.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// 
        /// </summary>
        List<IObserver> Observers { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        void Register(IObserver observer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        void Unregister(IObserver observer);

        /// <summary>
        /// 
        /// </summary>
        void Notify(object[] args = null);
    }
}
