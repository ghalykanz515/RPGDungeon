using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RPGDungeon.Economy
{
    public class CoinCollecting : MonoBehaviour
    {
        [SerializeField] private Text coinCountText;

        [HideInInspector]private int coinCount = 0;

        public void CollectCoin()
        {
            coinCount++;
            coinCountText.text = coinCount.ToString();
        }
    }
}