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

        public void Initialize(Agent agent)
        {
            
        }

        [SerializeField] private Transform targetTrm;
        [ContextMenu("Test")]
        private void Test()
        {
            SetDestination(targetTrm.position);
        }

        public void SetDestination(Vector3 destination)
        {
            _currentPos = Map.GetTilePosition(transform.position);
            _destination = Map.GetTilePosition(destination);

            CalcRoute();
        }

        public List<Vector3> GetPath()
        {
            //return new List<Vector3>();
            return _routePath;
        }

        private void OnDrawGizmos()
        {
            if (_lineDebug == false) return;
            if (_routePath.Count < 2) return;

            Gizmos.color = Color.red;
            for (int i = 0; i < _routePath.Count - 1; i++)
            {
                Gizmos.DrawSphere(_routePath[i], 0.15f);
                Gizmos.DrawLine(_routePath[i], _routePath[i+1]);
            }
            Gizmos.DrawSphere(_routePath[^1], 0.15f);
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

