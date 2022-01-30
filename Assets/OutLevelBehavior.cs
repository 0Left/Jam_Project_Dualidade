using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutLevelBehavior : MonoBehaviour
{
    public string nextLevel;
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player") && other.transform.gameObject.GetComponent<MainChar_Mov>().isMeBackOnGround()){
            //Ver se joga pra lá ou faz aqui mesmo
            if(other.transform.gameObject != null){
                Debug.Log("Next level is: " + nextLevel);
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
