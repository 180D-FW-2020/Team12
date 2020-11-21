using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public float distance;
    public float height;
    public GameObject objectToFollow;

    // Update is called once per frame
    void LateUpdate() //after all updates that late update
    {
        if(objectToFollow == null)
            return;

        Vector3 destination = objectToFollow.transform.position;
        destination.x = 0f;
        destination.y += height;
        destination.z += distance;

        transform.position = destination;

    }
}
