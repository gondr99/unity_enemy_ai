using UnityEngine;

namespace _01Scripts.BTEnemy
{
    public class WayPoints : MonoBehaviour
    {
        [SerializeField] private WayPoint[] wayPoints;

        private int _currentIdx;
        
        public Vector3 GetNextWayPoint()
        {
            Debug.Assert(wayPoints.Length >= 1, "1개 이상의 웨이포인트를 가져야합니다.");
            _currentIdx = (_currentIdx + 1) % wayPoints.Length;
            return wayPoints[_currentIdx].Position;
        }
    }
}