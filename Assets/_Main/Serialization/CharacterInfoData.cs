using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterInfoData
{
   // public MyVector3 pos;
    public int hp;
    public float speed;
    [SerializeField]private bool isAlive;

    public bool IsAlive { get => isAlive; set => isAlive = value; }
}