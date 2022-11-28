using System;
using Photon.Pun;

public class UIManager : MonoBehaviourPun
{
    private PoinstUI _pointsUI;
    private TimerUI _timerUI;
    private int _points;
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }
        else
        {
            _pointsUI = GetComponent<PoinstUI>();
            _timerUI = GetComponent<TimerUI>();
        }
    }

    void Start()
    {
        Subscribe();
    }

    void Subscribe()
    {
        _timerUI.OnTimeIsUp += Finish;
    }

    // Update is called once per frame
    void Finish()
    {
        
    }

    void Addpoints(int points)
    {
        _points += points;
    }
    
}
