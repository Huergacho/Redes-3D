using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CharacterModel : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private string groundTag;
    private Rigidbody2D _rb2d;
    bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector2 dir)
    {
        _rb2d.velocity = new Vector2(dir.x, dir.y) * speed;
    }
    public void Jump()
    {
        if (canJump)
        {
            _rb2d.AddForce(Vector2.up * jumpForce); 
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
