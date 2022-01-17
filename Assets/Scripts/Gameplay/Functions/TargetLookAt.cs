using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLookAt : MonoBehaviour
{
    public GameObject target;
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        AimLookAtMouse();
    }

    private void AimLookAtMouse()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Always looking directly at the target
        transform.rotation = rotation;

        // Nice slow movemewnt effect between rotations
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
