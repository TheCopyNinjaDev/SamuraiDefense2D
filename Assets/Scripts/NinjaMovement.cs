using UnityEngine;

public class NinjaMovement : MonoBehaviour 
{

    public float movement_speed = 7f;
    public float move_dir = -1f;

    Animator animator;


    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    } 

    void Move()
    {
        transform.position = new Vector3(transform.position.x + movement_speed * move_dir, transform.position.y);
        animator.SetFloat("speed", 1f);
    }
}