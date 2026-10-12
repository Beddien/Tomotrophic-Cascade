using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;





    // Update is called once per frame
    void Update()
    {

    transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
	transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);




    }
}