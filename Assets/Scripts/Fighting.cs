using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Fighting : MonoBehaviour
{

    #region public_references
        [FormerlySerializedAs("UpperHit")] public AnimationClip upperHit;
        [FormerlySerializedAs("DownHit")] public AnimationClip downHit;

        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyUpGuardLayer;
        public LayerMask enemyDownGurdLayer;

        public GameObject smoke;

        [FormerlySerializedAs("sword_cluck")] public AudioClip swordCluck;

        [FormerlySerializedAs("sword_col")] public ParticleSystem swordCol;

        public static bool IsFighting;
    #endregion

    private Animator _animator;
    private AudioSource _audioSource;

    private void Start() 
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(PlayAnim(downHit, "DownHitButtonClicked"));
            AttackDown();
            IsFighting = true;
        }
        if(Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(PlayAnim(upperHit, "UpperHitButtonClicked"));
            AttackUp();
            IsFighting = true;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            IsFighting = false;
        }
        if(Input.GetButtonUp("Fire2"))
        {
            IsFighting = false;
        }
    }

    IEnumerator PlayAnim(AnimationClip anim, string trig)
    {
        _animator.SetBool(trig, true);
        yield return new WaitForSeconds(anim.length);
        _animator.SetBool(trig, false);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void AttackUp()
    {
        // ReSharper disable once Unity.PreferNonAllocApi
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, 
            enemyDownGurdLayer);

        foreach(var enemy in hitEnemies)
        {
            Destroy(enemy.gameObject, 0.2f);
            Instantiate(smoke, enemy.transform.position, Quaternion.identity);
        }

        // ReSharper disable once Unity.PreferNonAllocApi
        var nHitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,
            enemyUpGuardLayer);
        foreach(var _ in nHitEnemies)
        {
            _audioSource.clip = swordCluck;
            _audioSource.Play();
            var attackPointTransform = attackPoint.transform;
            var attackPosition = attackPointTransform.position;
            var blockPos = new Vector3(attackPosition.x, attackPosition.y + 1f, attackPosition.z);

            Instantiate(swordCol, blockPos, Quaternion.identity);
            Destroy(GameObject.Find("Sword_col(Clone)"), 0.2f);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void AttackDown()
    {
        // ReSharper disable once Unity.PreferNonAllocApi
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, 
            enemyUpGuardLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            Destroy(enemy.gameObject, 0.3f);
            Instantiate(smoke, enemy.transform.position, Quaternion.identity);
        }

        // ReSharper disable once Unity.PreferNonAllocApi
        var nHitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, 
            enemyDownGurdLayer);
        foreach(var _ in nHitEnemies)
        {
            _audioSource.clip = swordCluck;
            _audioSource.Play();
            Instantiate(swordCol, attackPoint.position, Quaternion.identity);
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


