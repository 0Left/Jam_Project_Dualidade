using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAgain());
    }
    IEnumerator StartAgain(){
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("Main_Menu");
    }
}
