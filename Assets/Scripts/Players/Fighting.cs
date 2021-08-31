using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Players
{
    public class Fighting : MonoBehaviour
    {

        #region public_references
        [FormerlySerializedAs("UpperHit")] public AnimationClip upperHit;
        [FormerlySerializedAs("DownHit")] public AnimationClip downHit;

        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyUpGuardLayer;
        [FormerlySerializedAs("enemyDownGurdLayer")] public LayerMask enemyDownGuardLayer;

        public GameObject smoke;

        [FormerlySerializedAs("sword_cluck")] public AudioClip swordCluck;

        [FormerlySerializedAs("sword_col")] public ParticleSystem swordCol;

        public static bool IsFighting;
        #endregion

        private Animator _animator;
        private AudioSource _audioSource;

        private void Start() 
        {
            // Initialization
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            Attack(default);
        }

        /// <summary>
        /// Attacks depending on input
        /// </summary>
        /// <param name="AttackType">Type of attack needs to be perform</param>
        public void Attack(string AttackType)
        {
            switch (AttackType)
            {
                case "down":
                    IsFighting = true;
                    StartCoroutine(PlayAnim(downHit, "DownHitButtonClicked"));
                    AttackDown();
                    break;
                case "up":
                    IsFighting = true;
                    StartCoroutine(PlayAnim(upperHit, "UpperHitButtonClicked"));
                    AttackUp();
                    break;
                default:
                    IsFighting = false;
                    break;
            }
        }

        /// <summary>
        /// Plays attack animation with cooldown
        /// </summary>
        /// <param name="anim">Currently playing animation</param>
        /// <param name="trig">Trigger for attack</param>
        /// <returns></returns>
        private IEnumerator PlayAnim(AnimationClip anim, string trig)
        {
            _animator.SetBool(trig, true);
            yield return new WaitForSeconds(anim.length);
            _animator.SetBool(trig, false);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Attacks from above
        /// </summary>
        private void AttackUp()
        {
            // ReSharper disable once Unity.PreferNonAllocApi
            var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, 
                enemyDownGuardLayer);

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
        /// <summary>
        /// Attacks from bottom
        /// </summary>
        private void AttackDown()
        {
            // ReSharper disable once Unity.PreferNonAllocApi
            var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, 
                enemyUpGuardLayer);

            foreach(var enemy in hitEnemies)
            {
                Destroy(enemy.gameObject, 0.3f);
                Instantiate(smoke, enemy.transform.position, Quaternion.identity);
            }

            // ReSharper disable once Unity.PreferNonAllocApi
            var nHitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, 
                enemyDownGuardLayer);
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
}


