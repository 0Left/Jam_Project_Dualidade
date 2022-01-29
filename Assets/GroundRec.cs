using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRec : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Ground")){
            gameObject.transform.parent.transform.gameObject.GetComponent<MainChar_Mov>().isOnGround();
        }
    }
}
