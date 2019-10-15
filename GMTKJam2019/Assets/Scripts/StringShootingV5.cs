using UnityEngine;

//VERSION 5: Use minimum rope length

public class StringShootingV5 : MonoBehaviour {
	private Rigidbody2D rb;
	private LineRenderer lr;

	public Transform debugObject;
	public LayerMask layerMask;

	public float minimumRopeLength = 2;
	public float ropePullOffset = 0;

	public float stringForceStrength = 0.0f;
	private Vector3 stringPoint;
	private bool stringAttached = false;

	public float stringForceStrengthBegin = 50f;
	public float stringForceStrengthEnd = 100f;
	public float stringForceTime = 0.0f;
	public float timeMaxStringStength = 1.0f;

	public float stringForceHorizontalMultiplier = 1.0f;
	public float stringForceVerticalMultiplier = 1.778f;

	public bool velocityXStopOnString = false;
	public bool velocityYStopOnString = true;

	/* Polish
	 * 
	 */

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		lr = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update() {

		//When we click the mouse, attach
		if (Input.GetKeyDown(KeyCode.Mouse0)) {

			//Get the world point of the click and the direction to the click
			Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
			Vector2 direction = (worldMousePosition - transform.position).normalized;

			//Check if we clicked a collider
			Vector2 point = Vector2.zero;
			Collider2D coll = Physics2D.OverlapPoint(worldMousePosition, layerMask);
			if (coll != null) {

				//Get the raycast hit
				RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, layerMask);

				//Testing indented hitting
				point = hit2D.point;
				point -= hit2D.normal * 0.5f;
			} else {
				point = worldMousePosition;
			}

			debugObject.position = point;
			debugObject.position += new Vector3(0, 0, -5);
			Debug.DrawRay(transform.position, direction * 10);

			lr.SetPosition(1, new Vector3(debugObject.position.x, debugObject.position.y, 0));

			//Attach
			stringPoint = point;
			stringAttached = true;
			stringForceStrength = stringForceStrengthBegin;
			stringForceTime = Time.time;

			if (velocityXStopOnString) {
				rb.velocity = new Vector2(0, rb.velocity.y);
			}
			if (velocityYStopOnString) {
				rb.velocity = new Vector2(rb.velocity.x, 0);
			}
		}

		//When we let go of the screen, un-attach
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			stringAttached = false;
		}

		//Increase string force
		if (stringAttached) {

			//Update lineRenderer position
			lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));

			stringForceStrength = Mathf.Lerp(stringForceStrengthBegin, stringForceStrengthEnd, (Time.time - stringForceTime) * timeMaxStringStength);
		}

		//Apply string force
		if (stringAttached) {
			StringAttract();
		}
	}


	void StringAttract() {
		Vector2 direction = (stringPoint - transform.position).normalized;
		float length = (stringPoint - transform.position).magnitude;

		Debug.DrawRay(transform.position, direction * 10);
		float xForce = direction.x * stringForceStrength * stringForceHorizontalMultiplier;
		float yForce = direction.y * stringForceStrength * stringForceVerticalMultiplier;

		if(length > minimumRopeLength + ropePullOffset) {
			rb.AddForce(new Vector2(xForce, yForce));
		}
	}
}
