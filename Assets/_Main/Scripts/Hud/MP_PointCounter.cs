using Photon.Pun;
using TMPro;


public class MP_PointCounter : MonoBehaviourPun
    {
        private TMP_Text _currentPoints;
        private void Awake()
        {
            _currentPoints = GetComponent<TMP_Text>();
        }
        public void Initialize(CharacterModel controller)
        {
            controller.OnAddPoints += UpdatePoints;
        }
        public void UpdatePoints(int data, string name)
        {
            _currentPoints.text = "Points: " + data.ToString();
            MasterController.Instance.photonView.RPC("UpdateScores",RpcTarget.All,name,data);
        }
    }