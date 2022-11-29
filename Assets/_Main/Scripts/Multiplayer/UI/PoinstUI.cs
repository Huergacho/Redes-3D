
using System;
using Photon.Pun;
using TMPro;
using UnityEngine;


public class PoinstUI : MonoBehaviourPun
{
    public event Action<int> OnUpdatePoint;
    private TMP_Text _pointText;
    private int currPoints = 0;
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        _pointText = GetComponent<TMP_Text>();
    }


    void AddPoints(int point)
    {
        currPoints += point;
        _pointText.text = currPoints.ToString();
        OnUpdatePoint?.Invoke(currPoints);
    }
    
}
