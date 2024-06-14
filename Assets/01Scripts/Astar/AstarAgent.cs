using System.Collections.Generic;
using UnityEngine;

namespace Gondr.Astar
{
    public class AstarAgent : MonoBehaviour
    {

        [SerializeField] private bool _cornerCheck = true;
        [SerializeField] private bool _lineDebug = false;

        [SerializeField] private LineDrawer _lineDrawer;

        private Vector3Int _currentPos, _destination;
        private AstarMapManager _map;
        public AstarMapManager Map{
            get  {
                if(_map == null) 
                    _map = AstarMapManager.Instance;
                return _map;
            }
        }

        private void Awake()
        {
            _lineDrawer = transform.Find("LineDrawer").GetComponent<LineDrawer>();
            if(_lineDebug)
                _lineDrawer.gameObject.SetActive(true);
        }

        public void SetDestination(Vector3 destination)
        {
            _currentPos = Map.GetTilePosition(transform.position);
            _destination = Map.GetTilePosition(destination);

            CalcRoute();

            if(_lineDebug)
            {
                _lineDrawer.DrawLine(_routePath.ToArray());
            }
        }

        public List<Vector3> GetPath()
        {
            //return new List<Vector3>();
            return _routePath;
        }

        #region A Star Algorithm
        //F = G + H
        // G는 현재까지 이동할 때 비용, 여기서부터 목적지까지의 어림잡아 비용(장애물 없다고 가정하고)
        private PriorityQueue<Node> _openList;
        private List<Node> _closeList;
        private List<Vector3> _routePath = new List<Vector3>();

        private bool CalcRoute()
        {
            _openList = new PriorityQueue<Node>();
            _closeList = new List<Node>();

            //맨 처음 시작지점을 openList에 넣는다.
            _openList.Push(new Node { pos = _currentPos, _parent = null, G = 0, F = CaclH(_currentPos) });

            bool result = false;
            
            //이곳에 PathFinding 코드가 들어가야 합니다.

            return result;
        }

        private void FindOpenList(Node currentNode)
        {
           //이곳에 오픈리스트를 찾는 코드가 들어가야 합니다.
        }

        //위치로부터 F값을 구하는 함수
        private float CaclH(Vector3Int pos)
        {
            Vector3Int distance = _destination - pos;
            return distance.magnitude;
        }

        #endregion
    }

}

