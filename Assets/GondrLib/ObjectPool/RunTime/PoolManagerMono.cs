using UnityEngine;

namespace GondrLib.ObjectPool.RunTime
{
    public class PoolManagerMono : MonoBehaviour 
    {
        [SerializeField] private PoolManagerSO poolManager;

        private void Awake()
        {
            poolManager.InitializePool(transform);
        }

        public T Pop<T>(PoolItemSO item) where T : IPoolable
        {
            return (T)poolManager.Pop(item);
        }

        public void Push(IPoolable target)
        {
            poolManager.Push(target);
        }
    }
}