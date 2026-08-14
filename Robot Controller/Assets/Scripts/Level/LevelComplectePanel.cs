using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Level Complete Panel Script handles the popup which appears when the level is complete.
/// The Main Panel of the Level Complete Panel structure zooms in, when the showPanelRoutine() is called
/// </summary>
public class LevelCompletePanel : MonoBehaviour
{
    public Image Background;
    public Image MainPanel;

    public void Awake()
    {
        MainPanel.gameObject.SetActive(false);
    }
    public void showPanelRoutine()
    {
        Debug.Log($"showPanelRoutine — Background: {Background}, MainPanel: {MainPanel}");
        StartCoroutine(ShowPanel());
    }

    private IEnumerator ShowPanel()
    {
        Debug.Log("ShowPanel coroutine started");

        // Fade in background
        Background.color = new Color(0f, 0f, 0f, 0f);
        float duration = 0.4f;
        for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
        {
            float alpha = t / duration;
            Background.color = new Color(0f, 0f, 0f, alpha * 0.6f);
            yield return null;
        }
        Background.color = new Color(0f, 0f, 0f, 0.6f);

        // Pop in main panel
        MainPanel.gameObject.SetActive(true);
        MainPanel.transform.localScale = Vector3.zero;
        duration = 0.25f;
        for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
        {
            float scale = Mathf.SmoothStep(0f, 1f, t / duration);
            MainPanel.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        MainPanel.transform.localScale = Vector3.one;
    }
}
