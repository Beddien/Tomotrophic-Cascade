using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 offsetFromParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        mainCamera = Camera.main;
        offsetFromParent = transform.localPosition;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = transform.parent.position + offsetFromParent;

        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        directionToCamera.y = 0;

        if (directionToCamera == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(-directionToCamera, Vector3.up);
        transform.rotation = targetRotation;
    }
}
