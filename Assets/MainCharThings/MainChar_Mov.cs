using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainChar_Mov : MonoBehaviour
{
    public float speed = 3000; 
    Vector2 move;
    Rigidbody2D rb;
    private float fallMultiplier = 3.5f;
    private bool isGravityDown;

    private bool canChangeGravity = true;
    private bool toOtherSide;
    private bool GoingToRight;
    private bool StopAndChange;
    public bool isBackOnGround;
    public bool canJump = true;
    public bool canDash = true;
    public float jumpVelocity = 300;

    public float dashDistance = 15f;
    private bool isDashing;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        if(!isDashing){
            if(rb.gravityScale>0){
                isGravityDown = true;
                gameObject.transform.eulerAngles = new Vector3 (0,0,0);
            }else{
                isGravityDown = false;
                gameObject.transform.eulerAngles = new Vector3 (0,0,180);
            }
            if(StopAndChange){
                StopAndChange = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if(rb.velocity.x > -101 && rb.velocity.x < 101){
                if(canJump && !toOtherSide){
                    rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
                }else{
                    rb.velocity += new Vector2(move.x * (speed / 50) *Time.deltaTime, 0);
                }
            }
            if(!toOtherSide){
                if (rb.velocity.y < 0 && isGravityDown) {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
                if (rb.velocity.y > 0 && !isGravityDown) {
                    rb.velocity -= Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
            }
        }
        if(rb.velocity.x > 200){
            rb.velocity = new Vector2(200,rb.velocity.y);
        }
        if(rb.velocity.x < -200){
            rb.velocity = new Vector2(-200,rb.velocity.y);
        }
        
    }
    public void notOnGround(){
        transform.parent = null;
        isBackOnGround = false;
    }
    public void isOnGround(){
        canChangeGravity = true;
        isBackOnGround = true;
        canJump = true;
        canDash = true;
        if(toOtherSide){
            toOtherSide = false;
        }
        return;
    }
    public void Die(){
        GetComponent<SpriteRenderer>().enabled = false;
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
            if(isGravityDown){
                rb.AddForce(Vector2.up*jumpVelocity, ForceMode2D.Impulse);
            }else{
                rb.AddForce(Vector2.down*jumpVelocity, ForceMode2D.Impulse);
            }
        }
        if (Input.GetKeyDown("z") && canDash){
            Vector2 SideToGo;
            canDash = false;
            if(GoingToRight){
                SideToGo = Vector2.right;
            }else{
                SideToGo = Vector2.left;
            }
            StartCoroutine(dashRoutine(SideToGo));
            //rb.AddForce(SideToGo*jumpVelocity*2, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown("x") && canChangeGravity){
            canChangeGravity = false;
            rb.gravityScale = (rb.gravityScale * -1);
            GameObject.Find("GlobalGravityChange").GetComponent<GravityMapController>().ChangeGravityOfBoxes();
            toOtherSide = true;
            canJump = false;
        }
    }
    private IEnumerator dashRoutine(Vector2 SideToGo){
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x,0f);
        rb.AddForce(SideToGo*jumpVelocity*2, ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = gravity;
        yield return new WaitForSeconds(0.3f);
        if(isBackOnGround){
            canDash = true;
        }

    }




}
