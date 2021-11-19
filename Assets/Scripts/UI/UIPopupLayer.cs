using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPopupLayer : Singleton<UIPopupLayer>
{
    public UIPopup[] _popups;

    private UIMenu _menu;
    private UIInventory _inventory;

    private void Awake()
    {   
    	DisablePopups();
    }

    private void Start()
    {
        _menu = GetPopupByType<UIMenu>();
        _inventory = GetPopupByType<UIInventory>();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!_menu.gameObject.activeSelf)
            {
                _menu.gameObject.SetActive(true);
                _menu.Initialize(
                    ()=>{
                        _menu.gameObject.SetActive(false);
                    },
                    ()=>{
                        Wallet.Instance.AddFunds(100);
                        _menu.gameObject.SetActive(false);
                    },
                    ()=>{
                        Application.Quit();
                        _menu.gameObject.SetActive(false);
                    }
                );
            }
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            if(!_inventory.gameObject.activeSelf)
            {
                OpenInventory();
            }
        }
    }

    public void OpenInventory(string slot=Constants.skin, bool isSelling=false)
    {
        _inventory.gameObject.SetActive(true);
        _inventory.SetupButtons();
        _inventory.Initialize(
            ()=>{
                _inventory.gameObject.SetActive(false);
            }
        );
        
        _inventory.Reset(slot);

        if(isSelling)
        {
            _inventory.SetSellingMode();
        }
    }

    public void CloseInventory()
    {
        _inventory.gameObject.SetActive(false);
    }

    public void DisablePopups()
    {
        _popups = gameObject.GetComponentsInChildren<UIPopup>(true);

        foreach(UIPopup current in _popups)
        {
            current.gameObject.SetActive(false);
        }
    }

    public T GetPopupByType<T>() where T : class
    {
        foreach(UIPopup current in _popups)
        {
            if(current.gameObject.GetComponent<T>() != null)
            {
                return current.gameObject.GetComponent<T>();
            }
        }
        return null;
    }
}
