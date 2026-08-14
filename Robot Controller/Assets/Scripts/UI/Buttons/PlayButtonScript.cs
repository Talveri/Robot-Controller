using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    void OnEnable()  => ButtonEvents.OnRestart += Activate;
    void OnDisable() => ButtonEvents.OnRestart -= Activate;

    public void StartProcess()
    {
        GetComponent<Button>().interactable = false;
        ButtonEvents.OnPlay?.Invoke();
    }

    private void Activate() => GetComponent<Button>().interactable = true;
}
