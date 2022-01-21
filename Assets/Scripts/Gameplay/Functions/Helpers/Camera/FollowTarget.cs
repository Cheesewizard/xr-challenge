using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smooth = 10f;

    void FixedUpdate()
    {
        Vector3 newPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, newPosition, smooth);

        transform.position = smoothPosition;
        transform.LookAt(target);
    }
}
