using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueTrigger), typeof(SpriteRenderer))]
public class Cutscene1 : MonoBehaviour
{
    SpriteRenderer greg;
    public Movement player;

    void Start()
    {
        greg = GetComponent<SpriteRenderer>();
        greg.color = Color.clear;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (EventTriggers.keys.Contains(1) && !EventTriggers.triggers.Contains("cutscene1"))
        {
            EventTriggers.triggers.Add("cutscene1");
            StartCoroutine(Run());
        }
    }

    IEnumerator Run()
    {
        greg.color = Color.white;
        player.enabled = false;

        for (int i = 0; i < 30; i++)
            yield return null;

        DialogueTrigger trigger = GetComponent<DialogueTrigger>();
        trigger.Trigger("cutscene");

        yield return new WaitUntil(() => !trigger.IsRunning);
        player.enabled = false;

        Stairs.spawn = new Vector3(-1.6f, 11.2f);
        SceneManager.LoadScene("Floor 3");
    }
}
