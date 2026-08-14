using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData")]
public class LevelDataSO : ScriptableObject
{
    [System.Serializable]
    public struct LevelEntry
    {
        public Level level;
        public string sceneName;
    }

    public LevelEntry[] levels;
}
