using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRubberBand : MonoBehaviour

	/* Design:
	 * The avatar hangs from a *rubber band* that the player can drag around
	 * The avatar will build up speed as the players moves the origin of the band it is tightened
	 * Let go of the band to sling the avatar
	 * Press the screen again to re-attach the avatar
	 * Hold down to dangle the player from the band
	 * IMPORTANT: The screen only moves when the player lets go of the rubber band. This forces 
	 * the player to "throw" the avatar around, in stead of just moving them with along with the rubber band
	 */

{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
