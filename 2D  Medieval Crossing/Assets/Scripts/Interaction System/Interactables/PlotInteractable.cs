using UnityEngine;

[RequireComponent(typeof(Plot))]
public class PlotInteractable : MonoBehaviour, IInteractable
{
    public string interactionText { get { return InteractionText; } }
    [SerializeField] private string InteractionText = "";


    Plot plot;

    void Start()
    {
        plot = GetComponent<Plot>();
    }

    void Update()
    {
        if (plot.seed != null)
        {
            InteractionText = "Harvest";
        }
        else InteractionText = "Plant";
    }

    void IInteractable.Interact()
    {
        if (plot.seed != null)
        {
            plot.Harvest();
        }
        //else plot.Plant();
        
    }
}
