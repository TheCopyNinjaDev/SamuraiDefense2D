using UnityEngine;

public class Movement : MonoBehaviour 
{
    #region public
        public float movement_speed = 7f;
    #endregion

    float move_dir = 0f;
    bool p_FacingRight = true;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Start() 
    {
        //Initialization of components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    private void Update() 
    {
        if(!Fighting.is_fighting) //If character is fighting than he can't move
        {
            move_dir = Input.GetAxisRaw("Horizontal"); //Gives the variable a direction of x
        }
        else move_dir = 0; //If he's fighting than he stops
        
        AnimWalking();
        Flipment();
    }

    private void FixedUpdate() 
    {
        //Gives a character a velocity
        rb.velocity = new Vector2(move_dir * movement_speed, gameObject.transform.position.y);
    } 

    //Flips a character to look left or right
    void Flipment()
    {
        if (move_dir > 0 && !p_FacingRight)
        {
            p_FacingRight = !p_FacingRight;
            Vector3 theScale = transform.localScale;
		    theScale.x *= -1;
		    transform.localScale = theScale;
            
        }
        else if (move_dir < 0 && p_FacingRight)
        {
            p_FacingRight = !p_FacingRight;
            Vector3 theScale = transform.localScale;
		    theScale.x *= -1;
		    transform.localScale = theScale;
        }
    }

    //Animates a character when he's walking 
    void AnimWalking()
    {
        float velocity = 0;
        if(move_dir == 1 || move_dir == -1) velocity = 1;
        else velocity = 0;
        animator.SetFloat("speed", velocity);
    }

}
