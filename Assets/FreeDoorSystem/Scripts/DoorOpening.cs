////////////////////////////////////
//Last edited by: Alexander Ameye //
//on: Friday, 14/08/2015          //
////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class DoorOpening : MonoBehaviour {

	//INSPECTOR SETTINGS
	public enum SideOfHinge
	{
		Left,
		Right
	}
	public SideOfHinge HingeSide;

	public enum DoorSwingSide
	{
		Left,
		Right
	}
	public DoorSwingSide SwingSide;

	public float Angle = 90.0F; // Only use positive angles <180°.
	public float Speed = 3F; // Opening/closing speed of the door.
	public int TimesOpenable = 0; // 0 = infinite.
    public string AcceptKeyTag;

	//PRIVATE SETTINGS
	private int n = 0;
	[HideInInspector] public bool Running = false;
    private GameObject player;
    private FirstPersonController playerController;

    // Define two end rotations for state 0 and 1.
    private Quaternion EndRot1, EndRot2;
	private int State;

	// Create a hinge.
	GameObject hinge;

	//START FUNCTION
	void Start ()
	{
		// Create a hinge.
		hinge = new GameObject();
		hinge.name = "hinge";

        player  = GameObject.Find("Player");
        playerController = player.GetComponent<FirstPersonController>();

        // Calculate Cosine and Sine of initial angle.
        float CosDeg = Mathf.Cos ((transform.eulerAngles.y * Mathf.PI) / 180);
		float SinDeg = Mathf.Sin ((transform.eulerAngles.y * Mathf.PI) / 180);

		// Set side of hinge left.
		if (HingeSide == SideOfHinge.Left)
		{
			float PosDoorX = transform.position.x;
			float PosDoorY = transform.position.y;
			float PosDoorZ = transform.position.z;

			float ScaleDoorX = transform.localScale.x;

			//Make copy of hinge's position.
			Vector3 HingePosCopy = hinge.transform.position;

			// Math.
			HingePosCopy.x = (PosDoorX - (ScaleDoorX / 2 * CosDeg));
			HingePosCopy.z = (PosDoorZ + (ScaleDoorX / 2 * SinDeg));
			HingePosCopy.y = PosDoorY;

			// Set the hinge to be exactly in the left-under corner of the door.
			hinge.transform.position = HingePosCopy;
		}

		// Set side of hinge right.
		if (HingeSide == SideOfHinge.Right)
		{
			float PosDoorX = transform.position.x;
			float PosDoorY = transform.position.y;
			float PosDoorZ = transform.position.z;

			float ScaleDoorX = transform.localScale.x;

			//Make copy of hinge's position.
			Vector3 HingePosCopy = hinge.transform.position;

			// Math.
			HingePosCopy.x = (PosDoorX + (ScaleDoorX / 2 * CosDeg));
			HingePosCopy.z = (PosDoorZ - (ScaleDoorX / 2 * SinDeg));
			HingePosCopy.y = PosDoorY;

			// Set the hinge to be exactly in the right-under corner of the door.
			hinge.transform.position = HingePosCopy;
		}

		// Make the hinge the parent of the door.
		transform.parent = hinge.transform;

		//USER ERROR CODES
		if(Angle == 180 || Angle < 0)
		{
			UnityEditor.EditorUtility.DisplayDialog ("Error 001", "Angle value can't exceed 180 degrees or be negative", "Ok", "");
			UnityEditor.EditorApplication.isPlaying = false;
		}

		// Make sure the door opens correctly when using different swingsides.
		if (SwingSide == DoorSwingSide.Left)
		{
			Angle = -Angle;
		}

		//Set 'EndRot1' to be the rotation when door is moved.
		EndRot1 = Quaternion.Euler(0, Angle, 0);

		//Set 'EndRot2' to be rotation when door is not yet moved.
		EndRot2 = Quaternion.Euler (0, transform.rotation.y, 0);

	}

	//UPDATE FUNCTION
	void Update ()
	{
		//DEBUGGING (TEST)
		//if (Running == true) print ("The door is opening/closing (Running = " + Running + ")");
		//else print ("The door is not opening/closing (Running = " + Running + ")");
	}

    public bool Unlock
    {
        get
        {
            if (String.IsNullOrEmpty(AcceptKeyTag)) return true;

            var item = playerController.inventory.GetItem(AcceptKeyTag);   
            if(item == null)
            {
                playerController.LoadQuest(2);
                return false;
            } else
            {
                playerController.inventory.RemoveItem(AcceptKeyTag);
                return true;
            }    
        }
    }

	//OPEN FUNCTION
	public IEnumerator Open ()
    {
        if (n < TimesOpenable || TimesOpenable == 0)
		{
			if (hinge.transform.rotation == (State == 0 ? EndRot1 : EndRot2))
			{
				// Change state from 1 to 0 and back (= change from Endrot1 to EndRot2).
				State ^= 1;
			}

			// Set 'finalRotation' to 'Endrot1' when moving and to 'EndRot2' when moving back.
    	Quaternion finalRotation = ((State == 0) ? EndRot1 : EndRot2);

    	// Make the door rotate until it is fully opened/closed.
    	while (Mathf.Abs(Quaternion.Angle(finalRotation, hinge.transform.rotation)) > 0.01f)
    	{
				Running = true;

    		hinge.transform.rotation = Quaternion.Lerp (hinge.transform.rotation, finalRotation, Time.deltaTime * Speed);
      	yield return new WaitForEndOfFrame();
    	}

			Running = false;

			if(TimesOpenable == 0)
			{
				n = 0;
			}

			else n++;

			//DEBUGGING (TEST)
			//print("Door was moved " + n + " times.");
		}
	}

	//GUI FUNCTION
	void OnGUI ()
	{
       // Player.GetComponent
		RayCasting raycasting = player.GetComponent<RayCasting>();

		if (raycasting.InReach == true)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 25), "Press 'E' to open/close");
		}
	}
}
