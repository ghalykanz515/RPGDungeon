using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RPGDungeon.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public float speed = 5f;

        [SerializeField] public float maxHealth = 100;
        [SerializeField] public float currentHealth = 100;

        [SerializeField] private Image healthFill;

        [HideInInspector] private Rigidbody2D rb;
        [HideInInspector] public Animator anim;
        [HideInInspector] private Vector2 moveAmount;

        [HideInInspector] private bool facingRight = true;

        [HideInInspector] private float maxFillAmount;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            maxFillAmount = healthFill.fillAmount;
        }

        private void Update()
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveAmount = moveInput.normalized * speed;

            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isRunning", true);
            }
            else 
            {
                anim.SetBool("isRunning", false);
            }
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveAmount * Time.deltaTime);

            if ((moveAmount.x < 0 && facingRight) || (moveAmount.x > 0 && !facingRight)) 
            {
                PlayerFlip();
            }
        }

        private void PlayerFlip() 
        {
            facingRight = !facingRight;

            Vector3 scale = rb.transform.localScale;
            scale.x *= -1;
            rb.transform.localScale = scale;
        }

        private void UpdateHealth(float currentHealth, float maxHealth) 
        {
            float fillAmount = currentHealth / maxHealth;
            healthFill.fillAmount = fillAmount * maxFillAmount;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealth(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}