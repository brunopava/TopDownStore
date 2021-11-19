using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHudFunds : MonoBehaviour
{
    public Text amountFunds;

    private void Update()
    {
    	amountFunds.text = Wallet.Instance.funds.ToString();
    }
}
