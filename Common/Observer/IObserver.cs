namespace EC.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="args"></param>
        void OnNotified(ISubject subject, object[] args = null);
    }
}
