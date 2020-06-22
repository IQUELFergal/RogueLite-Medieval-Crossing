using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTranslationInteractable : MonoBehaviour, IInteractable
{
    public Vector3 translation = Vector3.zero;
    [Min(0.001f)] public float duration = 0.001f;

    bool opened = false;
    bool isMoving = false;
    Vector3 basePosition;

    public string interactionText { get { return InteractionText; } }
    [SerializeField] private string InteractionText = "Open";

    private void Start()
    {
        basePosition = transform.position;
    }

    public void Interact()
    {
        if (!isMoving) StartCoroutine(MoveDoor());
    }

    IEnumerator MoveDoor()
    {
        isMoving = true;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.position = opened ? Vector3.Lerp(basePosition + translation, basePosition, timer / duration) : Vector3.Lerp(basePosition, basePosition + translation, timer / duration);

            yield return null;
        }
        isMoving = false;
        opened = !opened;
    }
}
