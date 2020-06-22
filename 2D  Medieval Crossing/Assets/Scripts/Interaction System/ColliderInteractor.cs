using UnityEngine;

public class ColliderInteractor : MonoBehaviour
{
    [SerializeField] InteractionUI interactionUI = null;
    private IInteractable currentInteractable = null;

    void Update()
    {
        CheckForInteraction();
        transform.position = transform.parent.position + (Vector3)transform.parent.GetComponent<PlayerController>().GetLastInputDir();
    }

    private void CheckForInteraction()
    {
        if (currentInteractable == null) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
            if (interactionUI != null)
            {
                interactionUI.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        currentInteractable = interactable;
        if (interactionUI != null)
        {
            interactionUI.SetText(currentInteractable.interactionText);
            interactionUI.gameObject.SetActive(true);
        }
    }
       
    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        if (interactable != currentInteractable) return;
        if (interactionUI != null)
        {
            interactionUI.gameObject.SetActive(false);
            interactionUI.SetText("");
        }
        
        currentInteractable = null;
    }
}