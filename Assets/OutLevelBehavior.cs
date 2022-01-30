using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLevelBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            //Ver se joga pra lá ou faz aqui mesmo
            other.transform.gameObject.GetComponent<MainChar_Mov>().NextLvl();
        }
    }
}
