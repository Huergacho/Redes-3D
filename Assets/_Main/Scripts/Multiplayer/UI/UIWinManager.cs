using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class UIWinManager : MonoBehaviourPun
{

    [SerializeField] private GameObject winnerTextContainer;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI winnerPointsText;
    public event Action<bool> OnWin;

    private void Awake()
    {
        winnerTextContainer.SetActive(false);
    }

    [PunRPC]
    public void WinScreen(string text,string points, bool status)
    {
        OnWin?.Invoke(true);
        winnerTextContainer.SetActive(status);
        winnerText.text = text;
        winnerPointsText.text = points;
    }
}