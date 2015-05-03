using UnityEngine;

[RequireComponent(typeof (Camera))]
public class Wireframe : MonoBehaviour
{
    private bool _isOn;
    public GameObject PlayLight;
    public GameObject WireLight;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isOn = !_isOn;
            GL.wireframe = _isOn;

            GetComponent<Camera>().clearFlags = _isOn ? CameraClearFlags.SolidColor : CameraClearFlags.Skybox;
            WireLight.SetActive(_isOn);
            PlayLight.SetActive(!_isOn);
        }
    }
}