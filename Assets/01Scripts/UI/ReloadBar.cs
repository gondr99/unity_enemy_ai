using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    [SerializeField] private Transform fillBar;

    public void ActiveReloadBar(bool isReloading)
    {
        gameObject.SetActive(isReloading);
    }
    
    public void SetNormalizeValue(float value)
    {
        Vector3 scale = fillBar.localScale;
        scale.x = Mathf.Clamp01(value);
        fillBar.localScale = scale;
    }

    public void FlipBar(bool isFacingRight)
    {
        transform.localEulerAngles = isFacingRight ? Vector3.zero : new Vector3(0, 180f, 0);
    }
}
