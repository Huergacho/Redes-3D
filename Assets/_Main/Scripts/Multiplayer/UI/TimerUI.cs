using Photon.Pun;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviourPun
{
    [SerializeField] private float timeLeft;
    private int timeInInt;
    private bool TimerOn;
    private TMP_Text _timerText;
  //  public event Action OnTimeIsUp;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        TimerOn = true;
        _timerText = GetComponent<TMP_Text>();
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
