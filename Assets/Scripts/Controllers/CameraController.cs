using UnityEditor;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject _camera;
    [SerializeField] public GameObject _character;
    public GameObject _guns;

    private float sens = 200;

    private float yRotation;
    private float xRotation;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _guns = GameObject.Find("Guns");
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update( )
    {
        
        
    }

    public void RotateCamera(float mouseX, float mouseY)
    {
        yRotation += mouseX * sens;
        xRotation -= mouseY * sens;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        _camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        _character.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        //_guns.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

}