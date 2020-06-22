using System;
using System.Collections;
using UnityEngine;

public class Furniture : Movable
{
    public bool drawColliderShape = false;
    public FurnitureData furnitureData;
    public float rotationTime = 1f;
    public ParticleSystem rotationParticles;

    [ContextMenu("Setup Furniture")]
    // Start is called before the first frame update
    void Start()
    {
        rotationParticles.Clear();
        if (furnitureData == null)
        {
            Debug.Log("No FurnitureData found...");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb==null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.freezeRotation = true;
        rb.gravityScale = 0;

        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            sr = gameObject.AddComponent<SpriteRenderer>();
        }

        col = GetComponent<BoxCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider2D>();
        }

        msk = GetComponent<SpriteMask>();
        if (msk == null)
        {
            msk = gameObject.AddComponent<SpriteMask>();
        }

        SetupFurniture();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rotation + " : " + (int)rotation);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotateCounterClockwise();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotateClockwise();
        }
    }

    //Do a Coroutine to rotate the furniture :

    //Ajouter un attribut rotationTime
    //Lerp la size du collider entre col.size[currentRotation] et col.size[nextRotation] à chaque 1/rotationTime
    //Ajouter particules

    void RotateClockwise() 
    {
        lastRotation = rotation;
        if ((int)rotation == Enum.GetValues(typeof(Rotation)).Length - 1 )
        {
            rotation = 0;
        }
        else rotation++;

        if (CheckRotation((int)rotation))
        {
            //UpdateFurniture();
            StartCoroutine("UpdateFurniture");
            return;
        }
        else rotation = lastRotation;
    }

    void RotateCounterClockwise()
    {
        lastRotation = rotation;
        if (rotation == 0)
        {
            rotation = (Rotation)Enum.GetValues(typeof(Rotation)).Length - 1;
        }
        else rotation--;

        if (CheckRotation((int)rotation))
        {
            //UpdateFurniture();
            StartCoroutine("UpdateFurniture");
            return;
        }
        else rotation = lastRotation;
        
    }

    //Sets up the furniture sprite and collider with the current rotation attribute
    void SetupFurniture()
    {
        //Change sprite 
        sr.sprite = furnitureData.sprites[(int)rotation];
        msk.sprite = sr.sprite;
        //Modify collider size depending on the current sprite sized and the Y scale modifier;
        col.size = new Vector2(sr.sprite.bounds.size.x, sr.sprite.bounds.size.y * furnitureData.colliderSettings[(int)rotation].scaleY);
        //Modify collider offset depending on the current sprite sized and the Y offset given in the furnitureData;
            //offsetY = 0   -> real offset = - spriteSizeY/2 + colliderSizeY/2
            //offsetY = 0.5 -> real offset = 0
            //offsetY = 1   -> real offset = + spriteSizeY/2 - colliderSizeY/2
        float offset = 2 * furnitureData.colliderSettings[(int)rotation].offsetY - 1;
        col.offset = new Vector2(col.offset.x, offset * (sr.sprite.bounds.size.y / 2 - col.size.y / 2));

    }

    //Rotation over time
    IEnumerator UpdateFurniture()
    {
        //Play Particle System
        rotationParticles.GetComponent<ParticleSystemRenderer>().sortingOrder = sr.sortingOrder + 1;
        rotationParticles.Play();
        //Get the next Sprite
        Sprite nextSprite = furnitureData.sprites[(int)rotation];
        float timer = 0f;
        while (timer < rotationTime)
        {
            timer += Time.deltaTime;
            // ---- LERPS ---- 

            //Modify collider size depending on the current sprite sized and the Y scale modifier;
            float colSizeX = Mathf.Lerp(sr.sprite.bounds.size.x, nextSprite.bounds.size.x, timer / rotationTime);
            float colSizeY = Mathf.Lerp(sr.sprite.bounds.size.y * furnitureData.colliderSettings[(int)lastRotation].scaleY,
                                        nextSprite.bounds.size.y * furnitureData.colliderSettings[(int)rotation].scaleY, timer / rotationTime);
            col.size = new Vector2(colSizeX, colSizeY);

            //Modify collider offset depending on the current sprite sized and the Y offset given in the furnitureData;
                //offsetY = 0   -> real offset = - spriteSizeY/2 + colliderSizeY/2
                //offsetY = 0.5 -> real offset = 0
                //offsetY = 1   -> real offset = + spriteSizeY/2 - colliderSizeY/2
            float offset = 2 * Mathf.Lerp(furnitureData.colliderSettings[(int)lastRotation].offsetY, furnitureData.colliderSettings[(int)rotation].offsetY, timer / rotationTime) - 1;
            float spriteSizeY = Mathf.Lerp(sr.sprite.bounds.size.y, nextSprite.bounds.size.y, timer / rotationTime);
            col.offset = new Vector2(col.offset.x, offset * (spriteSizeY / 2 - colSizeY / 2));

            yield return null;
        }
        //Change sprite 
        sr.sprite = nextSprite;
        msk.sprite = sr.sprite;
    }


    bool CheckRotation(int direction)
    {
        if (direction < 0 || direction > Enum.GetValues(typeof(Rotation)).Length - 1)
        {
            Debug.LogError("Wrong parameter passed in CheckRotation()");
            return false;
        }
        //Cast a collider of the size of the futur funiture rotation and if it collides with something, return false, else return true
        //maybe create a circle collider with a radius equal to the diagonal length of the boxCollider2D R = Mathf.Sqrt(Mathf.Pow(col.size.x,2)+Mathf.Pow(col.size.y,2))
        return true;
    }

    void OnDrawGizmos()
    {
        if (drawColliderShape)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + (Vector3)GetComponent<BoxCollider2D>().offset, GetComponent<BoxCollider2D>().size);
        }
    }

    void OnValidate()
    {
        Start();
    }
}
