using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPopupLayer : Singleton<UIPopupLayer>
{
    public UIPopup[] _popups;

    private UIMenu _menu;

    private void Awake()
    {   
    	DisablePopups();
    }

    private void Start()
    {
        _menu = GetPopupByType<UIMenu>();
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
