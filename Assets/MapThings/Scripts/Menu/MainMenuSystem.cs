using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    public GameObject CreditsObject;
    void Start()
    {
        HideCredits();
    }
    public void HideCredits(){
        CreditsObject.SetActive(false);
    }
    public void ShowCredits(){
        CreditsObject.SetActive(true);
    }
    public void StartANewGame(){
        SceneManager.LoadScene("Lvl_1");
    }
    public void QuitGame(){
        Application.Quit();
    }
}
