using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class SP_CharacterView : MonoBehaviourPun
{
    [SerializeField]protected Identificator identificatorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        identificatorPrefab.SetTarget(this.transform);
        identificatorPrefab.SetColor(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
