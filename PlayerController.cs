using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Variables del movimiento del personaje
    public float jumpForce = 6f;
    public float runningSpeed = 2f;
    

    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 startPosition;
    SpriteRenderer spriteRenderer;
    
    
    

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    public LayerMask groundMask;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        
    }

    // Use this for initialization
    void Start () {

        startPosition = this.transform.position;
	}

    public void StartGame(){
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        Invoke("RestartPosition", 0.3f);
    }

    void RestartPosition(){
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Jump")){
            Jump();
            rigidBody.AddForce(Vector2.down * 1, ForceMode2D.Force);
            
            
        }

        
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); 
       
        
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
        rigidBody.velocity = new Vector2 (moveHorizontal * runningSpeed, rigidBody.velocity.y);
        rigidBody.AddForce(Vector2.down * 1, ForceMode2D.Force);

        }
        if(moveHorizontal == -1)
        {
        spriteRenderer.flipX = true;//gira el sprite al caminar
        }
        if(moveHorizontal == 1)
        {
        spriteRenderer.flipX = false;
        }
        if(GameManager.sharedInstance.currentGameState == GameState.menu){// si no estamos en la partida se para el personaje
            rigidBody.Sleep();//duerme el rigidbody
            animator.enabled = false;//duerme el animator
        }
           
        
    }

    void Jump()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (IsTouchingTheGround())
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                
            }
        }
    }

    bool IsTouchingTheGround(){
        if(Physics2D.Raycast(this.transform.position,
                             Vector2.down,
                             1.5f, 
                             groundMask)){
        return true;
        }
        else 
        {
            return false;
        }
    }


    public void Die(){
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }

}