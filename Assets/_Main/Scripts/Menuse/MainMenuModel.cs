using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuModel : MonoBehaviour
{
    private MainMenuView _menuView;
    // Start is called before the first frame update

    private void Awake()
    {
        _menuView = GetComponent<MainMenuView>();
    }

    public void Subscribe(MainMenuController controller)
    {
        controller.OnSingleButClick += GoToSingleGame;
        controller.OnCoOpButClick += GoToCoOpGame;
        controller.OnSingleButSelect += SingleButSelected;
        controller.OnCoOpButSelect += CoOpButSelected;
        controller.OnSingleButUnselect += SingleButUnselected;
        controller.OnCoOpButUnselect += CoOpButUnselected;
    }

    private void GoToSingleGame()
    {
        
        SceneManager.LoadScene("SinglePlayer");
    }
    private void GoToCoOpGame()
    {
        SceneManager.LoadScene("Connect");
    }

    private void SingleButSelected()
    {
        _menuView.SingleSelected();
    }    
    private void CoOpButSelected()
    {
        _menuView.CoOpSelected();
    }
    private void SingleButUnselected()
    {
        _menuView.SingleUnselected();
    }    
    private void CoOpButUnselected()
    {
        _menuView.CoOpUnselected();
    }
}
