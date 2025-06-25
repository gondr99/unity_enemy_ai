using UnityEngine;

namespace _01Scripts.BTEnemy.NinjaBT
{
    [CreateAssetMenu(fileName = "Param", menuName = "SO/Anim/Param", order = 0)]
    public class AnimParamSo : ScriptableObject
    {
        public string paramName;
        public int hashValue;

        private void OnValidate()
        {
            if(string.IsNullOrEmpty(paramName ) == false)
                hashValue = Animator.StringToHash(paramName);
        }
    }
}