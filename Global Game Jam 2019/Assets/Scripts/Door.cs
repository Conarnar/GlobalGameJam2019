using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int position;
    public TileBase[] doorTiles = new TileBase[2];
    public AudioClip[] sounds = new AudioClip[2];
    public int lockId = 0;
    
    public bool opened = false;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Toggle()
    {
        if (Unlocked())
        {
            opened = !opened;
            tilemap.SetTile(position, doorTiles[opened ? 1 : 0]);
            audioSource.clip = sounds[opened ? 1 : 0];
            audioSource.Play();
            GetComponent<BoxCollider2D>().isTrigger = opened;
        }
        else
        {
            DialogueTrigger dialogue = GetComponent<DialogueTrigger>();

            if (dialogue != null)
            {
                dialogue.Trigger("door");
            }
        }
    }

    bool Unlocked()
    {
        return lockId == 0 || EventTriggers.keys.Contains(lockId);
    }
}
