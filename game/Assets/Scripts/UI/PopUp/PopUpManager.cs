using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager main;

    public GameObject Window;


    public TMP_Text Message;

    public void Awake()
    {
        if (main != null)
        {
            return;
        }
        main = this;
    }
    public void Display(string message)
    {
        Message.text = message;
        GameObject clone = Instantiate(Window,Window.transform.parent);
        clone.SetActive(true);
        
    }

}
