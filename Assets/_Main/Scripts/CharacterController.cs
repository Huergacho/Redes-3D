using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(CharacterModel))]
public class CharacterController : MonoBehaviourPun
{
    private CharacterModel model;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }
        model = GetComponent<CharacterModel>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveCommand();
        JumpCommand();
    }
    void JumpCommand()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            model.Jump();
        }
    }
    void MoveCommand()
    {
        var movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        if(movement != Vector2.zero)
        {
            model.Move(movement);
        }
    }
}
