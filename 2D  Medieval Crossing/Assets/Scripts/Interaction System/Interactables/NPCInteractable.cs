using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    public string interactionText { get { return InteractionText; } }
    [SerializeField] private string InteractionText = "Talk";

    void IInteractable.Interact()
    {
        Debug.Log("Chatting...");
    }
}
