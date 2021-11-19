using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOpenStore : MonoBehaviour
{
	public string shopTitle;
	public int maxProducts;
	public Slots productSelection;

	private UIPopupShop _popupShop;

	private bool _isStoreOpen;

	private void CloseStore()
	{
		_isStoreOpen = false;
		_popupShop.PurgeProductCells();
        _popupShop.gameObject.SetActive(false);
	}

    private void OnMouseDown()
    {
    	if(!_isStoreOpen)
    	{
	   		OpenPopupShop();
	   		CameraController.Instance.ZoomOnPlayer();
	   		NotificationCenter.DefaultCenter.PostNotification(this, "DisableMovement");
    	}
	}

	public void OpenPopupShop()
	{
		if(_popupShop == null)
		{
			_popupShop = UIPopupLayer.Instance.GetPopupByType<UIPopupShop>();
		}

		_isStoreOpen = true;
        _popupShop.gameObject.SetActive(true);

        _popupShop.SetPopupTitle(shopTitle);
        _popupShop.CreateProducts(maxProducts, productSelection.ToString());
        _popupShop.Initialize(
            ()=>{
                _popupShop.PurgeProductCells();
                _popupShop.gameObject.SetActive(false);
                _isStoreOpen = false;
                NotificationCenter.DefaultCenter.PostNotification(this, "EnableMovement");
                CameraController.Instance.ZoomOutPlayer();
                NotificationCenter.DefaultCenter.PostNotification(this, "ResetSkin");
            }
        );
		
	}
}
