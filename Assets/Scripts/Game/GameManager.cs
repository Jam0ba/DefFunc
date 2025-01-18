using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 300;
    [SerializeField] private bool enableVSync = false;
    [SerializeField] private bool enableAntiAliasing = true;
    [SerializeField] private int shadowQuality = 2;
    [SerializeField] private float fixedDeltaTime = 0.02f;

    void Awake()
    {
        if (enableVSync)
        {
            QualitySettings.vSyncCount = 1; 
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate;
        }

        ApplyQualitySettings();

        Time.fixedDeltaTime = fixedDeltaTime;

    }

    private void ApplyQualitySettings()
    {
        QualitySettings.antiAliasing = enableAntiAliasing ? 4 : 0;

        switch (shadowQuality)
        {
            case 0:
                QualitySettings.shadows = ShadowQuality.Disable;
                break;
            case 1:
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
            case 2:
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
            case 3:
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
        }
    }
}
