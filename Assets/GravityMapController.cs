using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMapController : MonoBehaviour
{
    public void ChangeGravityOfBoxes(){
        GameObject[] Boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in Boxes)
        {
            box.GetComponent<Rigidbody2D>().gravityScale = (box.GetComponent<Rigidbody2D>().gravityScale * -1);
        }
    }
}
