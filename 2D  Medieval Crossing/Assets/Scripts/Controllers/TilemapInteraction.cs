using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInteraction : MonoBehaviour
{
    public Tilemap tilemap;
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision)
        {
            Debug.Log(collision);
        }
    }
}
