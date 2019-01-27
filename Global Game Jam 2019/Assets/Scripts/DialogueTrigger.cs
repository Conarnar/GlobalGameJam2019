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

    void Start()
    {
        textBox.color = Color.clear;
        text.text = "";
    }

    public void Trigger(string key)
    {
        player.enabled = false;

        Dialogue dialogue = dialogues.Find(d => d.key == key);

        if (dialogue != null)
            StartCoroutine(RunDialogue(dialogue.sentences));
    }

    IEnumerator RunDialogue(string[] sentences)
    {
        for (int i = 0; i < 10; i++)
        {
            textBox.color = new Color(1, 1, 1, (i + 1)/10f);
            yield return null;
        }

        foreach (string line in sentences)
        {
            text.text = "";
            char[] characters = line.ToCharArray();

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
                yield return null;
            }

            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }
        }

        text.text = "";

        for (int i = 0; i < 10; i++)
        {
            textBox.color = new Color(1, 1, 1, (9 - i) / 10f);
            yield return null;
        }

        player.enabled = true;
    }
}

[System.Serializable]
public class Dialogue
{
    public string key;
    [TextArea]
    public string[] sentences;
}