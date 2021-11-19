using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInvetoryItem : MonoBehaviour
{
	public delegate void OnSell();

    public Image itemImage;
    public Button sellButton;
    public GameObject isEquiped;

    private UIInventory _inventory;
    public UIInventory inventory{
    	set {
    		_inventory = value;
    	}
    }

    private Hashtable _table;

    private int _inventoryItemIndex;
    public int inventoryIndex{
    	get {
    		return  _inventoryItemIndex;
    	}
    }

    private string _slot;
    public string itemSlot{
    	get {
    		return _slot;
    	}
    }


    private void Awake()
    {
    	isEquiped.SetActive(false);
    }

    public void SetSpriteSheet(Dictionary<string, Sprite> spritesheet, int itemIndex, string slot, int inventoryItemIndex)
    {
        Sprite current = spritesheet.First().Value;
    	itemImage.sprite = current;

    	_inventoryItemIndex = inventoryItemIndex;
    	_slot = slot;

    	_table = new Hashtable();
    	_table.Add("spritesheet", spritesheet);
        _table.Add("product_index", itemIndex);

    	if(InventoryManager.Instance.IsItemEquiped(slot, itemIndex))
    	{
    		isEquiped.SetActive(true);
    	}

    	GetComponent<Button>().onClick.AddListener(
    		()=>{
    			InventoryManager.Instance.EquipItem(slot, inventoryItemIndex);
    			ChangeSkin(slot);
    			_inventory.Reset(slot);
    		}
    	);
    }

    private void ChangeSkin(string slot)
    {
        switch(slot)
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

    public void DisableSellButton()
    {
    	sellButton.gameObject.SetActive(false);
    }

    public void SetupSellButton(OnSell onSell, int price)
    {
    	if(!isEquiped.activeSelf)
    	{
    		sellButton.GetComponentInChildren<Text>().text = "Sell for " +price.ToString();
	    	sellButton.gameObject.SetActive(true);
	    	sellButton.onClick.AddListener(
	    		()=>{
	    			onSell();
	    		}
	    	);
    	}
    }
}
