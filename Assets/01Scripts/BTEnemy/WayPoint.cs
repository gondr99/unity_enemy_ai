using UnityEngine;

namespace _01Scripts.BTEnemy
{
    public class WayPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.15f);
        }
    }
}