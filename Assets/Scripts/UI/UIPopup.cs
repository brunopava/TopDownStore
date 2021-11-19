using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour
{
	public Text popupTitle;

	public Button closeButton;
	public Button confirmButton;
	public Button cancelButton;

	public delegate void OnClose();
	public delegate void OnConfirm();
	public delegate void OnCancel();

	public OnClose baseOnClose;
	public OnConfirm baseOnConfirm;
	public OnCancel baseOnCancel;


	public void SetPopupTitle(string title)
    {
    	if(popupTitle != null)
    	{
    	    popupTitle.text = title;
    	}
    }

	public void Initialize(OnClose onClose = null, OnConfirm onConfirm = null, OnCancel onCancel = null)
	{
		baseOnClose = onClose;
		baseOnCancel = onCancel;
		baseOnConfirm = onConfirm;

		if(closeButton != null)
		{
			closeButton.onClick.AddListener(() => {
				if(onClose != null)
				{
					onClose();
					RemoveAllEventListeners();
				}
			});
		}

		if(confirmButton != null)
		{
			confirmButton.onClick.AddListener(() => {
				if(onConfirm != null)
				{
					onConfirm();	
					RemoveAllEventListeners();
				}
			});
		}

		if(cancelButton != null)
		{
			cancelButton.onClick.AddListener(() => {
				if(onCancel != null)
				{
					onCancel();	
					RemoveAllEventListeners();
				}
			});		
		}
	}

	public void RemoveAllEventListeners()
	{
		if(closeButton != null)
		{
			closeButton.onClick.RemoveAllListeners();
		}

		if(confirmButton != null)
		{
			confirmButton.onClick.RemoveAllListeners();
		}

		if(cancelButton != null)
		{
			cancelButton.onClick.RemoveAllListeners();
		}
	}    
}
