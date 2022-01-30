using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSideToSidePlataform : MonoBehaviour
{

    public float TimeToGoSideBySide = 1f;
    private void Start() {
        moveSpeed = moveSpeed * -1;
        StartCoroutine(RepeatMe());
    }
    public float moveSpeed = 1f;
    private void FixedUpdate() {
        transform.position += new Vector3(moveSpeed, 0,0);
    }

    IEnumerator RepeatMe(){
        moveSpeed = moveSpeed * -1;
        Debug.Log("DoME" + moveSpeed);
        yield return new WaitForSeconds(TimeToGoSideBySide);
        StartCoroutine(RepeatMe());
        
    }
}
