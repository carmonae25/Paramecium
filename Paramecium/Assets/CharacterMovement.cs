using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [Header("Character Attributes")]
    [SerializeField] float speed = 2.0f;
    private Rigidbody2D myRb;
    private float currentSize;
    private float sizeScale = 0.67f;

    public float healthPts = 10f;
    public Hazard hazard;
    public SpriteRenderer mySr;
    public AudioSource playerHitSound, lowHealthSound, deathSound;
    public Vector2 myInput;

    private bool canDash = true, isDashing;
    public bool isHit = false;
    [SerializeField] float dashMagnitude = 20.0f, dashTime = 0.1f;
    private float dashCooldown = 0.25f;

    private TrailRenderer myTrail;
    private CapsuleCollider2D myCollider;

    // private Camera playerView;

    // Start is called before the first frame update
    void Start()
    {        
        //Align child sprites at (0,0,0)
        foreach (Transform child in transform)
        {
            child.position = new Vector3(0,0,0);
        }

        mySr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerHitSound = transform.GetChild(1).GetComponent<AudioSource>();
        lowHealthSound = transform.GetChild(2).GetComponent<AudioSource>();
        lowHealthSound.volume = 1.2f;
        deathSound = transform.GetChild(3).GetComponent<AudioSource>();

        myRb = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TrailRenderer>();
        myCollider = GetComponent<CapsuleCollider2D>();
        currentSize = myCollider.size.x;
    }
    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Escape))
            {SceneManager.LoadScene("MainMenu"); }
            
        //prevent movement while dashing
        if(isDashing)
        {
            return;
        }      

        getPlayerInput();   //get (x,y) of player's current position
        getDash();          //dash on player input
    }
    void FixedUpdate() 
    {
        //prevent movement while dashing
        if(isDashing || isHit)
        {
            return;
        }    

        //move the player
        if(!isHit)
            movePlayer();
        //rotate the player accordingly
        if(myInput.x != 0 || myInput.y != 0)
            rotatePlayer(); 
        // adjustCamera();
    }
    private void getPlayerInput()
    {
        myInput.x = Input.GetAxisRaw("Horizontal");    //get the horizontal keyboard input
        myInput.y = Input.GetAxisRaw("Vertical");      //get the vertical keyboard input     
    }
    private void getDash()
    {
        //Dash if player hits [L-Shift], [R-Shift], or [spacebar] while moving
        //and is currently not in the middle of a dash
        if( (
            Input.GetKeyDown(KeyCode.RightShift) || 
            Input.GetKeyDown(KeyCode.LeftShift) ||
            Input.GetKeyDown(KeyCode.Space)
            ) 
            && canDash)
        {
            StartCoroutine(dash());
        }
    }
    private void movePlayer()
    {
        Vector3 VecDirection = new Vector3(myInput.x, myInput.y, 0);  //get the direction of movement
        myRb.velocity = VecDirection.normalized * speed;    //move accordingly
    }
    private void rotatePlayer()
    {
        float angle = Mathf.Atan2(myInput.y, myInput.x) * Mathf.Rad2Deg;  //get the angle of rotation based on movement direction
        if(!isHit)
            transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);   //rotate accordingly
    }
    private IEnumerator dash()
    {
        canDash = false;    //Player is unable to dash again while dashing
        isDashing = true;   //Player is currently dashing

        // decrease the size of the player's hitbox
        myCollider.size = new Vector2(currentSize * sizeScale, myCollider.size.y);

        //dash
        myRb.velocity = new Vector3(myInput.x, myInput.y, 0.0f).normalized * dashMagnitude;

        //emit trail while dashing for a little bit of time
        myTrail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        myTrail.emitting = false;

        isDashing = false;  //Player is no longer dashing

        // restore the size of the player's hitbox.
        myCollider.size = new Vector2(currentSize, myCollider.size.y);

        //Cooldown until player can dash again   
        yield return new WaitForSeconds(dashCooldown);     
   
        canDash = true;     //Player can dash once more
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(isDashing == true && other.tag == "enemy")
            Debug.Log("do damage");

        if(other.GetComponent<Hazard>() != null)
        {
            Debug.Log("take damage");        
          
            hazard = other.GetComponent<Hazard>();
            StartCoroutine(takeDamage());

            if(healthPts > 0)
                playerHitSound.Play();
            else{
                deathSound.Play();
            }

            if(healthPts == 5f)
               lowHealthSound.Play();
        }

        if(other.tag == "genetic code")
        {
            Debug.Log("You win!");
            SceneManager.LoadScene("MainMenu");
        }
    }

    private IEnumerator takeDamage(){
        
        //Stops player input when true. In other words, the player is stunned.
        isHit = true;  

        // decrease helth
        healthPts -= hazard.damage;

        //Knockback
        myRb.velocity = new Vector3(-myInput.x, -myInput.y, 0f).normalized * 10f;

        yield return new WaitForSeconds(0.5f);

        isHit = false;
    }

}
