using UnityEngine;

public class BillbordOrientation : MonoBehaviour
{
    private Camera _mainCamera;


    private void Start()
    {
        _mainCamera = Camera.main;

        if (!_mainCamera)
        {
            Debug.LogError("" + nameof(BillbordOrientation) + " " + nameof(Start) + " " + nameof(_mainCamera) + "is null");
            enabled = false;
        }
    }

    private void Update()
    {
        var cameraXTransform = _mainCamera.transform;
        var forward = (transform.position - cameraXTransform.position).normalized;
        var up = Vector3.Cross(forward, cameraXTransform.right);
        transform.rotation = Quaternion.LookRotation(forward, up);
    }
}
