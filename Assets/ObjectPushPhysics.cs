using UnityEngine;
using System.Collections;

public class ObjectPushPhysics : MonoBehaviour
{

    public float pushForce = 2.0f;
    public string gameTagCollider = "pushable";

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == gameTagCollider)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            //checking whether rigidbody is either non-existant or kinematic
            if (body == null || body.isKinematic)
                return;

            if (hit.moveDirection.y < -.3f)
                return;

            //set up push direction for object
            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            //apply push force to object
            body.velocity = pushForce * pushDirection;
        }
    }
}
