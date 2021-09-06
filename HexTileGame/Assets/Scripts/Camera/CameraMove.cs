using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float zoomSpeed;

    float wheelInput = 0;

    void Update()
    {
        wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += Vector3.left * moveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += Vector3.right * moveSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += Vector3.forward * moveSpeed;
        }
        
        if(Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position += Vector3.back * moveSpeed;
        }

        if (wheelInput > 0)
        {
            gameObject.transform.position += Vector3.down * zoomSpeed;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 2, 5), transform.position.z);
        }
        if (wheelInput < 0)
        {
            gameObject.transform.position += Vector3.up * zoomSpeed;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 2, 5), transform.position.z);
        }
    }
}
