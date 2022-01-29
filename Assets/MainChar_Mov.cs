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
        if(StopAndChange){
            StopAndChange = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if(rb.velocity.x > -101 && rb.velocity.x < 101){
            if(canJump){
                rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
            }else{
                rb.velocity += new Vector2(move.x * (speed / 100) *Time.deltaTime, 0);
            }
        }
    }
    private bool GoingToRight;
    private bool StopAndChange;
    public bool canJump = true;
    public bool canDash = true;
    public float jumpVelocity = 300;
    public void isOnGround(){
        canJump = true;
        canDash = true;
        return;
    }
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") < 0 && GoingToRight){
            GoingToRight = false;
            GetComponent<SpriteRenderer>().flipX = true;
            StopAndChange = true;
        }else if (Input.GetAxisRaw("Horizontal") > 0 && !GoingToRight){
            GoingToRight = true;
            GetComponent<SpriteRenderer>().flipX = false;
            StopAndChange = true;
        }
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown("space") && canJump){
            canJump = false;
            rb.AddForce(Vector2.up*jumpVelocity, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown("z") && canDash){
            Vector2 SideToGo;
            canDash = false;
            if(GoingToRight){
                SideToGo = Vector2.right;
            }else{
                SideToGo = Vector2.left;
            }
            rb.AddForce(SideToGo*jumpVelocity*2, ForceMode2D.Impulse);
        }
    }
}
