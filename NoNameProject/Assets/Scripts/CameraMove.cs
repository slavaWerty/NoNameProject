using Interfaces;
using UnityEngine;

public class CameraMove : IInitzializable
{
    private Transform _cameraTransform;

    private Vector3 _nowMousePos;
    private Vector3 _lastMousePos;

    public CameraMove(Transform cameraTransform) => _cameraTransform = cameraTransform;

    public void Initzialize() => _lastMousePos = Input.mousePosition;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _lastMousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            _nowMousePos = Input.mousePosition;

            _cameraTransform.position += _cameraTransform.right * (_lastMousePos.x - _nowMousePos.x) * Time.deltaTime;
            _cameraTransform.position += _cameraTransform.up * (_lastMousePos.y - _nowMousePos.y) * Time.deltaTime;

            _lastMousePos = Input.mousePosition;
        }
    }
}