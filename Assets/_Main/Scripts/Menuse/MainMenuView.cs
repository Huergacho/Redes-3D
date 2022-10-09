using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button singleButClick;
    [SerializeField] private Button coOpButClick;

    [SerializeField] private Sprite singleClickSprite;
    [SerializeField] private Sprite coOpClickSprite;

    private Sprite _singleDefault;
    private Sprite _coOpDefault;

    private void Start()
    {
        _singleDefault = singleButClick.image.sprite;
        _coOpDefault = coOpButClick.image.sprite;
    }

    public void SingleSelected()
    {
        singleButClick.image.sprite = singleClickSprite;
    }

    public void CoOpSelected()
    {
        coOpButClick.image.sprite = coOpClickSprite;
    }
    public void SingleUnselected()
    {
        singleButClick.image.sprite = _singleDefault;
    }

    public void CoOpUnselected()
    {
        coOpButClick.image.sprite = _coOpDefault;
    }
}
