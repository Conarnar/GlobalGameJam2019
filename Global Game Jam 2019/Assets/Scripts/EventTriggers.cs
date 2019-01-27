using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggers : MonoBehaviour
{
    public static List<int> keys = new List<int>();
    public static List<string> triggers = new List<string>();
    
    public string triggerName;
    public string requireTrigger;
    public bool deleteIfTriggered = false;
    public bool invokeOnStart = false;
    public UnityEvent events;

    void Start()
    {
        if (deleteIfTriggered && triggers.Contains(triggerName) && (requireTrigger == "" || triggers.Contains(requireTrigger)))
        {
            gameObject.SetActive(false);
        }

        if (invokeOnStart && !triggers.Contains(triggerName) && (requireTrigger == "" || triggers.Contains(requireTrigger)))
        {
            events.Invoke();
        }
    }

    public void InvokeIfNotTriggered()
    {
        if (!triggers.Contains(triggerName) && (requireTrigger == "" || triggers.Contains(requireTrigger)))
        {
            events.Invoke();
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

    public void triggerKey(string key)
    {
        triggers.Add(key);
    }
}
