using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace EC.Core.Common
{
    public class Pool<T> : IPool<T> where T : Object
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IFactory<T> _factory;
        
        /// <summary>
        /// 
        /// </summary>
        protected readonly Stack<T> _items;
        
        /// <summary>
        /// 
        /// </summary>
        protected readonly uint _seed;
        
        /// <summary>
        /// 
        /// </summary>
        protected uint _size;

        /// <summary>
        /// 
        /// </summary>
        public IFactory<T> Factory => _factory;
        
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<T> Items => _items;
        
        /// <summary>
        /// 
        /// </summary>
        public uint Size => _size;

        /// <summary>
        /// 
        /// </summary>
        public event Action<T> OnCreate;

        /// <summary>
        /// 
        /// </summary>
        public event Action<T> OnGet;

        /// <summary>
        /// 
        /// </summary>
        public event Action<T> OnRelease;

        /// <summary>
        ///
        /// </summary>
        private Pool()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="seed"></param>
        /// <param name="OnCreate"></param>
        /// <param name="OnGet"></param>
        /// <param name="OnRelease"></param>
        public Pool(IFactory<T> factory, uint seed = 10, Action<T> OnCreate = null, Action<T> OnGet = null, Action<T> OnRelease = null, bool autoInitialize = false)
        {
            if (factory == null)
            {
                const string ExceptionMessage = "Factory is null!";
                throw new NullReferenceException(ExceptionMessage);
            }

            this._factory = factory;
            this._seed = seed;
            this.OnCreate += OnCreate;
            this.OnGet += OnGet;
            this.OnRelease += OnRelease;
            this._items = new Stack<T>((int) _seed);
            if(autoInitialize) Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            for (int index = 0; index < _seed; index++)
            {
                CreateItem();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void CreateItem()
        {
            _size++;
            T item = _factory.Create();
            _items.Push(item);
            OnCreate?.Invoke(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual T Get()
        {
            if (_items.Count == 0) return null;

            T item = _items.Pop();
            OnGet?.Invoke(item);
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryGet(out T item)
        {
            item = Get();
            return item is not null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="releasedItem"></param>
        public virtual void Release(T releasedItem)
        {
            _items.Push(releasedItem);
            OnRelease?.Invoke(releasedItem);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {
            this.OnCreate = null;
            this.OnGet = null;
            this.OnRelease = null;
            
            for (int index = 0, count = _items.Count; index < count; index++)
            {
                _items.Pop();
            }
        }
    }
}
