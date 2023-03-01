using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {
        
            _mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_mainCamera != null)
        {
            var cameraXTransform = _mainCamera.transform;
            var forward = transform.position - cameraXTransform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, cameraXTransform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
           // _heathUpText.transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }
}
