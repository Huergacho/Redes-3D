using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HudManager : MonoBehaviourPun
{
    private TimerUI _timer;

    private UIWinManager _winhud;
    // Start is called before the first frame update
    void Awake()
    {
        _timer = GetComponent<TimerUI>();
        _winhud = GetComponent<UIWinManager>();
    }

    public void StartTimer()
    {
        _timer.SetStart();
    }

    public void WinScreen(string text, string points, bool status)
    {
        _winhud.WinScreen(text,points,status);
    }
    

}
