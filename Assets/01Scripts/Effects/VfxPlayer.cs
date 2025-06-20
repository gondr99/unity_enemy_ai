using GondrLib.ObjectPool.RunTime;
using UnityEngine;
using UnityEngine.VFX;
public class VfxPlayer : MonoBehaviour, IPoolable
{
    [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
    public GameObject GameObject => gameObject;
    
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private float effectDuration;
    private Pool _myPool;
    public void SetUpPool(Pool pool) => _myPool = pool;

    public void ResetItem()
    {
        
    }

    public async void PlayVfx(Vector3 position)
    {
        transform.position = position;
        visualEffect.Play();
        await Awaitable.WaitForSecondsAsync(effectDuration);
        _myPool.Push(this);
    }
}
