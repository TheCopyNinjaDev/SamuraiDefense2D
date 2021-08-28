using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Players
{
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


        private void LateUpdate() 
        {
            //If character is fighting than he can't move
            if(!Fighting.IsFighting) 
            {
                AnimWalking();
                MoveInDirection(_moveDir);
                Flip();
            }
        }

        /// <summary>
        /// Sets needed direction
        /// </summary>
        /// <param name="direction">Needed direction</param>
        public void SetMoveDirection(float direction)
        {
            _moveDir = direction;
        } 

        /// <summary>
        /// Moves player to a direction
        /// </summary>
        /// <param name="direction">float - where players need to go</param>
        private void MoveInDirection(float direction)
        {
            _rb.velocity = new Vector2(direction * movementSpeed, gameObject.transform.position.y);
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
}
