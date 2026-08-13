using UnityEngine;
using TMPro;

public class PerformanceOverlay : MonoBehaviour
{
    public TMP_Text fpsText;
    private float deltaTime;
    private float updateInterval = 0.5f;
    private float timeSinceUpdate;

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        timeSinceUpdate += Time.unscaledDeltaTime;

        if (timeSinceUpdate >= updateInterval)
        {
            float fps = 1.0f / deltaTime;
            fpsText.text = $"FPS: {Mathf.RoundToInt(fps)}";
            timeSinceUpdate = 0f;
        }
    }
}
