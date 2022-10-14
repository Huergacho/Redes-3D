using System;
using System.Diagnostics.Tracing;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
public class RoundCounter : MonoBehaviourPun
{
    public TextMeshProUGUI RoundText;
    [PunRPC]
    public void UpdateText(int amount)
    {
        RoundText.text = amount.ToString();
   }
}