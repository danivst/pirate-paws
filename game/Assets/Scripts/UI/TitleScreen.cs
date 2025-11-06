using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tutorialPanel;
    void Start()
    {
        if (!PlayerPrefs.HasKey("SavedData")){
            tutorialPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    public void CloseTutorial(){
        tutorialPanel.SetActive(false);
    }
    public void Play(){
        SceneManager.LoadScene(1);
    }
    public void OceanCleanup(){
        Application.OpenURL("https://theoceancleanup.com/");
    }
}
