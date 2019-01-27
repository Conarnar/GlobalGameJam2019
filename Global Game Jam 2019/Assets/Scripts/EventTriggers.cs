using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggers : MonoBehaviour
{
    public static List<int> keys = new List<int>();
    public static bool takenFood = false;
    public static bool takenBust = false;
    public static bool placedBust = false;
    public static bool takenBat = false;
    public static bool boardBroken = false;
    public static bool takenPistol = false;

    public bool board = false;
    public int keyId = 0;
    public bool food = false;

    void Start()
    {
        if (keys.Contains(keyId) ||
            (board && boardBroken) || 
            (food && takenFood))
        {
            gameObject.SetActive(false);
        }
    }

    public void HideObject()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;
    }

    public void AddKey(int lockId)
    {
        keys.Add(lockId);
    }

    public void ObtainFood()
    {
        takenFood = true;
    }

    public void ObtainBust()
    {
        takenBust = true;
    }

    public void ObtainBat()
    {
        takenBat = true;
    }

    public void BreakBoard()
    {
        boardBroken = true;
        GetComponent<AudioSource>().Play();
    }

    public void ObtainPistol()
    {
        takenPistol = true;
        GetComponent<AudioSource>().Play();
    }
}
