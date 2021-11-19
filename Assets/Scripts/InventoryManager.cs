using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Dictionary<string, List<int>> playerItems;

    //These are the index of the spritesheets rather than the index of playerItems
    public int equipedSkinIndex = 1;
    public int equipedHairIndex = 1;
    public int equipedEyesIndex = 1;
    public int equipedShirtIndex = 1;
    public int equipedPantsIndex = 1;
    public int equipedShoesIndex = 1;

    private void Awake()
    {
    	playerItems = new Dictionary<string, List<int>>();

    	playerItems.Add(Slots.skin.ToString(), new List<int>());
    	playerItems.Add(Slots.front_hair.ToString(), new List<int>());
    	playerItems.Add(Slots.eyes.ToString(), new List<int>());
    	playerItems.Add(Slots.shirt.ToString(), new List<int>());
    	playerItems.Add(Slots.pants.ToString(), new List<int>());
    	playerItems.Add(Slots.shoes.ToString(), new List<int>());

    	playerItems[Slots.skin.ToString()].Add(equipedSkinIndex);
    	playerItems[Slots.front_hair.ToString()].Add(equipedHairIndex);
    	playerItems[Slots.eyes.ToString()].Add(equipedEyesIndex);
    	playerItems[Slots.shirt.ToString()].Add(equipedShirtIndex);
    	playerItems[Slots.pants.ToString()].Add(equipedPantsIndex);
    	playerItems[Slots.shoes.ToString()].Add(equipedShoesIndex);
    }

    public void AddItem(string slot, int spriteIndex)
    {
    	playerItems[slot].Add(spriteIndex);
    	EquipItem(slot, playerItems[slot].Count-1);
    }

    public void SellItem(string slot, int index)
    {
    	Wallet.Instance.AddFunds(GetSellingPrice(slot,index));
    	playerItems[slot].RemoveAt(index);
    }

    public int GetSellingPrice(string slot, int index)
    {
    	return playerItems[slot][index]*10;
    }

    public void EquipItem(string slot, int itemIndex)
    {
    	switch(slot)
    	{
    		case(Constants.skin):
    			equipedSkinIndex = playerItems[slot][itemIndex];
    		break;
    		case(Constants.front_hair):
    			equipedHairIndex = playerItems[slot][itemIndex];
    		break;
    		case(Constants.eyes):
    			equipedEyesIndex = playerItems[slot][itemIndex];
    		break;
    		case(Constants.shirt):
    			equipedShirtIndex = playerItems[slot][itemIndex];
    		break;
    		case(Constants.pants):
    			equipedPantsIndex = playerItems[slot][itemIndex];
    		break;
    		case(Constants.shoes):
    			equipedShoesIndex = playerItems[slot][itemIndex];
    		break;
    	}
    }

    public bool IsItemEquiped(string slot, int itemIndex)
    {
    	bool isEquiped = false;

    	switch(slot)
    	{
    		case(Constants.skin):
    			isEquiped = (equipedSkinIndex == itemIndex);
    		break;
    		case(Constants.front_hair):
    			isEquiped = (equipedHairIndex == itemIndex);
    		break;
    		case(Constants.eyes):
    			isEquiped = (equipedEyesIndex == itemIndex);
    		break;
    		case(Constants.shirt):
    			isEquiped = (equipedShirtIndex == itemIndex);
    		break;
    		case(Constants.pants):
    			isEquiped = (equipedPantsIndex == itemIndex);
    		break;
    		case(Constants.shoes):
    			isEquiped = (equipedShoesIndex == itemIndex);
    		break;
    	}

    	return isEquiped;
    }


}
