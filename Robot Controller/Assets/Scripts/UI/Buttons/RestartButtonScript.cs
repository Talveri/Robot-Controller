using UnityEngine;

public class RestartButtonScript : MonoBehaviour
{
    public void RestartProcess() => ButtonEvents.OnRestart?.Invoke();
}
