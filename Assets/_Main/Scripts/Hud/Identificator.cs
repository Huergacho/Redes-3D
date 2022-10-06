using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using  Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Identificator : MonoBehaviourPun
{
    [SerializeField] private Image _colorIdent;
    [SerializeField] private Vector3 offset;
    private Transform _target;
    [SerializeField]private Color[] colorBucket;
    public bool hasChanged = false;
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }

  public void SetTarget(Transform target)
  {
      _target = target;
  }

  public void SetColor(int playerIndex)
  {
      _colorIdent.color = colorBucket[playerIndex];
  }

  public Image GetColor()
  {
    return _colorIdent;
  }
  private void Update()
  {
      if (_target != null)
      {
          var pos = _camera.WorldToScreenPoint(_target.position + offset);
          transform.position = pos;
      }
  }
}
