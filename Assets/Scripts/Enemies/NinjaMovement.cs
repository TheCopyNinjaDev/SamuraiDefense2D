using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class NinjaMovement : MonoBehaviour 
    {

        [FormerlySerializedAs("movement_speed")] public float movementSpeed = 7f;
        [FormerlySerializedAs("move_dir")] public float moveDir = -1f;

        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("speed");


        private void Start() 
        {
            // Initialize animator
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            // Moving NPCs
            Move();
        }

        /// <summary>
        /// Moves NPCs to move direction
        /// </summary>
        private void Move()
        {
            var position = transform.position;
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.position = new Vector3(position.x + movementSpeed * moveDir, position.y);
            _animator.SetFloat(Speed, 1f);
        }
    }
}