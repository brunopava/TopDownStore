using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// using System.Linq;

public class ResourcesManager : Singleton<ResourcesManager>
{	
	
    public Sprite[] LoadSkinIndex(string skinType, int skinIndex)
    {
    	Sprite[] spritesheet = Resources.LoadAll<Sprite>(Constants.GetAssetPath(skinType, skinIndex));
    	return spritesheet;
    }
}
