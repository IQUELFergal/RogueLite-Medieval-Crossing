using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerInteractable : MonoBehaviour, IInteractable
{
    public string interactionText { get { return InteractionText; } }
    [SerializeField] private string InteractionText = "Open";

    void IInteractable.Interact()
    {
        Debug.Log("Opening...");
    }
}
