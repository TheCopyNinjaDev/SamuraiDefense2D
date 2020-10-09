using System.Collections;
using UnityEngine;

public class Fighting : MonoBehaviour
{

    #region public_references
        public AnimationClip UpperHit;
        public AnimationClip DownHit;

        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyUpGuardLayer;
        public LayerMask enemyDownGurdLayer;

        public GameObject smoke;

        public AudioClip sword_cluck;

        public ParticleSystem sword_col;

        public static bool is_fighting = false;
    #endregion

    Animator animator;
    AudioSource audioSource;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(PlayAnim(DownHit, "DownHitButtonClicked"));
            AttackDown();
            is_fighting = true;
        }
        if(Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(PlayAnim(UpperHit, "UpperHitButtonClicked"));
            AttackUp();
            is_fighting = true;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            is_fighting = false;
        }
        if(Input.GetButtonUp("Fire2"))
        {
            is_fighting = false;
        }
    }

    IEnumerator PlayAnim(AnimationClip anim, string trig)
    {
        animator.SetBool(trig, true);
        yield return new WaitForSeconds(anim.length);
        animator.SetBool(trig, false);
    }

    void AttackUp()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyDownGurdLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            Destroy(enemy.gameObject, 0.2f);
            Instantiate(smoke, enemy.transform.position, Quaternion.identity);
        }

        Collider2D[] n_hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyUpGuardLayer);
        foreach(Collider2D enemy in n_hitEnemies)
        {
            audioSource.clip = sword_cluck;
            audioSource.Play();
            Vector3 block_pos = new Vector3(attackPoint.transform.position.x, attackPoint.transform.position.y + 1f, attackPoint.transform.position.z);

            Instantiate(sword_col, block_pos, Quaternion.identity);
            Destroy(GameObject.Find("Sword_col(Clone)"), 0.2f);
        }
    }

    void AttackDown()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyUpGuardLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            Destroy(enemy.gameObject, 0.3f);
            Instantiate(smoke, enemy.transform.position, Quaternion.identity);
        }

        Collider2D[] n_hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyDownGurdLayer);
        foreach(Collider2D enemy in n_hitEnemies)
        {
            audioSource.clip = sword_cluck;
            audioSource.Play();
            Instantiate(sword_col, attackPoint.position, Quaternion.identity);
            Destroy(GameObject.Find("Sword_col(Clone)"), 0.2f);
        }

    }

    private void OnDrawGizmosSelected() 
    {
        if(attackPoint == null) 
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}


