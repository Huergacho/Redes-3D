using Photon.Pun;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviourPun
{
    [SerializeField] private float timeLeft = 99;
    private int timeInInt;
    private bool TimerOn = false;
    private TMP_Text _timerText;
    
    private void Awake()
    {
        // if (!PhotonNetwork.IsMasterClient)
        // {
        //     Destroy(this);
        // }
    }

    void Start()
    {
        _timerText = GetComponent<TMP_Text>();
    }

    [PunRPC]
    public void SetStart()
    {
        TimerOn = true;
    }
    void Update()
    {
        if (TimerOn)
        {
            timeLeft -= Time.deltaTime;
            timeInInt = (int)timeLeft;
            
            _timerText.text = timeInInt.ToString();
            if (timeInInt == 0)
            {
                TimerOn = false;
                MasterController.Instance.FinishGame();
            }
        }
        
    }

}
