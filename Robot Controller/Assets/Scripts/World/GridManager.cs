using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField] private float gridSize = 1f;
    [SerializeField] Tilemap map;
    [SerializeField] TileDataBase tileDataBase;

    private readonly Dictionary<Vector2Int, TerrainType> tileMap =
        new Dictionary<Vector2Int, TerrainType>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        ScanTileMap();
    }

    public bool IsPassable(Vector2Int pos)
    {
        if (!tileMap.TryGetValue(pos, out TerrainType type)) return true;
        return type != TerrainType.Wall && type != TerrainType.Table;
    }

    public float GetExtraEnergyCost(Vector2Int pos)
    {
        if (!tileMap.TryGetValue(pos, out TerrainType type)) return 0f;
        return type switch
        {
            TerrainType.WetFloor => 5f,
            TerrainType.Goo      => 15f,
            _                    => 0f
        };
    }

    public TerrainType GetTerrainType(Vector2Int pos)
    {
        tileMap.TryGetValue(pos, out TerrainType type);
        return type;
    }

    public Vector2Int WorldToGrid(Vector3 worldPos) => new Vector2Int(
        Mathf.FloorToInt(worldPos.x / gridSize),
        Mathf.FloorToInt(worldPos.y / gridSize)
    );

    public Vector3 GridToWorld(Vector2Int pos, float z = 0f) =>
        new Vector3(pos.x * gridSize + gridSize * 0.5f,
                    pos.y * gridSize + gridSize * 0.5f, z);

    private void ScanTileMap()
    {
        tileMap.Clear();

        BoundsInt bounds = map.cellBounds;

        foreach(Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = map.GetTile(pos);
            if(tile == null)
                continue;
            TileGroupData tileGroupData = tileDataBase.groups.FirstOrDefault(g => g.tiles.Contains(tile));

            if(tileGroupData != null) tileMap[new Vector2Int(pos.x,pos.y)] = tileGroupData.terrainType;
        }
    }
}
