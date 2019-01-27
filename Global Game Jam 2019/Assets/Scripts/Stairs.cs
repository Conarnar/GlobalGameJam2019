using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    public string nextScene;
    public static Vector3 spawn = new Vector3(-1.6f, 11.2f);

    public Vector3 spawnPoint;

    bool entered = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!entered)
        {
            entered = true;
            collision.GetComponent<Movement>().enabled = false;
            collision.attachedRigidbody.velocity = new Vector2();
            StartCoroutine(NextFloor(collision));
        }
    }

    IEnumerator NextFloor(Collider2D collision)
    {
        for (int i = 0; i < 30; i++)
        {
            collision.transform.position = Vector3.Lerp(collision.transform.position, (Vector2) transform.position - collision.offset, 0.1f);
            yield return null;
        }

        spawn = spawnPoint;
        SceneManager.LoadScene(nextScene);
    }
}
