using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class MainChar_Mov : MonoBehaviour
{
    public float speed = 3000; 
    Vector2 move;
    Rigidbody2D rb;
    private float fallMultiplier = 2.5f;
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
            if(!toOtherSide && !canJump){
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
        //particulinha.SetActive(true);
        if(toOtherSide){
            toOtherSide = false;
        }
        return;
    }
    public void Die(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public bool isMeBackOnGround(){
        return isBackOnGround;
    }
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") < 0 ){
            if(isGravityDown){GetComponent<SpriteRenderer>().flipX = true;}else{GetComponent<SpriteRenderer>().flipX = false;}
            if(GoingToRight){
                GoingToRight = false;
                StopAndChange = true;
            }
        }else if (Input.GetAxisRaw("Horizontal") > 0 ){
            if(isGravityDown){GetComponent<SpriteRenderer>().flipX = false;}else{GetComponent<SpriteRenderer>().flipX = true;}
            if(!GoingToRight){
                GoingToRight = true;
                StopAndChange = true;
            }
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
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = (rb.gravityScale * -1);
            GameObject.Find("GlobalGravityChange").GetComponent<GravityMapController>().ChangeGravityOfBoxes();
            if(!GetComponent<SpriteRenderer>().flipX){GetComponent<SpriteRenderer>().flipX = true;}else{GetComponent<SpriteRenderer>().flipX = false;}
            toOtherSide = true;
            canJump = false;
        }
    }
    //public GameObject particulinha;
    private IEnumerator dashRoutine(Vector2 SideToGo){
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x,0f);
        rb.AddForce(SideToGo*dashDistance*2, ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.2f);
        //particulinha.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(0f,0f);
        isDashing = false;
        rb.gravityScale = gravity;
        yield return new WaitForSeconds(0.3f);
        if(isBackOnGround){
            canDash = true;
            //particulinha.SetActive(true);
        }

    }




}
