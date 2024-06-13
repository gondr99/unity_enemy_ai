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
            int cnt = 0;
            while (_openList.Count > 0)
            {
                Node n = _openList.Pop();
                FindOpenList(n);
                _closeList.Add(n);
                if (n.pos == _destination)
                {
                    result = true;
                    break;
                }
            }


            if (result)
            {
                _routePath.Clear();
                Node last = _closeList[_closeList.Count - 1];
                while (last._parent != null)
                {
                    _routePath.Add(Map.GetTileCenterWorld( last.pos));
                    last = last._parent;
                }
                _routePath.Reverse();
            }else{
                Debug.LogWarning("Can not find path");
            }

            return result;
        }

        private void FindOpenList(Node currentNode)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    Vector3Int next = currentNode.pos + new Vector3Int(j, i, 0);

                    //해당 지점은 이미 방문했다.
                    Node n = _closeList.Find(x => x.pos == next);
                    if (n != null) continue;

                    //코너링시 해당 코너에 장애물이 있다면 금지
                    if (_cornerCheck &&
                        (!Map.CanMove(new Vector3Int(next.x, currentNode.pos.y, 0))
                        || !Map.CanMove(new Vector3Int(currentNode.pos.x, next.y, 0)))
                        )
                    {
                        continue;
                    }

                    if (Map.CanMove(next))
                    {
                        //이전노드에서 여기까지 오는 비용 + 이전노드까지 왔던 비용을 더해서 G를 만들어주고
                        float g = (currentNode.pos - next).magnitude + currentNode.G;

                        Node nextOpenNode = new Node { pos = next, _parent = currentNode, G = g, F = g + CaclH(next) };
                        Node exists = _openList.Contains(nextOpenNode);

                        //Debug.Log(nextOpenNode.pos + ", " + nextOpenNode.F);

                        if (exists != null)
                        {
                            //이미 openList에 존재한다면 누가 더 짧은지를 계산해서 넣어준다. 아니면 아무일도 안해줘도 된다.
                            if (nextOpenNode.G < exists.G)
                            {
                                exists.G = nextOpenNode.G;
                                exists.F = nextOpenNode.F;
                                exists._parent = nextOpenNode._parent;
                            }
                        }
                        else
                        {
                            //존재하지 않으면 넣어주고
                            _openList.Push(nextOpenNode);
                        }

                    }
                }
            }
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

