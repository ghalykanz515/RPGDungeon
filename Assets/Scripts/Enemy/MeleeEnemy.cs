using RPGDungeon.Player;
using System.Collections;
using UnityEngine;
using PlayerType = RPGDungeon.Player.Player;

namespace RPGDungeon.Enemy
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] public float stopDistance;

        [HideInInspector] private float attackTime;

        [SerializeField] public float attackSpeed;

        private void Update()
        {
            if (player != null)
            {
                if (Vector2.Distance(transform.position, player.position) > stopDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
                else
                {
                    if (Time.time >= attackTime)
                    {
                        attackTime = Time.time + timeBetweenAttacks;
                        StartCoroutine(Attack());
                    }
                }
            }
        }

        IEnumerator Attack()
        {
            anim.SetTrigger("EnemyAttack");
            player.GetComponent<PlayerType>().TakeDamage(damage);
            player.GetComponent<PlayerType>().anim.SetTrigger("isDamage");
            Debug.Log(player.GetComponent<PlayerType>().name);
            Vector2 originalPosition = transform.position;
            Vector2 targetPosition = player.position;

            float percent = 0f;
            while (percent <= 1)
            {
                percent += Time.deltaTime * attackSpeed;
                float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
                transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolation);
                yield return null;
            }
        }
    }
}