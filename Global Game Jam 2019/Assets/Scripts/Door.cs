using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int position;
    public TileBase[] doorTiles = new TileBase[2];
    public int lockId = 0;

    public static List<int> keys = new List<int>();

    void Update()
    {
        foreach (Rigidbody2D rigidbody in GetComponentsInChildren<Rigidbody2D>())
        {
            rigidbody.simulated = !Unlocked();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Unlocked())
            tilemap.SetTile(position, doorTiles[1]);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (Unlocked())
            tilemap.SetTile(position, doorTiles[0]);
    }

    bool Unlocked()
    {
        return lockId == 0 || keys.Contains(lockId);
    }
}
