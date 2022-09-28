using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CharacterModel : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private string groundTag;
    private Rigidbody _rb;
    bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector3 dir)
    {
        _rb.velocity = new Vector3(dir.x, 0,dir.z) * speed;
    }
    public void Jump()
    {
        if (canJump)
        {
            _rb.AddForce(Vector2.up * jumpForce); 
        }
    }
    public void lookDir(Vector3 dir){
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != groundTag)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }
    
}
