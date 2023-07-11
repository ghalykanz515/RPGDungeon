using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RPGDungeon.Economy
{
    public class CoinCollecting : MonoBehaviour
    {
        [SerializeField] private Text coinCountText;

        public int coinCount = 0;

        public GameObject claimPrompt;

        public void CollectCoin()
        {
            coinCount++;
            coinCountText.text = coinCount.ToString();
        }

        void Update()
        {
            if(coinCount == 3){
                claimPrompt.SetActive(true);
            }
        }
    }
}