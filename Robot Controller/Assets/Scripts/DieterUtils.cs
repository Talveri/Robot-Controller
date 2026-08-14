using System;
using System.Collections;
using UnityEngine;

public class DieterUtils : MonoBehaviour
{
    [SerializeField] private string appName;
    public static DieterUtils Instance { get; private set; }

    private TMPro.TMP_Text countdownText = null;
    private int remainingTime = 0;
    private int currentDisplayedTime = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Application.deepLinkActivated += OnDeepLinkActivated;

            if (!string.IsNullOrEmpty(Application.absoluteURL))
                OnDeepLinkActivated(Application.absoluteURL);

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Sets a text element for displaying the remaining time
    public void SetTextElement(TMPro.TMP_Text textElement)
    {
        countdownText = textElement;
        if (remainingTime > 0)
            countdownText.text = remainingTime.ToString() + "min";
    }

    // Switches to the master app and closes this app
    // Call with true if puzzle was completed successfully, false if not
    public void SwitchToMasterApp(bool success)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string deepLinkURL = "dieterMasterApp" + "://" + "startLink" + "?" + appName + "-" + success;
        Application.OpenURL(deepLinkURL);
#endif
        Application.Quit();
    }

    private void OnDeepLinkActivated(string url)
    {
        string parameter = url.Split('?')[1];
        string appName = parameter.Split('-')[0];
        string time = parameter.Split('-')[1];

        if (parameter == null || appName != "DieterMasterApp")
            return;

        try
        {
            remainingTime = Int32.Parse(time);
            StartCoroutine(Countdown(remainingTime));
        }
        catch
        {
            Debug.LogError("Failed to read escape room time");
        }
    }

    private IEnumerator Countdown(int time)
    {
        float timer = time * 60f;

        while (timer > 0)
        {
            timer -= 1f;
            int currentTime = (int)(timer / 60f) + 1;
            remainingTime = currentTime < 0 ? 0 : currentTime;

            if (remainingTime != currentDisplayedTime)
            {
                if (countdownText != null)
                    countdownText.text = remainingTime.ToString() + " min";

                currentDisplayedTime = remainingTime;
            }

            yield return new WaitForSecondsRealtime(1f);
        }

        SwitchToMasterApp(false);
    }
}