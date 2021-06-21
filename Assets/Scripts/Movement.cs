using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour 
{
    #region public
        [FormerlySerializedAs("movement_speed")] public float movementSpeed = 7f;
    #endregion

    private float _moveDir;
    private bool _pFacingRight = true;
    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("speed");

    private void Start() 
    {
        //Initialization of components
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    private void Update() 
    {
        //If character is fighting than he can't move
        if(!Fighting.IsFighting) 
        {
            _moveDir = Input.GetAxisRaw("Horizontal"); //Gives the variable a direction of x
        }
        else _moveDir = 0; //If he's fighting than he stops
        
        AnimWalking();
        Flip();
    }

    private void FixedUpdate() 
    {
        //Gives a character a velocity
        _rb.velocity = new Vector2(_moveDir * movementSpeed, gameObject.transform.position.y);
    } 

    /// <summary>
    /// Flips a character to look left or right
    /// </summary>
    private void Flip()
    {

        if (_moveDir > 0 && !_pFacingRight)
        {
            _pFacingRight = !_pFacingRight;
            var theScale = transform.localScale;
		    theScale.x *= -1;
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.localScale = theScale;
            
        }
        else if (_moveDir < 0 && _pFacingRight)
        {
            _pFacingRight = !_pFacingRight;
            var theScale = transform.localScale;
		    theScale.x *= -1;
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.localScale = theScale;
        }
    }

    /// <summary>
    /// Animates a character when he's walking 
    /// </summary>
    private void AnimWalking()
    {
        float velocity;
        if(Math.Abs(_moveDir - 1) < 0.01f || Math.Abs(_moveDir + 1) < 0.01f) velocity = 1;
        else velocity = 0;
        _animator.SetFloat(Speed, velocity);
    }

}
