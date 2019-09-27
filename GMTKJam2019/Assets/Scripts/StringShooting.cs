using UnityEngine;

public class StringShooting : MonoBehaviour
{
	private Rigidbody2D rb;

	public LayerMask layerMask;

	public float stringForceStrength = 0.0f;
	private Vector3 stringPoint;
	private bool stringAttached = false;

	private float stringForceStrengthBegin = 50f;
	private float stringForceStrengthEnd = 100f;
	private float stringForceTime = 0.0f; 

	/* Polish
	 * Have a line renderer from the avatar to the connection-point
	 */

	/* Design
	 * The longer the finger has been held down, the stronger the string force is
	 * When the finger is released, the connection is severed
	 * 
	 * -
	 * 
	 * It is really annoying that it takes so long to turn the gravity around, it makes swinging hard. Maybe stop downwards momentum on attach? Or limit the distance we can get from the point, real rope like?
	 * Currently it feels very elastic, which can be nice enough if I want to feel more gooey, like feelings. But it might just be too elastic? Hard to control?
	 * Just stopping the velocity is also really hard to control.
	 * It is super easy to loose control of the horizontal speed.
	 */

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		//When we click the mouse, attach
		if (Input.GetKeyDown(KeyCode.Mouse0)) {

			//Get the direction
			Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
			Vector2 direction = (worldMousePosition - transform.position).normalized;

			//Get the raycast hit
			Debug.DrawRay(transform.position, direction * 10);
			RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, layerMask);

			//Testing indented hitting
			Vector2 point = hit2D.point;
			point -= hit2D.normal * 0.5f;

			//Attach
			stringPoint = point;
			stringAttached = true;
			stringForceStrength = stringForceStrengthBegin;
			stringForceTime = Time.time;

			rb.velocity = Vector3.zero;
		}

		//When we let go of the screen, un-attach
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			stringAttached = false;
		}

		//Increase string force
		if (stringAttached) {
			stringForceStrength = Mathf.Lerp(stringForceStrengthBegin, stringForceStrengthEnd, Time.time - stringForceTime);
		}

		//Apply string force
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
