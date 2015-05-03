using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 1;
    public float rotSpeedVert;
    public float rotSpeedHor;

    public Transform cam;

    private void Update()
    {
        var deltaPos = speed*Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += deltaPos*cam.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= deltaPos*transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= deltaPos*cam.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += deltaPos*transform.right;
        }
        transform.eulerAngles = (transform.eulerAngles + new Vector3(0, Input.GetAxis("Mouse X")*rotSpeedHor, 0));

        cam.localEulerAngles = cam.localEulerAngles + new Vector3(-Input.GetAxis("Mouse Y")*rotSpeedVert, 0, 0);
    }
}