using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class SP_CharacterView : MonoBehaviourPun
{
    [SerializeField]protected Identificator identificatorPrefab;

    [SerializeField] protected SP_PointCounter spPointCounter;
    // Start is called before the first frame update
    void Start()
    {
        identificatorPrefab.SetTarget(this.transform);
        identificatorPrefab.SetColor(1);
        spPointCounter.SetColor(identificatorPrefab.GetColor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
