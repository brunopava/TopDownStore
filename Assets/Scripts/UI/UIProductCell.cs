using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIProductCell : MonoBehaviour
{
    public Image productImage;

    public Text pricetag;
    public Button buyButton;
    public Button tryButton;

    private Hashtable _table;
    private Sprite _currentProduct;

    private string _productType;
    public string productType
    {
    	set{
    		_productType = value;
    	}
    }

    private int _productPrice;

    public void SetSpriteSheet(Dictionary<string, Sprite> spritesheet, int productIndex)
    {
        _currentProduct = spritesheet.First().Value;
    	productImage.sprite = _currentProduct;
        _productPrice = productIndex*10;
        pricetag.text = _productPrice.ToString();

    	_table = new Hashtable();
    	_table.Add("spritesheet", spritesheet);
        _table.Add("product_index", productIndex);
    }

    public void InitButtons()
    {
        tryButton.onClick.AddListener(()=>{
            TryProductOn();
        });

        buyButton.onClick.AddListener(
            ()=>{
                UIPopupConfirmation popup = UIPopupLayer.Instance.GetPopupByType<UIPopupConfirmation>();
                popup.gameObject.SetActive(true);

                popup.SetImage(_currentProduct);
                popup.SetPopupTitle("Buy this product?");
                popup.SetButtonsText("Buy", "Nevermind");
                popup.SetPriceTag(_productPrice);
                popup.Initialize(
                    null,
                    ()=>{
                        if(Wallet.Instance.HasFunds(_productPrice))
                        {
                            Wallet.Instance.RemoveFunds(_productPrice);
                            _table.Add("purshased", true);
                            TryProductOn();
                            popup.gameObject.SetActive(false);
                        }else{
                            UIPopupAlert alert = UIPopupLayer.Instance.GetPopupByType<UIPopupAlert>();
                            alert.gameObject.SetActive(true);
                            alert.SetPopupTitle("You have no funds left!");
                            alert.Initialize(()=>{
                                alert.gameObject.SetActive(false);
                                popup.gameObject.SetActive(false);
                            });
                        }
                    },
                    ()=>{
                        popup.gameObject.SetActive(false);
                    }
                );
            }
        );
    }

    private void TryProductOn()
    {
        switch(_productType)
        {
            case(Constants.shirt):
                NotificationCenter.DefaultCenter.PostNotification(this, "ChangeShirt", _table);
            break;
            case(Constants.pants):
                NotificationCenter.DefaultCenter.PostNotification(this, "ChangePants", _table);
            break;
            case(Constants.shoes):
                NotificationCenter.DefaultCenter.PostNotification(this, "ChangeShoes", _table);
            break;
            case(Constants.front_hair):
                NotificationCenter.DefaultCenter.PostNotification(this, "ChangeHair", _table);
            break;
            case(Constants.skin):
                NotificationCenter.DefaultCenter.PostNotification(this, "ChangeSkin", _table);
            break;
            case(Constants.eyes):
                NotificationCenter.DefaultCenter.PostNotification(this, "ChangeEyes", _table);
            break;
        }
    }
}
