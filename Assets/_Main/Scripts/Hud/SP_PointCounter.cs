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
        protected SP_CharacterController _character;
        private void Awake()
        {
            _camera = Camera.main;
            _currentPoints = GetComponent<TextMeshProUGUI>();
        }

        protected virtual void Start()
        {
            AssignPlayer();
            Initialize();
        }

        protected void Initialize()
        {
            _character.OnAddPoints += UpdatePoints;
            UpdatePoints(0);  
        }
        public void SetTarget(MP_CharacterController controller)
        {
            _character = controller;
        }
        private void AssignPlayer()
        {
            if (SP_GameManager.SPInstance != null)
            {
                _character = SP_GameManager.SPInstance.Character.GetComponent<SP_CharacterController>();
            }
            else
            {
                print("fiumba");
            }
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