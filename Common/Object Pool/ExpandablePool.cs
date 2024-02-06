using System;
using Object = UnityEngine.Object;

namespace EC.Core.Common
{
    public class ExpandablePool<T> : Pool<T> where T : Object
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly uint _maximumSize;

        /// <summary>
        /// 
        /// </summary>
        public uint MaximumSize => _maximumSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="seed"></param>
        /// <param name="maximumSize"></param>
        /// <param name="OnCreate"></param>
        /// <param name="OnGet"></param>
        /// <param name="OnRelease"></param>
        public ExpandablePool(IFactory<T> factory, uint seed = 10, uint maximumSize = 1000, Action<T> OnCreate = null, Action<T> OnGet = null,
            Action<T> OnRelease = null) : base(factory, seed, OnCreate, OnGet, OnRelease)
        {
            this._maximumSize = maximumSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override T Get()
        {
            if (_items.Count == 0 && _size < _maximumSize) 
                CreateItem();
            return base.Get();
        }
    }
}
