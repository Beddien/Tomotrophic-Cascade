using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;

    public GameObject target;
    public int aimRadius = 5;



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
                if (Math.Abs(hit.point.x) - Math.Abs(transform.position.x) < aimRadius && Math.Abs(hit.point.z) - Math.Abs(transform.position.z) < aimRadius)
                {
                    target.GetComponent<MeshRenderer>().enabled = true;
                    target.transform.position = hit.point;
                }
            
            else
                {
                    target.GetComponent<MeshRenderer>().enabled = true;
                    double scalar = Math.Sqrt(hit.point.x * hit.point.x + hit.point.z * hit.point.z)/aimRadius;
                    target.transform.position = new Vector3((float)(hit.point.x / scalar), hit.point.y, (float)(hit.point.z / scalar));
                }
            }


        Debug.Log("ray hitting object");
    }
    }
}