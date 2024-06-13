using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    [SerializeField] private Tilemap _mapTile, _collisionTile;

    private List<Vector3Int> _directions;

    private void Awake()
    {
        Instance = this;    

        _directions = new List<Vector3Int>(8);
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                _directions.Add(new Vector3Int(j, i));
            }
        }
    }

    public bool GetRandomFromPosition(Vector3 from, out Vector3 to)
    {
        Vector3Int fromTilePos = _mapTile.WorldToCell(from);

        for(int i = _directions.Count -1; i >= 0; i--)
        {
            int idx = Random.Range(0, i+1);
            Vector3Int nextPos = fromTilePos + _directions[idx];
            if(IsValidTilePos(nextPos)){
                to = _mapTile.GetCellCenterWorld(nextPos);
                return true;
            }
            //Swap
            (_directions[idx], _directions[i]) = (_directions[i], _directions[idx]);
        }
        to = Vector3.zero;
        return false;
    }

    private bool IsValidTilePos(Vector3Int tilePos)
    {
        TileBase tile = _mapTile.GetTile(tilePos);
        TileBase colliderTile = _collisionTile.GetTile(tilePos);

        return tile != null && colliderTile == null;
    }
}
