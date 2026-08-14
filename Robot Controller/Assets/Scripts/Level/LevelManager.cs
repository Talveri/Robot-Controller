using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private LevelDataSO levelData;

    /// If the start screen is implemented in a seperate scene, this member should be initialized
    /// with their designated enum
    private Level currentLevel = Level.Start;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        transform.SetParent(null);
        currentLevel = Array.Find(levelData.levels, e => e.sceneName == SceneManager.GetActiveScene().name).level;
        DontDestroyOnLoad(gameObject);
        
    }

    public void LoadNextLevel()
    {
        currentLevel = (Level)((int)(currentLevel + 1) % (int) (Level.End+1));
        LoadLevel(currentLevel);
    }
    private void LoadLevel(Level level)
    {
        LevelDataSO.LevelEntry entry = Array.Find(levelData.levels, e => e.level == level);
        SceneManager.LoadScene(entry.sceneName);
    }
}
