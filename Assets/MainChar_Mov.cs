using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainChar_Mov : MonoBehaviour
{
    public float speed = 6000; 
    Vector2 move;
    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        //rb.AddForce(move * speed * Time.deltaTime, ForceMode2D.Force);
        //rb.velocity = move * speed * Time.deltaTime;
        //rb.MovePosition(rb.position + (move * speed * Time.deltaTime));
        rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
    }
    private bool Jump;

    void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //if (Input.GetKeyDown("space")){Jump = true;}else{Jump = false;}
    }
}
