using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDistance = 2.0f;
    [SerializeField] private float _maxDistance = 10.0f;
    [SerializeField] private float _sensitivity = 180.0f;

    private float _distance = 5.0f;
    private float _rotationX = 0.0f;
    private float _rotationY = 30.0f;

    void LateUpdate()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _distance -= scroll * 5.0f;
        _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;

            float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

            _rotationX += mouseX;

            _rotationY -= mouseY;
            _rotationY = Mathf.Clamp(_rotationY, 10f, 80f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        Quaternion rotation = Quaternion.Euler(_rotationY, _rotationX, 0);
        transform.position = _target.position + _target.up - (rotation * Vector3.forward * _distance);

        transform.LookAt(_target.position + _target.up);
    }

}
