using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed = 5f;
    private Animator anim;
    private Rigidbody2D rb;
    private enum Direction { Up, Down, Left, Right }
    private 
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(MoveX, MoveY).normalized;
        rb.velocity = movement * moveSpeed;
        if (MoveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.Play("FacingRight");
        }
        else if (MoveX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.Play("FacingRight");
        }
        else if (MoveY > 0)
        {
            anim.Play("FacingUp");
        }
        else if (MoveY < 0)
        {
            anim.Play("FacingDown");
        }
        else
        {
            anim.Play("Idle");
        }
    }
    
}
