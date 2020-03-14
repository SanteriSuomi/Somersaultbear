using System.Collections.Generic;
using UnityEngine;

namespace Somersaultbear
{
    public class ObjectPool<T> : Singleton<ObjectPool<T>> where T : Component
    {
        private Queue<T> pool;
        private Transform poolParent;
        [SerializeField]
        private T prefabToPool = default;
        [SerializeField]
        private int poolSize = 25;
        [SerializeField]
        private SCOVariableBool isPooled = default;

        protected override void Awake()
        {
            base.Awake();
            if (!isPooled.Value) // Ensure pool is only spawned once per game
            {
                isPooled.Value = true;
                InitializePool();
            }
        }

        private void InitializePool()
        {
            pool = new Queue<T>(poolSize);
            poolParent = new GameObject($"{typeof(T).Name} Pool Objects").transform;
            DontDestroyOnLoad(poolParent);
            for (int i = 0; i < poolSize; i++)
            {
                AddNewObjectToPool(); 
            }
        }

        private void OnDestroy() 
            => isPooled.Value = false;

        private void OnApplicationQuit() 
            => isPooled.Value = false;

        /// <summary>
        /// Get an object from the pool.
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            T peekObj = pool.Peek();
            if (peekObj == null)
            {
                AddNewObjectToPool();
            }

            T popObj = pool.Dequeue();
            if (popObj != null)
            {
                ActivateObj(popObj, true);
            }

            return popObj;
        }

        /// <summary>
        /// Return an object to the pool.
        /// </summary>
        /// <param name="returnObj"></param>
        public void Return(T returnObj)
        {
            ActivateObj(returnObj, false);
            pool.Enqueue(returnObj);
        }

        private void AddNewObjectToPool()
        {
            T newObj = Instantiate(prefabToPool);
            ActivateObj(newObj, false);
            newObj.transform.SetParent(poolParent);
            pool.Enqueue(newObj);
        }

        private void ActivateObj(T poolObj, bool activate)
            => poolObj.gameObject.SetActive(activate);
    }
}