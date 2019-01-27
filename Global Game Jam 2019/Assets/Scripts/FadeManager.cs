using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    static FadeManager instance = null;
    public Image image;

    Coroutine coroutine;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            FadeToColor(Color.clear, 30);
        }
    }

    public static void FadeToColor(Color color, int duration)
    {
        if (instance.coroutine != null)
        {
            instance.StopCoroutine(instance.coroutine);
        }

        instance.coroutine = instance.StartCoroutine(instance.FadeEffect(color, duration));
    }

    public static void SetColor(Color color)
    {
        if (instance.coroutine != null)
        {
            instance.StopCoroutine(instance.coroutine);
        }

        instance.image.color = color;
    }

    IEnumerator FadeEffect(Color color, int duration)
    {
        Color startColor = image.color;

        for (int i = 0; i < duration; i++)
        {
            image.color = Color.Lerp(startColor, color, (i + 1f) / duration);
            yield return null;
        }
    }
}
