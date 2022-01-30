using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRec : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Ground") || other.CompareTag("Box")){
            gameObject.transform.parent.transform.gameObject.GetComponent<MainChar_Mov>().notOnGround();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //if(other.CompareTag("Ground") || other.CompareTag("Box")){
            gameObject.transform.parent.transform.gameObject.GetComponent<MainChar_Mov>().isOnGround();
        //}
        if(other.CompareTag("Plataform")){
            gameObject.transform.parent.transform.parent = other.transform;
        }
    }

 
}
