using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBox : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    //NO ME TOMA LA COLISION
    private void OnCollisionEnter(Collision collision)
    {
        if ((playerLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("FALOPA");
            }
        }
    }
}
