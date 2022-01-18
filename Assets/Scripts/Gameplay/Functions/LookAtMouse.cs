using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask layerMask;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        AimLookAtMouse();
        //Test();
        //RotateToMousePos();
    }

    private void AimLookAtMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        {
            var direction = hit.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
            transform.forward = direction;
        }
    }
}