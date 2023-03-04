using UnityEngine;

public class NormplizeForvardLook : MonoBehaviour
{
    private Camera _mainCamera;


    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_mainCamera != null)
        {
            var cameraXTransform = _mainCamera.transform;
            var forward = transform.position - cameraXTransform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, cameraXTransform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }
}
