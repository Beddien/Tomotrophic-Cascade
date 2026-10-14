using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;

    public GameObject target;



    // Update is called once per frame
    void Update()
    {

    transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
	transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);

    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
    if (Physics.Raycast(ray, out RaycastHit hit)) 
    {
        //target.SetActive;
        //target.transform.position = hit.point;
        Debug.Log("ray hitting object");
    }
    }
}