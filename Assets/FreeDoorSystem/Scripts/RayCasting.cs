using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayCasting : MonoBehaviour
{
	//INSPECTOR SETTINGS
	public float Reach = 5F; // Within this radius the player is able to open/close the door.

	//PRIVATE SETTINGS
	[HideInInspector] public bool InReach;

	//DEBUGGING
	public Color DebugRayColor = Color.red;
    Animator animator;
    bool jailDoorOpen;

	//START FUNCTION
	void Start()
	{

	}

	//UPDATE FUNCTION
	void Update()
	{
		// Set origin of ray to 'center of screen' and direction of ray to 'cameraview'.
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0F));

		RaycastHit hit; // Variable reading information about the collider hit.

		// Cast a ray from the center of screen towards where the player is looking.
		if (Physics.Raycast (ray, out hit, Reach) && hit.collider.tag == "Door")
		{
			InReach = true;

			if (Input.GetKey(KeyCode.E))
			{
                // Give the object that was hit the name 'Door'.
                //GameObject Door = hit.transform.gameObject;

                // Get access to the 'DoorOpening' script attached to the door that was hit.
                //DoorOpening dooropening = Door.GetComponent<DoorOpening>();

                DoorOpening dooropening = hit.transform.gameObject.GetComponent<DoorOpening>();

                    // Check whether the door is opening/closing or not.
                    if (dooropening.Running == false)
                    {
                        if(dooropening.Unlock)
                        {
                           StartCoroutine(dooropening.Open());
                        }
                    }
			}
		}
        else if (Physics.Raycast(ray, out hit, Reach) && hit.collider.tag == "JailDoor")
        {
            InReach = true;

            if (Input.GetKey(KeyCode.E))
            {
                animator = hit.transform.gameObject.GetComponent<Animator>();
                animator.SetTrigger("Open");
                //Debug.Log("Jaildoor activeted");
            }
        }

        else InReach = false;

		//DEBUGGING
		//if (InReach == true) print ("The player is able to open the door (Inreach = " + InReach + ").");
		//else print ("The player is not able to open the door (Inreach = " + InReach + ").");

		// Draw the ray as a colored line for debugging purposes.
		Debug.DrawRay (ray.origin, ray.direction*Reach, DebugRayColor);
	}
    //GUI FUNCTION
    void OnGUI()
    {
        if (InReach == true)
        {
            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 25), "Press 'E' to open/close");
        }
    }
}
