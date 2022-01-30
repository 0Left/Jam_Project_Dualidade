using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAttach : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Plataform")){
        gameObject.transform.parent.transform.parent = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Plataform")){
        gameObject.transform.parent.transform.parent = null;
        }
    }
}
