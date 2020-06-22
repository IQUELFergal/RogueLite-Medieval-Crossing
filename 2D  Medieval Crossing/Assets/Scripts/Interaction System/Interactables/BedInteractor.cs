using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteractor : MonoBehaviour, IInteractable
{
    public string interactionText { get { return InteractionText; } }
    [SerializeField] private string InteractionText = "Sleep";

    void IInteractable.Interact()
    {
        Debug.Log("Sleeping...");
        //Get the day night cycle, then add some time and play an animation maybe a fade
    }
}
