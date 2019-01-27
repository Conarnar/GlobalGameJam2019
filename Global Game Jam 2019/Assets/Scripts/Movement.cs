using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public Vector2 facingDirection;
    public bool moving
    {
        get
        {
            return rigidBody.velocity.sqrMagnitude != 0;
        }
    }

    void Start()
    {
        facingDirection = Vector2.down;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 direction;

        if (facingDirection.x != 0)
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

            if (direction.magnitude == 0)
            {
                direction = new Vector2(0, Input.GetAxisRaw("Vertical"));
            }
        }
        else
        {
            direction = new Vector2(0, Input.GetAxisRaw("Vertical"));

            if (direction.magnitude == 0)
            {
                direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            }
        }

        if (direction.sqrMagnitude > 0)
        {
            facingDirection = direction;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !moving)
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position + GetComponent<Collider2D>().offset, facingDirection, 1, 1 << 8);

            if (hit)
            {
                hit.transform.GetComponent<Interactable>().onInteract.Invoke();
            }
        }

        rigidBody.velocity = direction * 2;
    }
}
