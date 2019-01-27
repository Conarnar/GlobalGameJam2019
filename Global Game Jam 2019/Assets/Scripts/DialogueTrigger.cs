using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Image textBox;
    public Text text;
    public Movement player;

    public List<Dialogue> dialogues = new List<Dialogue>();

    public bool IsRunning { get; private set; } = false;
    public bool IsWaiting { get; private set; } = false;
    public int GetIndex { get; private set; } = 0;

    public bool pause = false;

    bool closed;

    void Start()
    {
        textBox.color = Color.clear;
        text.text = "";
    }

    public void Trigger(string key)
    {
        Trigger(key, 0);
    }

    public void TriggerAfterFadeDelay(string key)
    {
        Trigger(key, 30);
    }

    public void Trigger(string key, int delay)
    {
        Dialogue dialogue = dialogues.Find(d => d.key == key);

        if (dialogue != null)
        {
            player.enabled = false;
            IsRunning = true;
            IsWaiting = false;
            StartCoroutine(RunDialogue(dialogue.sentences, delay));
        }
    }

    IEnumerator RunDialogue(string[] sentences, int delay)
    {
        for (int i = 0; i < delay; i++)
            yield return null;

        for (int i = 0; i < 10; i++)
        {
            textBox.color = new Color(1, 1, 1, (i + 1)/10f);
            yield return null;
        }

        for (GetIndex = 0; GetIndex < sentences.Length; GetIndex++)
        {
            text.text = "";
            char[] characters = sentences[GetIndex].ToCharArray();

            for (int i = 0; i < characters.Length; i++)
            {
                text.text += characters[i];
                if (Input.GetKey(KeyCode.Space))
                {
                    i++;
                    if (i < characters.Length)
                    {
                        text.text += characters[i];
                    }
                }
                IsWaiting = false;
                yield return null;
            }

            closed = false;

            while ((!Input.GetKeyDown(KeyCode.Space) || pause) && !closed)
            {
                IsWaiting = true;
                yield return null;
            }
        }

        IsWaiting = false;

        text.text = "";

        for (int i = 0; i < 10; i++)
        {
            textBox.color = new Color(1, 1, 1, (9 - i) / 10f);
            yield return null;
        }

        IsRunning = false;
        player.enabled = true;
    }

    public void Close()
    {
        closed = true;
    }
}

[System.Serializable]
public class Dialogue
{
    public string key;
    [TextArea]
    public string[] sentences;
}