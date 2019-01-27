using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggers : MonoBehaviour
{
    public static List<int> keys = new List<int>();
    public static bool food = false;
    public static bool bust = false;
    public static bool bat = false;
    public static bool boardBroken = false;
    public static bool pistol = false;

    public bool board = false;

    void Start()
    {
        if (board && boardBroken)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddKey(int lockId)
    {
        keys.Add(lockId);
    }

    public void ObtainFood()
    {
        food = true;
    }

    public void ObtainBust()
    {
        bust = true;
    }

    public void ObtainBat()
    {
        bat = true;
    }

    public void BreakBoard()
    {
        boardBroken = true;
        GetComponent<AudioSource>().Play();
    }

    public void ObtainPistol()
    {
        pistol = true;
        GetComponent<AudioSource>().Play();
    }
}
