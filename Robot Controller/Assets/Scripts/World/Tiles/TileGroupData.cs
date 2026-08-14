using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// The TileGroup Data is a scriptable Object, which contains all the tiles of a specific terrain type
/// </summary>
[CreateAssetMenu(menuName = "TileDatabase/TileGroupData")]
public class TileGroupData : ScriptableObject
{
    public TileBase[] tiles;
    public TerrainType terrainType;
}