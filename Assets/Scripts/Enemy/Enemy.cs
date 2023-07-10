using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGDungeon.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public float speed = 10;
        [SerializeField] public int health = 10;

        [SerializeField] public Transform player;

        [SerializeField] public float timeBetweenAttacks;
        [SerializeField] public int damage = 5;

        [SerializeField] public float playerInteractionRadius = 10f;
        [SerializeField] public float chaseInteractionRadius = 6f;

        [SerializeField] public GameObject reward;

        [HideInInspector] private bool facingRight = true;
        [HideInInspector] private float maxInteractionRadius;
        [HideInInspector] public Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();

            maxInteractionRadius = playerInteractionRadius;
        }

        private void FixedUpdate()
        {
            if (!IsPlayerNearby())
            {
                player = null;
                playerInteractionRadius = maxInteractionRadius;
            }
            else 
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                playerInteractionRadius = chaseInteractionRadius;
            }

            if (player == null)
            {
                anim.SetBool("EnemyRunning", false);
                return;
            }
            else 
            {
                anim.SetBool("EnemyRunning", true);
                Vector2 direction = player.position - transform.position;
                Debug.Log(direction.sqrMagnitude);

                if ((direction.x < 0 && facingRight) || (direction.x > 0 && !facingRight))
                {
                    PlayerFlip();
                }
            }
        }

        private void PlayerFlip()
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        public void TakeDamage(int damageAmount) 
        {
            health -= damageAmount;

            if (health <= 0) 
            {
                Instantiate(reward, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        public bool IsPlayerNearby() 
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerInteractionRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerInteractionRadius);
        }
    }
}
