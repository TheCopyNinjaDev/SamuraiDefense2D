using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{ 
    
    public class NinjaMovement : MonoBehaviour 
    {

        [FormerlySerializedAs("movement_speed")] public float movementSpeed = 7f;
        private int _moveDir;

        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("speed");


        private void Start() 
        {
            // Initialize animator
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            // Applies direction
            _moveDir = ChooseDirection();
            
            // Moving NPCs
            Move(_moveDir);
            
            // Rotates NPCs
            TurnToDirection(_moveDir);
        }

        /// <summary>
        /// Moves NPCs to move direction
        /// </summary>
        /// <param name="direction">direction he needs to go</param>
        private void Move(int direction)
        {
            var position = transform.position;
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.position = new Vector3(position.x + movementSpeed * direction, position.y);
            _animator.SetFloat(Speed, 1f);
        }

        /// <summary>
        /// Turns npc to a headed direction
        /// </summary>
        /// <param name="direction">The direction in which npc needs to be turned</param>
        private void TurnToDirection(int direction)
        {
            switch (direction)
            {
                case 1:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case -1:
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
            }
        }

        /// <summary>
        /// Defines in which direction npc needs to go
        /// </summary>
        /// <returns>The direction npc needs to go</returns>
        private int ChooseDirection()
        {
            return -Math.Sign(transform.position.x);
        }
    }
}