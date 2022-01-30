using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBehavior : MonoBehaviour
{
    bool isPaused;
    public GameObject PauseMenu;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                PauseMenu.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
            }else{
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;

            }
        }
    }
    public void RestartLevel(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }
    public void QuitGame(){
        Application.Quit();
    }
}
