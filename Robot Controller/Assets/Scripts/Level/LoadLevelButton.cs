using UnityEngine;

public class LoadLevelButton : MonoBehaviour
{
    public void Load() => LevelManager.Instance.LoadNextLevel();
}
