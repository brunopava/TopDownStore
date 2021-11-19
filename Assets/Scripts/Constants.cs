using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Slots{
	back_hair,
	skin,
	eyes,
	shirt,
	front_hair,
	shoes,
	pants
}

public class Constants
{
    public static Dictionary<string, int> allAssets = new Dictionary<string, int>()
    {
        {"skin", 5},
        {"front_hair", 30},
        {"eyes", 4},
        {"shirt", 15},
        {"shoes", 12},
        {"pants", 12}
    };

	public const string rootFolder = "Characters/";
	public const string maleFolder = "Male/";
	public const string femaleFolder = "Female/";
	public const string accesoriesFolder = "UnisexAccessories/";

    public static Dictionary<string, string> assetFolders = new Dictionary<string,string>()
    {
    	{"back_hair", "Layer0_Back Hair"},
    	{"skin", "Layer1_Skin"},
    	{"eyes", "Layer2_Eyes"},
    	{"shirt", "Layer3_Shirt"},
    	{"front_hair", "Layer4_Front_Hair"},
    	{"shoes", "Layer5_Shoes"},
    	{"pants", "Layer6_Pants"}
    };

    public static Dictionary<string, string> assetTypes = new Dictionary<string,string>()
    {
    	{"back_hair", "_Hair_B_"},
    	{"skin", "_Skin_"},
    	{"eyes", "_Eyes_"},
    	{"shirt", "_Shirt_"},
    	{"front_hair", "_Hair_"},
    	{"shoes", "_Shoes_"},
    	{"pants", "_Pants_"}
    };

    public const string back_hair = "back_hair";
    public const string skin = "skin";
    public const string eyes = "eyes";
    public const string shirt = "shirt";
    public const string front_hair = "front_hair";
    public const string shoes = "shoes";
    public const string pants = "pants";

    private static string male = "Male";
    private static string female = "Female";

    public static string GetMalePath(string spritePart)
    {
    	return rootFolder + maleFolder + assetFolders[spritePart];
    }

    public static string GetFemalePath(string spritePart)
    {
    	return rootFolder + femaleFolder + assetFolders[spritePart];
    }

    public static string GetAccessoriesPath(string spritePart)
    {
    	return rootFolder + accesoriesFolder;
    }

    public static string GetAssetPath(string assetType, int assetIndex, bool isMale=true)
    {
    	string gender = male;
    	if(!isMale)
    	{
    		gender = female;
    	}
    	return GetMalePath(assetType) + "/" + gender + assetTypes[assetType] + assetIndex.ToString();
    }
}
