using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Password : MonoBehaviour
{
    public int characters;
    public bool numbers;
    public string answer;
    public Movement player;

    public Image textBox;
    public Text text;

    public UnityEvent onCorrect;
    public UnityEvent onIncorrect;

    int index = 0;
    char[] charArray;
    bool activated;

    void Start()
    {
        textBox.color = Color.clear;
        text.text = "";
    }

    void Update()
    {
        if (activated)
        {
            text.text = (numbers ? "Combination?\n" : "Password?\n") + new string(charArray).ToUpper();

            if (numbers)
            {
                for (char ch = '0'; ch <= '9'; ch++)
                {
                    string letter = new string(ch, 1);

                    if (Input.GetKeyDown(letter) && index < characters)
                    {
                        charArray[index] = ch;
                        index++;
                    }

                    if (Input.GetKeyDown("[" + ch + "]") && index < characters)
                    {
                        charArray[index] = ch;
                        index++;
                    }
                }
            }
            else
            {
                for (char ch = 'a'; ch <= 'z'; ch++)
                {
                    string letter = new string(ch, 1);

                    if (Input.GetKeyDown(letter) && index < characters)
                    {
                        charArray[index] = ch;
                        index++;
                    }
                }
            }

            if (index >= characters)
            {
                player.enabled = true;
                textBox.color = Color.clear;
                text.text = "";

                if (new string(charArray).ToLower() == answer.ToLower())
                {
                    onCorrect.Invoke();
                }
                else
                {
                    onIncorrect.Invoke();
                }

                charArray = null;
                activated = false;
            }
        }
        else
        {
            text.text = "";
        }
    }

    public void Activate()
    {
        player.enabled = false;
        charArray = new char[characters];
        index = 0;
        activated = true;
        textBox.color = Color.white;

        if (numbers)
            charArray = new string('*', characters).ToCharArray();
        else
            charArray = new string('_', characters).ToCharArray();
    }
}
