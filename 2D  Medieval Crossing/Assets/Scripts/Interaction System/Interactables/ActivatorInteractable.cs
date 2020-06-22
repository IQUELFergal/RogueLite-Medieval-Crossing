using UnityEngine;

public class ActivatorInteractable : MonoBehaviour, IInteractable
{
    public string interactionText { get { return InteractionText; } }
    [SerializeField] private string InteractionText = "Activate";

    public GameObject[] interactables;

    void IInteractable.Interact()
    {
        for (int i = 0; i < interactables.Length; i++)
        {
            if (interactables[i].GetComponent<IInteractable>() != null)
            {
                interactables[i].GetComponent<IInteractable>().Interact();
            }
        }
    }
}
