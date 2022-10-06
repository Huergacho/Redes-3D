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
        [SerializeField]private Identificator _identificator;
        private Camera _camera;
        private SP_CharacterController _character;
        private void Awake()
        {
            _camera = Camera.main;
            _currentPoints = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _character = SP_GameManager.SPInstance.Character.GetComponent<SP_CharacterController>();
            _character.addPoints += UpdatePoints;
            _playerColor = _identificator.GetColor();
            UpdatePoints(0);
        }

        public void UpdatePoints(int data)
        {
            _currentPoints.text = "Points: " + data.ToString();
        }
    }