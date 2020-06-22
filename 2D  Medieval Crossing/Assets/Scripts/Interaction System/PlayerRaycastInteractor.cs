using UnityEngine;

public class PlayerRaycastInteractor : MonoBehaviour
{
	public InteractionUI interactionUI = null;
	public Transform startingPoint;

	public float distance = 1f;
	public KeyCode interactKey;
	public KeyCode moveKey;
	GameObject box;


	private void Start()
	{
		if (startingPoint == null)
		{
			Debug.LogError("No starting point found for the raycast : Assigning this gameObject's transform...");
			startingPoint = transform;
		}
	}


	// Update is called once per frame
	void Update()
	{
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D hit = Physics2D.Raycast(startingPoint.position, transform.GetComponent<PlayerController>().GetLastInputDir(), distance);
		Debug.DrawRay(startingPoint.position, transform.GetComponent<PlayerController>().GetLastInputDir(), Color.red);

		//Movable
		if (hit.collider != null && hit.collider.GetComponent<Movable>() != null)
		{
			if (Input.GetKeyDown(moveKey))
			{
				box = hit.collider.gameObject;
				box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
				box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
				box.GetComponent<FixedJoint2D>().enabled = true;
				box.GetComponent<Movable>().beingPushed = true;
			}
		}
		if (Input.GetKeyUp(moveKey) && box != null &&box.GetComponent<Movable>().beingPushed)
		{
			box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			box.GetComponent<FixedJoint2D>().enabled = false;
			box.GetComponent<Movable>().beingPushed = false;
			box = null;
		}

		//Interactable
		else if (hit.collider != null && hit.collider.GetComponent<IInteractable>() != null && Input.GetKeyDown(interactKey))
		{
			box = hit.collider.gameObject;
			box.GetComponent<IInteractable>().Interact();
		}
	}
}
