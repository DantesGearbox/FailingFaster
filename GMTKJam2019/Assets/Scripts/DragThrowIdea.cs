using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragThrowIdea : MonoBehaviour
{

	// When we touch, the ball gets moved to the touch-position.
	// Move finger and the ball moves with.
	// The ball keeps momentum when we lift the finger.

	Rigidbody2D rb;
	Vector3 worldMousePosition = Vector3.zero;
	Vector3 previousMousePosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		previousMousePosition = worldMousePosition;
		worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
		worldMousePosition.z = 0;

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			transform.position = worldMousePosition;
		}

		if (Input.GetKey(KeyCode.Mouse0))
		{
			Vector3 distanceMoved = worldMousePosition - previousMousePosition;

			rb.velocity = distanceMoved;
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{

		}
    }
}
