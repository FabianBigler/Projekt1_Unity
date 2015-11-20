using UnityEngine;
using System.Collections;

public class EnterRoomTrigger : MonoBehaviour {
    public AudioSource ambienteSound;

	// Use this for initialization
	void Start () {
        ambienteSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        ambienteSound.Play();
    }
}
