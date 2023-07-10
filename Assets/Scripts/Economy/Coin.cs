using System.Collections;
using UnityEngine;

namespace RPGDungeon.Economy
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                CoinCollecting coinCollector = other.GetComponent<CoinCollecting>();
                if (coinCollector != null)
                {
                    coinCollector.CollectCoin();
                    Destroy(gameObject);
                }
            }
        }
    }
}