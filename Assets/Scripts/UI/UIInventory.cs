using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIInventory : UIPopup
{
    public GameObject inventoryItemPrefab;
    public RectTransform itemGrid;
	private List<UIInvetoryItem> _items = new List<UIInvetoryItem>();

	public Button buttonSkin;
	public Button buttonEyes;
	public Button buttonHair;
	public Button buttonShirt;
	public Button buttonPants;
	public Button buttonShoes;

	private List<Button> _buttons = new List<Button>();


	private void Awake()
	{

	}

	public void Reset(string slot)
	{
		switch(slot)
		{
			case(Constants.skin):
				ShowSkin();
			break;
			case(Constants.eyes):
				ShowEyes();
			break;
			case(Constants.front_hair):
				ShowHair();
			break;
			case(Constants.pants):
				ShowPants();
			break;
			case(Constants.shirt):
				ShowShirts();
			break;
			case(Constants.shoes):
				ShowShoes();
			break;
		}
	}

	private void OnEnable()
	{
		if(_buttons.Count == 0)
		{
			_buttons.Add(buttonSkin);
			_buttons.Add(buttonEyes);
			_buttons.Add(buttonHair);
			_buttons.Add(buttonShirt);
			_buttons.Add(buttonPants);
			_buttons.Add(buttonShoes);
		}

		closeButton.gameObject.SetActive(true);
	}

	public void SetSellingMode()
	{
		closeButton.gameObject.SetActive(false);

		foreach(UIInvetoryItem current in _items)
		{
			current.SetupSellButton(()=>{
				InventoryManager.Instance.SellItem(current.itemSlot, current.inventoryIndex);
				Reset(current.itemSlot);
				_items.Remove(current);
			}, InventoryManager.Instance.GetSellingPrice(current.itemSlot, current.inventoryIndex));
		}
	}

	public void SetupButtons()
	{
		buttonSkin.onClick.AddListener(
			()=>{
				ShowSkin();
			}
		);

		buttonEyes.onClick.AddListener(
			()=>{
				ShowEyes();
			}
		);

		buttonHair.onClick.AddListener(
			()=>{
				ShowHair();
			}
		);

		buttonPants.onClick.AddListener(
			()=>{
				ShowPants();
			}
		);

		buttonShoes.onClick.AddListener(
			()=>{
				ShowShoes();
			}
		);

		buttonShirt.onClick.AddListener(
			()=>{
				buttonShirt.Select();
				PurgeItemCells();
				CreateItemCells(Constants.shirt);
			}
		);
	}

	public void CreateItemCells(string itemType)
    {
    	List<int> items = InventoryManager.Instance.playerItems[itemType];

    	for(int i = 0; i < items.Count; i ++)
    	{
    		Sprite[] sprites = ResourcesManager.Instance.LoadSkinIndex(itemType, items[i]);
    		Dictionary<string, Sprite> spritesheet = sprites.ToDictionary(x => x.name, x => x);

    		GameObject cell = GameObject.Instantiate(inventoryItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
    		UIInvetoryItem item = cell.GetComponent<UIInvetoryItem>();

    		item.inventory = this;
    		item.DisableSellButton();
    		item.SetSpriteSheet(spritesheet, items[i], itemType, i);

    		// item.productType = itemType;
      //       item.InitButtons();

    		_items.Add(item);
    		cell.transform.SetParent(itemGrid);
    	}

        StartCoroutine(Reposition());
    }

    public void PurgeItemCells()
    {
    	foreach(UIInvetoryItem current in _items)
    	{
    		GameObject.Destroy(current.gameObject);
    	}

    	_items = new List<UIInvetoryItem>();
    }
        
    public IEnumerator Reposition()
    {
        yield return new WaitForSeconds(0.05f);
        itemGrid.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 100f;
    }

    private void ShowSkin()
    {
    	buttonSkin.Select();
		PurgeItemCells();
		CreateItemCells(Constants.skin);
    }

    private void ShowEyes()
    {
    	buttonEyes.Select();
		PurgeItemCells();
		CreateItemCells(Constants.eyes);
    }

    private void ShowHair()
    {
    	buttonEyes.Select();
		PurgeItemCells();
		CreateItemCells(Constants.front_hair);
    }

    private void ShowShirts()
    {
    	buttonEyes.Select();
		PurgeItemCells();
		CreateItemCells(Constants.shirt);
    }

    private void ShowPants()
    {
    	buttonEyes.Select();
		PurgeItemCells();
		CreateItemCells(Constants.pants);
    }

    private void ShowShoes()
    {
    	buttonEyes.Select();
		PurgeItemCells();
		CreateItemCells(Constants.shoes);
    }
}
