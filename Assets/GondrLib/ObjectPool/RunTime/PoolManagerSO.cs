using System.Collections.Generic;
using UnityEngine;

namespace GondrLib.ObjectPool.RunTime
{
    [CreateAssetMenu(fileName = "PoolManager", menuName = "SO/Pool/Manager", order = 0)]
    public class PoolManagerSO : ScriptableObject
    {
        public List<PoolItemSO> itemList = new ();

        private Dictionary<PoolItemSO, Pool> _pools;
        private Transform _rootTrm;

        public void InitializePool(Transform rootTrm)
        {
            _rootTrm = rootTrm;
            _pools = new Dictionary<PoolItemSO, Pool>();

            foreach (var item in itemList)
            {
                IPoolable poolable = item.prefab.GetComponent<IPoolable>();
                Debug.Assert(poolable != null, $"Pooling item does not have Ipoolable script");
                
                Transform poolParent = new GameObject(item.prefab.name).transform;
                poolParent.SetParent(_rootTrm);
                
                Pool pool = new Pool(poolable, poolParent, item.initCount);
                _pools.Add(item, pool);
            }
        }

        public IPoolable Pop(PoolItemSO item)
        {
            if (_pools.TryGetValue(item, out Pool pool))
            {
                return pool.Pop();
            }

            return null;
        }

        public void Push(IPoolable item)
        {
            if (_pools.TryGetValue(item.PoolItem, out Pool pool))
            {
                pool.Push(item);
            }
            else
            {
                Debug.LogError($"Pooling item {item.PoolItem} does not exist");
            }
        }

    }
}