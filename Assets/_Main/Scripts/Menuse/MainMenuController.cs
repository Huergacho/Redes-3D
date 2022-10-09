using System;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private MainMenuModel _menuModel;
    // Start is called before the first frame update
    public event Action OnSingleButClick;
    public event Action OnCoOpButClick;
    public event Action OnSingleButSelect;
    public event Action OnCoOpButSelect;
    public event Action OnSingleButUnselect;
    public event Action OnCoOpButUnselect;

    void Awake()
    {
        _menuModel = GetComponent<MainMenuModel>();
    }

    private void Start()
    {
        _menuModel.Subscribe(this);
    }

    // Update is called once per frame
    public void OnButtonSingleClick()
    {
        OnSingleButClick?.Invoke();
    }
    
    public void OnButtonCoOpClick()
    {
        OnCoOpButClick?.Invoke();
    }

    public void OnSelectSingle()
    {
        OnSingleButSelect?.Invoke();
    }

    public void OnSelectCoOp()
    {
        OnCoOpButSelect?.Invoke();
    }
    public void OnUnselectSingle()
    {
        OnSingleButUnselect?.Invoke();
    }

    public void OnUnselectCoOp()
    {
        OnCoOpButUnselect?.Invoke();
    }

}
