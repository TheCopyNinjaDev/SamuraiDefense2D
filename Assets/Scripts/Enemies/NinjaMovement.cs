using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class NinjaMovement : MonoBehaviour 
    {

        [FormerlySerializedAs("movement_speed")] public float movementSpeed = 7f;
        [FormerlySerializedAs("move_dir")] public int moveDir = -1;

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
            moveDir = ChooseDirection();
            
            // Moving NPCs
            Move(moveDir);
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

        private void Rotate()
        {
            //TODO rotation relatively on direction
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