using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIPopupShop : UIPopup
{
    public RectTransform productGrid;

    public GameObject productCellPrefab;

    private List<UIProductCell> _products = new List<UIProductCell>();

    public void CreateProducts(int maxProducts, string productSelection)
    {
    	for(int i = 0; i < maxProducts; i ++)
    	{
    		Sprite[] sprites = ResourcesManager.Instance.GetSpritesheet(productSelection, i+1);
    		Dictionary<string, Sprite> spritesheet = sprites.ToDictionary(x => x.name, x => x);

    		GameObject cell = GameObject.Instantiate(productCellPrefab, Vector3.zero, Quaternion.identity) as GameObject;
    		UIProductCell product = cell.GetComponent<UIProductCell>();

    		product.productType = productSelection;
    		product.SetSpriteSheet(spritesheet, i+1);
            product.InitButtons();

    		_products.Add(product);
    		cell.transform.SetParent(productGrid);
    	}

        StartCoroutine(Reposition());
    }

    public void PurgeProductCells()
    {
    	foreach(UIProductCell current in _products)
    	{
    		GameObject.Destroy(current.gameObject);
    	}

    	_products = new List<UIProductCell>();
    }
        
    public IEnumerator Reposition()
    {
        yield return new WaitForSeconds(0.05f);
        productGrid.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 100f;
    }
}
