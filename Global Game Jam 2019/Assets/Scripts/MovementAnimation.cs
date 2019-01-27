using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class MovementAnimation : MonoBehaviour
{
    Movement movement;
    new SpriteRenderer renderer;

    public MoveFrames upFrames;
    public MoveFrames rightFrames;
    public MoveFrames downFrames;
    public MoveFrames leftFrames;

    int frame = 0;

    void Start()
    {
        movement = GetComponent<Movement>();
        renderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        MoveFrames frames = movement.facingDirection.x > 0 ? rightFrames :
            (movement.facingDirection.y > 0 ? upFrames :
            (movement.facingDirection.x < 0 ? leftFrames : downFrames
            ));

        

        if (movement.moving)
        {
            renderer.sprite = frames.moving[(frame / 6) % frames.moving.Length];
            frame++;
        }
        else
        {
            renderer.sprite = frames.still;
        }
    }
}

[System.Serializable]
public class MoveFrames
{
    public Sprite still;
    public Sprite[] moving;
}