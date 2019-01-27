using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene2 : MonoBehaviour
{
    SpriteRenderer greg;
    public Movement player;
    public Button[] buttons = new Button[2];

    public Sprite gregDead;
    public GameObject blood;
    public GameObject finalKey;

    string choice = null;

    void Start()
    {
        blood.SetActive(false);
        finalKey.SetActive(false);
        greg = GetComponent<SpriteRenderer>();

        if (EventTriggers.triggers.Contains("pistol") && !EventTriggers.triggers.Contains("cutscene2"))
        {
            EventTriggers.triggers.Add("cutscene2");
            StartCoroutine(Run());
        }
        else
        {
            if (EventTriggers.triggers.Contains("kill"))
            {
                greg.sprite = gregDead;
                blood.SetActive(true);

                if (!EventTriggers.triggers.Contains("key8"))
                    finalKey.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Run()
    {
        player.enabled = false;

        for (int i = 0; i < 30; i++)
            yield return null;

        DialogueTrigger trigger = GetComponent<DialogueTrigger>();
        trigger.Trigger("cutscene");

        yield return new WaitUntil(() => trigger.GetIndex > 10);
        trigger.pause = true;
        yield return new WaitUntil(() => trigger.IsWaiting);

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }

        yield return new WaitUntil(() => choice != null);
        trigger.pause = false;

        if (choice == "trust")
        {
            trigger.Close();
            player.canMove = false;

            for (int i = 0; i < 30; i++)
                yield return null;

            for (int i = 0; i < 150; i++)
            {
                player.rigidBody.velocity = new Vector2(0, -2);
                yield return null;
            }

            EventTriggers.keys.Add(8);
            player.canMove = true;
            trigger.Trigger("trust");
        }
        else
        {
            EventTriggers.triggers.Add("kill");

            trigger.Close();
            player.canMove = false;

            for (int i = 0; i < 30; i++)
                yield return null;

            FadeManager.SetColor(Color.black);
            GetComponent<AudioSource>().Play();

            greg.sprite = gregDead;
            blood.SetActive(true);
            finalKey.SetActive(true);

            for (int i = 0; i < 120; i++)
                yield return null;

            FadeManager.FadeToColor(Color.clear, 30);

            for (int i = 0; i < 90; i++)
                yield return null;

            player.canMove = true;
            trigger.Trigger("kill");
        }
    }

    public void Choice(bool ch)
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }

        choice = ch ? "trust" : "kill";
    }
}
