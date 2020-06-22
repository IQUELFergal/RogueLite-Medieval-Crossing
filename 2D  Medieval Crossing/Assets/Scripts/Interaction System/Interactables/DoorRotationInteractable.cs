using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotationInteractable : MonoBehaviour, IInteractable
{
    public bool inverseDirection = false;
    public Quaternion rotation = Quaternion.Euler(0, 0, 0);
    [Min(0.001f)] public float duration = 0.001f;

    bool opened = false;
    bool isMoving = false;
    Quaternion baseRotation;

    public string interactionText { get { return InteractionText; } }
    [SerializeField] private string InteractionText = "Open";

    private void Start()
    {
        baseRotation = transform.localRotation;
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
            transform.rotation = opened ? Quaternion.Lerp(baseRotation * rotation, baseRotation, timer / duration) : Quaternion.Lerp(baseRotation, baseRotation * rotation, timer / duration);

            yield return null;
        }
        isMoving = false;
        opened = !opened;
    }
}
