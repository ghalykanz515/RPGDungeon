using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using RPGDungeon.Economy;
using UnityEngine.SceneManagement;

public class TokenScript2 : MonoBehaviour
{
    public CoinCollecting coinCollectScript;

    public GameObject HasNotClaimedState;
    public GameObject ClaimingState;
    public GameObject HasClaimedState;

    public int coinToClaim;

    [SerializeField] public TMPro.TextMeshProUGUI coinEarnedText;

    [SerializeField] private TMPro.TextMeshProUGUI tokenBalanceText;

    private const string DROP_ERC20_CONTRACT = "0xd9A200fcc04606744C484405D4b340C2b025F24f";

    void Start()
    {
        HasNotClaimedState.SetActive(true);
        ClaimingState.SetActive(false);
        HasClaimedState.SetActive(false);
    }
    void Update()
    {
        coinEarnedText.text = "Coin Earned: " + coinCollectScript.coinCount.ToString();
        coinToClaim = coinCollectScript.coinCount;
    }
    public async void GetTokenBalance()
    {
        try
        {
            var address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
            var data = await contract.ERC20.BalanceOf(address);
            tokenBalanceText.text = "$GOLD: " + data.displayValue;
        }
        catch (System.Exception)
        {
            Debug.Log("Error getting token balance");
        }
    }
    public void ResetBalance()
    {
        tokenBalanceText.text = "$GOLD: 0";
    }

    public async void MintERC20()
    {
        try{
            Debug.Log("Minting ERC20");
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
            HasNotClaimedState.SetActive(false);
            ClaimingState.SetActive(true);
            var results = await contract.ERC20.Claim(coinToClaim.ToString());
            Debug.Log("ERC20 Minted");
            GetTokenBalance();
            ClaimingState.SetActive(false);
            HasClaimedState.SetActive(true);
        }catch{
            Debug.Log("Error minting ERC20");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
