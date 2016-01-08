using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

public class IgniteTorchTrigger : MonoBehaviour {
    public GameObject KeyToActivate;

    static List<string> torches;
    private bool successTriggered;

    private const int successCount = 4;
    private Light light;
    private ParticleSystem flame;
    private FirstPersonController player;

	// Use this for initialization
	void Start () {
        torches = new List<string>();
        successTriggered = false;
        light = GetComponentInChildren<Light>();
        flame = GetComponentInChildren<ParticleSystem>(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<FirstPersonController>();
        if (player != null)
        {
            if (player.inventory.GetItem("Torch") != null)
            {
                if (!torches.Contains(tag)) torches.Add(tag);
                light.intensity = 3;
                flame.gameObject.SetActive(true);
                CheckSuccess();
            }
        }
    }

    private void CheckSuccess()
    {
        if (torches.Count == successCount)
        {
            if (!successTriggered)
            {
                Debug.Log("MISSION COMPLETE");
                successTriggered = true;
                KeyToActivate.SetActive(true);
            }
        }
    }
}
