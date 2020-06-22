using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Plot : MonoBehaviour
{
    public Sprite withoutSeedSprite;
    public Sprite withSeedSprite;
    public Vector2Int plantTranslationLimitInPixel = Vector2Int.zero;

    public SeedData seed = null;

    int growDuration;
    float currentTime;
    public int currentStage = 0;

    SpriteRenderer plotSr;
    SpriteRenderer plantSr;

    // Start is called before the first frame update
    void Start()
    {
        plotSr = GetComponent<SpriteRenderer>();
        plantSr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        //Debug
        Plant(seed);
    }

    // Update is called once per frame
    void Update()
    {
        if(seed !=null) Grow();
    }

    public void Plant(SeedData s) //appelée après que le joueur ait interagi avec le plot, que le menu se soit ouvert et que le joueur ait appuyé sur le bouton correspondant à la graine qu'il veut planter
    {
        //Translate the plant SpriteRenderer from a random number of pixel given some limits
        float xtranslation = Random.Range(-plantTranslationLimitInPixel.x, plantTranslationLimitInPixel.x) / ((plantSr.sprite != null) ? plantSr.sprite.pixelsPerUnit : plotSr.sprite.pixelsPerUnit);
        float ytranslation = Random.Range(-plantTranslationLimitInPixel.y, plantTranslationLimitInPixel.y) / ((plantSr.sprite != null) ? plantSr.sprite.pixelsPerUnit : plotSr.sprite.pixelsPerUnit);
        Vector2 translation = new Vector2(xtranslation, ytranslation);
        plantSr.transform.Translate(translation);

        seed = s;
        Debug.Log("Planting " + seed.name);
        plotSr.sprite = withSeedSprite;
        plantSr.sprite = seed.growStages[0];
        growDuration = Random.Range(seed.minGrowDuration, seed.maxGrowDuration);
        currentTime = 0;
    }

    void Grow()
    {
        currentTime += Time.deltaTime;
        if (currentTime > growDuration * (currentStage + 1) / seed.growStages.Length && currentStage < (seed.growStages.Length - 1))
        {
            currentStage++;
            plantSr.sprite = seed.growStages[currentStage];
        }
    }

    void Grow(float time)
    {
        currentTime += time;
        for (int i = 0; i < seed.growStages.Length - 1; i++)
        {
            if (currentTime > growDuration * (i + 1) / seed.growStages.Length && currentTime < growDuration * (i + 2) / seed.growStages.Length) //surement pas bon
            {
                currentStage = i;
                plantSr.sprite = seed.growStages[currentStage];
            }
        }
    }

    public void Harvest()
    {
        Debug.Log("Harvesting " + seed.name + " from " + this);
        seed = null;
        plantSr.sprite = null;
        plotSr.sprite = withoutSeedSprite;
    }
}
