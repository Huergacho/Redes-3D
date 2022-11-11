using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SP_PointCounter : MonoBehaviourPun
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

        protected virtual void Start()
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
        public virtual void UpdatePoints(int data)
        {
            _currentPoints.text = "Points: " + data.ToString();
        }
    }