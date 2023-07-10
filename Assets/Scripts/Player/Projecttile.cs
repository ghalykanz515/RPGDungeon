using EnemyType = RPGDungeon.Enemy.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGDungeon.Player 
{
    public class Projecttile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;

        [SerializeField] private GameObject explosion;

        [SerializeField] private int damage = 5;

        private void Start()
        {
            Invoke("DestroyProjectile", lifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        void DestroyProjectile() 
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyType>().TakeDamage(damage);
                DestroyProjectile();
            }
        }
    }
}
