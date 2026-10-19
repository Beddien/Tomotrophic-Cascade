using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;

    public GameObject target;
    public GameObject followRegion;
    public int aimRadius = 100;


    // Update is called once per frame
    void Update()
    {
    


    // Basic movement along the x and z axes
    transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
	transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);

    // Follow region positioning, making sure to always keep it behind the player as the player moves
    followRegion.transform.position = new Vector3((float)(Input.GetAxis("Horizontal") * -3.25 + transform.position.x),
    transform.position.y, (float)(-1.5 + transform.position.z));

    // Target positioning
    // Draws rays from the camera and places the target on the hit point when ray collides
    // With a 'Ground' object. If the hit point is beyond the aim radius' limit,
    // Clamps the target's position to the edge of the circle.

    // BUG: right now the rays get caught on the player and tomotroph colliders. Can we make it go through them?
    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
    if (Physics.Raycast(ray, out RaycastHit hit)) 
    {
        if (hit.collider.CompareTag("Ground"))
            {
                target.GetComponent<MeshRenderer>().enabled = true;
                UnityEngine.Vector3 offset = hit.point - transform.position;
                
                UnityEngine.Vector3 clamped = UnityEngine.Vector3.ClampMagnitude(offset, aimRadius);
                target.transform.position = new Vector3(transform.position.x + clamped.x, hit.point.y, transform.position.z + clamped.z);
                
            }


        else
            {
                target.GetComponent<MeshRenderer>().enabled = false;
            }
    }
    }

}