using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MP_PointCounter : SP_PointCounter
    {
        private TextMeshProUGUI _currentPoints;
        private Image _playerColor;
        private Camera _camera;
        protected MP_CharacterController _character;
        private void Awake()
        {
            _camera = Camera.main;
            _currentPoints = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            //Initialize();
        }

        public void Initialize()
        {
            _character.OnAddPoints += UpdatePoints;
            UpdatePoints(0);  
        }
        public void SetTarget(MP_CharacterController controller)
        {
            _character = controller;
        }
        public void SetColor(Color color)
        {
            _currentPoints.color = color;
        }
        public void UpdatePoints(int data)
        {
            _currentPoints.text = "Points: " + data.ToString();
        }
    }