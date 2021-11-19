using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResourcesManager : Singleton<ResourcesManager>
{	
	private List<Sprite[]> skinSpritesheets = new List<Sprite[]>();
	private List<Sprite[]> hairSpritesheets = new List<Sprite[]>();
	private List<Sprite[]> eyesSpritesheets = new List<Sprite[]>();
	private List<Sprite[]> shirtSpritesheets = new List<Sprite[]>();
	private List<Sprite[]> shoesSpritesheets = new List<Sprite[]>();
	private List<Sprite[]> pantsSpritesheets = new List<Sprite[]>();

	private void Awake()
	{
		foreach(KeyValuePair<string,int> current in Constants.allAssets)
		{
			for(int i = 1; i<=current.Value; i++)
			{
				CatalogResource(current.Key, LoadSkinIndex(current.Key, i));
			}
		}
	}

	private void CatalogResource(string slot, Sprite[] sprites)
	{
		switch(slot)
		{
			case(Constants.skin):
				skinSpritesheets.Add(sprites);
			break;

			case(Constants.front_hair):
				hairSpritesheets.Add(sprites);
			break;

			case(Constants.eyes):
				eyesSpritesheets.Add(sprites);
			break;

			case(Constants.pants):
				pantsSpritesheets.Add(sprites);
			break;

			case(Constants.shirt):
				shirtSpritesheets.Add(sprites);
			break;

			case(Constants.shoes):
				shoesSpritesheets.Add(sprites);
			break;
		}
	}

	private Sprite[] GetInternalResource(string slot, int index)
	{
		Sprite[] target = null;

		switch(slot)
		{
			case(Constants.skin):
				target = skinSpritesheets[index];
			break;

			case(Constants.front_hair):
				target = hairSpritesheets[index];
			break;

			case(Constants.eyes):
				target = eyesSpritesheets[index];
			break;

			case(Constants.pants):
				target = pantsSpritesheets[index];
			break;

			case(Constants.shirt):
				target = shirtSpritesheets[index];
			break;

			case(Constants.shoes):
				target = shoesSpritesheets[index];
			break;
		}

		return target;
	}

	public Sprite[] GetSpritesheet(string skinType, int skinIndex)
	{
		return GetInternalResource(skinType, skinIndex-1);
	}

    public Sprite[] LoadSkinIndex(string skinType, int skinIndex)
    {
    	Sprite[] spritesheet = Resources.LoadAll<Sprite>(Constants.GetAssetPath(skinType, skinIndex));
    	return spritesheet;
    }
}
