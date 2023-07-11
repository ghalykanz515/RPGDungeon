using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGDungeon.Player;

public class ConnectWalletScript2 : MonoBehaviour
{
    public GameObject claimPrompt;
    public GameObject connectPrompt;
    public Player playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        claimPrompt.SetActive(false);
        connectPrompt.SetActive(true);
        playerMovementScript.speed = 0f;
    }

    // void Update(){}
    // Update is called once per frame
    public void ConnectWallet()
    {
        connectPrompt.SetActive(false);
        playerMovementScript.speed = 5f;
    }

    public void showConnectPrompt()
    {
        connectPrompt.SetActive(true);
        playerMovementScript.speed = 0f;
    }
}
