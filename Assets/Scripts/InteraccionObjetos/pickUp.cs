using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    public float pickUpRange = 20f;
    public Transform holdingPosition;
    private GameObject heldObject;
    private bool isHolding = false;
    private Ray ray;
    public Camera cam;
    
    void Update()
    {
        ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * pickUpRange, Color.red);
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isHolding)
            {
                ReleaseObject();
            }
            else
            {
                TryPickUpObject();
            }
        }
    }

    void TryPickUpObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpRange)) // El out es un puntero
        {
            if (hit.collider.CompareTag("cogible"))
            {
                heldObject = hit.collider.gameObject;
                heldObject.transform.position = holdingPosition.position;
                heldObject.transform.parent = holdingPosition;
                isHolding = true;
            }
        }
    }

    void ReleaseObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.parent = null;
            isHolding = false;
            heldObject = null;
        }
    }
}