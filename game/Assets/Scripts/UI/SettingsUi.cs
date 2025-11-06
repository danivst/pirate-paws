using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUi : MonoBehaviour
{
    public static SettingsUi main;
    public List<GameObject> uiElements; // Items to enable/disable to open/close it

    public TMP_Dropdown qualityDropdown;
    public Slider masterVolume;
    public Slider musicVolume;

    public float EffectDuration = 0.05f;

    void Awake()
    {
        main = this;
    }

    void SetGameplay(bool active)
    {
   
        if (MovementManager.main == null)
        {
            return;
        }
        MovementManager.main.canMove = active;
        CameraManager.main.Enabled = active;
        JoyStick.main.Active = active;
    }
    public void OpenButton()
    {
        SetGameplay(false);
        foreach (GameObject obj in uiElements)
        {
            obj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            obj.SetActive(true);
            obj.transform.DOScale(new Vector3(1, 1, 1), EffectDuration);
        }
    }
    public void CloseButton()
    {
        SetGameplay(true);
        foreach (GameObject obj in uiElements)
        {
            obj.transform.DOScale(new Vector3(0, 0, 0), EffectDuration).onComplete = () => obj.SetActive(false);
        }
    }
    public void UpdateQuality(int level)
    {
        QualitySettings.SetQualityLevel(level);
    }
    public void UpdateMasterVolume(float level)
    {
        SoundManager.main.SetVolume((int)level);
    }
    public void UpdateMusicVolume(float level)
    {
        SoundManager.main.SetVolume(-1, (int)level);
    }
}
