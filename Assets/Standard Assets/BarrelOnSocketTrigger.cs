using UnityEngine;
using System.Collections;

public class BarrelOnSocketTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("raisableObject"))
        {

        }

    }
}
