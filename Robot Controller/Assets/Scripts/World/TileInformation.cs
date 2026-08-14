using UnityEngine;

public class TileInformation : MonoBehaviour
{
    public TerrainType terrainType = TerrainType.Floor;

    [SerializeField] private float gridSize = 1f;

    public Vector2Int GridPosition => new Vector2Int(
        Mathf.FloorToInt(transform.position.x / gridSize),
        Mathf.FloorToInt(transform.position.y / gridSize)
    );

    public bool IsPassable =>
        terrainType != TerrainType.Wall &&
        terrainType != TerrainType.Table;

    public float ExtraEnergyCost => terrainType switch
    {
        TerrainType.WetFloor => 5f,
        TerrainType.Goo      => 15f,
        _                    => 0f
    };
}
