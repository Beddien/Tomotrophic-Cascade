using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;

    public GameObject target;
    public int aimRadius = 100;


    // Update is called once per frame
    void Update()
    {

    transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
	transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);

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