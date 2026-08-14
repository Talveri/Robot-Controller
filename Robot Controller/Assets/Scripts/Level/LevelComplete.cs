using UnityEngine;

class LevelComplete : MonoBehaviour
{
    [SerializeField] private LevelCompletePanel levelCompletePanel;

    void Start() => levelCompletePanel.gameObject.SetActive(false);

    void OnEnable()
    {
        ButtonEvents.OnGoalReached += ShowPanel;
        ButtonEvents.OnRestart     += HidePanel;
    }

    void OnDisable()
    {
        ButtonEvents.OnGoalReached -= ShowPanel;
        ButtonEvents.OnRestart     -= HidePanel;
    }

    private void ShowPanel()
    {
        levelCompletePanel.gameObject.SetActive(true);
        levelCompletePanel.showPanelRoutine();
    }
    private void HidePanel() => levelCompletePanel.gameObject.SetActive(false);
}
