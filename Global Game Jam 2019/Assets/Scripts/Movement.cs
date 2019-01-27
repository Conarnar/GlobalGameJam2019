using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Vector2 facingDirection;

    void Start()
    {
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
        
        if (Input.GetKeyDown(KeyCode.Space) && rigidBody.velocity.sqrMagnitude == 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, 1, 1 << 8);

            if (hit)
            {
                hit.transform.GetComponent<Interactable>().onInteract.Invoke();
            }
        }

        rigidBody.velocity = direction * 2;
    }
}
