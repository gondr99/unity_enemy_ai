using System;
using System.Collections.Generic;
using System.Linq;
using Unity.AppUI.UI;
using UnityEngine;

namespace Gondr.Astar
{
    public class AstarAgent : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private bool _cornerCheck = true;
        [SerializeField] private bool _lineDebug = false;
        
        private Vector3Int _currentPos, _destination;
        private AstarMapManager _map;
        public AstarMapManager Map{
            get  {
                if(_map == null) 
                    _map = AstarMapManager.Instance;
                return _map;
            }
        }

        private Agent _owner;
        private AgentMovement _movement;
        private List<Vector3> _cornerPoints;
        private int _currentPathIndex;
        private Vector2 _beforePosition;

        public bool IsArrived { get; private set; }
        public bool IsPathFailed { get; private set; }
        public bool IsStop { get; set; }
        
        public void Initialize(Agent agent)
        {
            _owner = agent;
            _movement = agent.GetCompo<AgentMovement>();
            _cornerPoints = new List<Vector3>();
        }

        public void SetDestination(Vector3 destination)
        {
            _cornerPoints.Clear();
            IsArrived = false;
            IsPathFailed = false;
            _currentPos = Map.GetTilePosition(transform.position);
            _destination = Map.GetTilePosition(destination);

            if (CalcRoute() == false) //경로 계산 실패시
            {
                IsPathFailed = true;
                return;
            }

            #region Subject

            int cornerIndex = 0;
            _cornerPoints.Add(_routePath[cornerIndex]);
            cornerIndex++;
            
            for (int i = 1; i < _routePath.Count - 1; i++)
            {
                Vector2 beforeDirection = _routePath[i] - _routePath[i - 1];
                Vector2 currentDirection = _routePath[i + 1] - _routePath[i];

                if (Mathf.Abs(Vector2.Angle(beforeDirection, currentDirection)) > 15f)
                {
                    _cornerPoints.Add(_routePath[i]);
                    cornerIndex++;
                }
            }
            _cornerPoints.Add(_routePath[^1]); //마지막 포인트도 넣어준다.
            #endregion
            
            _currentPathIndex = 1;

            
            //코너포인트 정리하고, 목적지 향해서 이동하기 시작하고, 이동완료하면 IsArrived true로 체크해주고.
            //IsStop 고려해서 이동.
        }

        private void Update()
        {
            if (IsStop) return;
            if (_currentPathIndex >= _cornerPoints.Count) return;

            if (CheckArrive() == false)
            {
                Vector2 direction = _cornerPoints[_currentPathIndex] - _owner.transform.position;
                _movement.SetMovement(direction.normalized);
            }
            else
            {
                _movement.StopImmediately();
            }
        }

        private bool CheckArrive()
        {
            Vector2 destination = _cornerPoints[_currentPathIndex];
            Vector2 currentPosition = _owner.transform.position;
            Vector2 beforeDirection = (destination - _beforePosition).normalized;
            Vector2 currentDirection = (destination - currentPosition).normalized;
            _beforePosition = currentPosition;

            if (Vector2.Dot(beforeDirection, currentDirection) <= 0 || Vector2.Distance(destination, currentDirection) < 0.01f)
            {
                _currentPathIndex++;
                if (_currentPathIndex >= _cornerPoints.Count)
                    IsArrived = true;
                return true;
            }

            return false;
        }
        

        private void OnDrawGizmos()
        {
            if (_lineDebug == false) return;
            
            //DrawPoints(_routePath);
            DrawPoints(_cornerPoints);
        }

        private void DrawPoints(List<Vector3> targetList)
        {
            if (targetList == null || targetList.Count < 2) return;

            Gizmos.color = Color.blue;
            for (int i = 0; i < targetList.Count - 1; i++)
            {
                Gizmos.DrawSphere(targetList[i], 0.15f);
                Gizmos.DrawLine(targetList[i], targetList[i+1]);
            }
            Gizmos.DrawSphere(targetList[^1], 0.15f);
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
            _openList.Push(new Node { pos = _currentPos, parent = null, G = 0, F = CalcH(_currentPos) });

            bool result = false;
            
            
            #region Subject
            //이곳에 PathFinding 코드가 들어가야 합니다.
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
                Node last = _closeList[^1];
                while (last.parent != null)
                {
                    _routePath.Add(Map.GetTileCenterWorld(last.pos));
                    last = last.parent;
                }
                _routePath.Add(Map.GetTileCenterWorld(last.pos));
                _routePath.Reverse();
            }
            #endregion

            return result;
        }

        
        private bool CheckCorner(Vector3Int nextPoint, Vector3Int currentPoint)
        {
            #region Subject

            if (_cornerCheck == false) return true;

            //if (nextPoint.x == currentPoint.x || nextPoint.y == currentPoint.y) return true; //직선이동은 통과
            
            //x축과, Y축에 장애물이 있는지 검사해서 있다면 false
            return Map.CanMove(new Vector3Int(nextPoint.x, currentPoint.y)) &&
                   Map.CanMove(new Vector3Int(currentPoint.x, nextPoint.y));

            #endregion
        }
        private void FindOpenList(Node currentNode)
        {
            #region Subject

            //이곳에 오픈리스트를 찾는 코드가 들어가야 합니다.
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x ==0 && y == 0) continue; //자기 자신은 검사하지 않는다.

                    Vector3Int nextPosition = new Vector3Int(x, y) + currentNode.pos;
                    if(Map.CanMove(nextPosition) == false) continue;
                    if (CheckCorner(nextPosition, currentNode.pos) == false )
                        continue;
                    
                    bool isVisited = _closeList.Any(node => node.pos == nextPosition);
                    if(isVisited) continue; //이미 방문한 노드는 방문하지 않는다.

                    float newG = currentNode.G + Vector3Int.Distance(currentNode.pos, nextPosition);

                    Node nextNode = new Node
                    {
                        pos = nextPosition,
                        parent = currentNode,
                        G = newG, F = newG + CalcH(nextPosition)
                    };

                    Node existNode = _openList.Contains(nextNode); //포함하고 있는지 검사

                    if (existNode == null)
                    {
                        _openList.Push(nextNode);
                    }
                }
            }

            #endregion
           
        }

        //위치로부터 F값을 구하는 함수
        private float CalcH(Vector3Int pos)
        {
            Vector3Int distance = _destination - pos;
            return distance.magnitude;
        }

        #endregion

        
    }

}

