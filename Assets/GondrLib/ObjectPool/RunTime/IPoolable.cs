using UnityEngine;

namespace GondrLib.ObjectPool.RunTime
{
    public interface IPoolable
    {
        public PoolItemSO PoolItem { get; }
        public GameObject GameObject { get; }
        public void SetUpPool(Pool pool); //안해도 된다.
        public void ResetItem();
    }
}