using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayCastRaisableObject : MonoBehaviour
{
	//INSPECTOR SETTINGS
	public float Reach = 5F; // Within this radius the player is able to open/close the door.

	//PRIVATE SETTINGS
	[HideInInspector] public bool InReach;

	//DEBUGGING
	public Color DebugRayColor = Color.red;
    public string GameTagCollider;
    private GameObject hitObject;

    //START FUNCTION
    void Start()
	{

	}

	//UPDATE FUNCTION
	void Update()
	{
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0F));

		

        if (Input.GetMouseButtonDown(0))
		{
            RaycastHit hit;
                
            if (Physics.Raycast(ray, out hit, Reach) && hit.collider.tag == GameTagCollider)
            {
                Debug.Log("raisableObject hit");
                hitObject = hit.collider.gameObject;
                hitObject.transform.parent = gameObject.transform;
            }
        }
        if (Input.GetMouseButtonUp(0)) // This will release the object 
        {
            hitObject.transform.parent = null;
            hitObject = null;
        }

	}
}
