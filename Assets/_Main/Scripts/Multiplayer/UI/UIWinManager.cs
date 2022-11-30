using Photon.Pun;
using TMPro;
using UnityEngine;

public class UIWinManager : MonoBehaviourPun
{

    [SerializeField] private GameObject winnerTextContainer;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI winnerPointsText;

    private void Awake()
    {
        winnerTextContainer.SetActive(false);
    }

    [PunRPC]
    public void WinScreen(string text,string points, bool status)
    {
        winnerTextContainer.SetActive(status);
        winnerText.text = text;
        winnerPointsText.text = points;
    }
}