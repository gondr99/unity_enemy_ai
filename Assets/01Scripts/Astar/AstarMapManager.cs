using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gondr.Astar
{
    public class AstarMapManager : MonoBehaviour
    {
        public static AstarMapManager Instance;
        [SerializeField] private Tilemap _floorTile, _collisionTile;

        private void Awake()
        {
            Instance = this;
        }

        public Vector3Int GetTilePosition(Vector3 worldPosition)
        {
            return _floorTile.WorldToCell(worldPosition);
        }

        public Vector3 GetTileCenterWorld(Vector3Int tilePosition)
        {
            return _floorTile.GetCellCenterWorld(tilePosition);
        }

        public bool CanMove(Vector3Int pos)
        {
            TileBase tile = _floorTile.GetTile(pos);
            if (tile == null)
            {
                return false;
            }

            //해당 타일이 비어있으면 
            return _collisionTile.GetTile(pos) == null;
        }

    }
}

