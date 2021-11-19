using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : Singleton<Wallet>
{
	public int funds;

	private void Awake()
	{
		funds = 100;
	}

	public void AddFunds(int amount)
	{
		funds += amount;
	}

	public void RemoveFunds(int amount)
	{
		funds -= amount;
	}

	public bool HasFunds(int amountNecessary)
	{
		if(funds >= amountNecessary)
		{
			return true;
		}
		return false;
	}
}
