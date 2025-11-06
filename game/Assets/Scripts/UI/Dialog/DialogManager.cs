using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public enum DialogType
{
    None = -1,
    TrashIsland = 0
}
[Serializable]
public struct DialogCameraTrans
{
    public DialogType DialogType;
    public List<Transform> transforms;
}

public class DialogManager : MonoBehaviour
{
    public static DialogManager main;

    Dictionary<DialogType, (string[], Action)> dialogs = new Dictionary<DialogType, (string[], Action)>() {
    {DialogType.TrashIsland, (new string[2] { "Hello traveler how ARE YOU!", "Done" }, () => RecycleUi.main.Open()) }
    };


    [Header("Settings")]
    public float textWriteInterval = 0.05f;

    [Header("Data")]
    public List<GameObject> gameObjectsDisable;
    public TMP_Text text;
    public GameObject parent;
    public RectTransform parentTransform;

    public List<DialogCameraTrans> cameraTransforms;

    [Header("Debug")]
    (string[], Action) dialogInfo;
    public int currentLine = -1;
    public DialogType currentDialog = DialogType.None;

    private void Awake()
    {
        main = this;
    }

    void CloseAnimation()
    {
        MovementManager.main.canMove = true;
        CameraManager.main.Enabled = true;

        foreach (var item in gameObjectsDisable)
        {
            item.SetActive(true);
        }

        parent.SetActive(false);
    }
    void OpenAnimation()
    {
        foreach (var item in gameObjectsDisable)
        {
            item.SetActive(false);
        }
        MovementManager.main.canMove = false;
        CameraManager.main.Enabled = false;
  
        parent.SetActive(true);
    }
    public void StartDialog(DialogType dialogType)
    {
        if (currentLine != -1)
        {
            return;
        }
        currentDialog = dialogType;
        currentLine = 0;
        dialogInfo = dialogs.GetValueOrDefault(dialogType);
        currentDialogCameraTrans = cameraTransforms.Find(pos => pos.DialogType == dialogType);
        UpdateText();
        OpenAnimation();
    }
    // Dialog Actions
    public void Cancel()
    {
        currentDialog = DialogType.None;
        currentLine = -1;
        currentText = null;
        currentChar = -1;
        CloseAnimation();
        
    }
    public void Next()
    {
        if (currentLine == -1)
        {
            return;
        }
        if (dialogInfo.Item1.Length - 1 == currentLine)
        {
            // End of dialog
            dialogInfo.Item2.Invoke();
            CloseAnimation();

            currentLine = -1;
            currentDialog = DialogType.None;
            return;
        }
        currentLine++;
        UpdateText();
    }
    //
   
    // Text Animation
    string currentText;
    DialogCameraTrans currentDialogCameraTrans;
    int currentChar = -1;
    float cooldown = 0f;

    public void UpdateText()
    {
        currentChar = 0;
        currentText = dialogInfo.Item1[currentLine];
        int index = currentLine;
        if (index >= currentDialogCameraTrans.transforms.Count)
        {
            index = currentDialogCameraTrans.transforms.Count - 1;
        }
        CameraManager.main.SetTransform(currentDialogCameraTrans.transforms[index], 1);
    }
    //

    void Update()
    {
        cooldown += Time.deltaTime;
        if (currentText != null && cooldown >= textWriteInterval && currentChar < currentText.Length + 1)
        {
            text.text = currentText[0..currentChar];

            currentChar++;
            cooldown = 0;
        }
    }
}
