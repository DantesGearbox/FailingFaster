using UnityEngine;

public class StringShooting : MonoBehaviour
{
	private Rigidbody2D rb;

	public LayerMask layerMask;

	public float stringForceStrength = 15;
	private Vector3 stringPoint;
	private bool stringAttached = false;

	/* The force should get stronger the closer we are to the point.
	 * Make a line renderer for the string
	 * */

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		//When we click the mouse, get the direction to the mouse
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
			Vector2 direction = (worldMousePosition - transform.position).normalized;

			//Debug.DrawRay(transform.position, direction * 10);
			RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, layerMask);

			//Testing
			Vector2 point = hit2D.point;
			point -= hit2D.normal * 0.5f;

			stringPoint = point;
			stringAttached = true;
		}

		if (stringAttached) {
			StringAttract();
		}
	}


	void StringAttract() {
		Vector2 direction = (stringPoint - transform.position).normalized;

		Debug.DrawRay(transform.position, direction * 10);
		rb.AddForce(direction * stringForceStrength);
	}
}
