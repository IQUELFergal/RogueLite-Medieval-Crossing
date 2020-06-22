using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicOrderLayering : MonoBehaviour
{
    public int offset = 0;
    public Transform objectTranform = null;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (objectTranform == null)
        {
            objectTranform = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        sr.sortingOrder = -(int)objectTranform.position.y + offset;
    }
}
