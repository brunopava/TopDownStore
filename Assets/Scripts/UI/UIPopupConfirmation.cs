using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupConfirmation : UIPopup
{
	public Image productImage;
	public Text pricetag;

	public void SetImage(Sprite product)
	{
		productImage.sprite = product;
	}

	public void SetButtonsText(string confirmText, string cancelText)
	{
		confirmButton.GetComponentInChildren<Text>().text = confirmText;
		cancelButton.GetComponentInChildren<Text>().text = cancelText;
	}

	public void SetPriceTag(int amount)
	{
		pricetag.text = amount.ToString();
	}
}
