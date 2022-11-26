using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MP_PointCounter : MonoBehaviourPun
    {
        private TextMeshProUGUI _currentPoints;
        private void Awake()
        {
            _currentPoints = GetComponent<TextMeshProUGUI>();
        }
        public void Initialize(CharacterController controller)
        {
            controller.OnAddPoints += UpdatePoints;
            UpdatePoints(0);  
        }
        public void UpdatePoints(int data)
        {
            _currentPoints.text = "Points: " + data.ToString();
        }
    }