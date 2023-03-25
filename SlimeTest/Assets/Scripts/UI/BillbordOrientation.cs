using UnityEngine;
/// <summary>
/// поворачивать любой объект к камере передней стороной
/// </summary>
public class BillbordOrientation : MonoBehaviour
{
    private Camera _mainCamera;


    private void Start()
    {
        _mainCamera = Camera.main;

        if (!_mainCamera)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        var cameraXTransform = _mainCamera.transform;
        var forward = transform.position - cameraXTransform.position;
        forward.Normalize();
        var up = Vector3.Cross(forward, cameraXTransform.right);
        transform.rotation = Quaternion.LookRotation(forward, up);
    }
}
