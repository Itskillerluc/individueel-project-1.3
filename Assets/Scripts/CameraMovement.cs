using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera _camera;
    public float panSensitivity = 0.5f;

    private Vector3 _dragOrigin;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        PanCameraMouse();
        PanCameraButtons();
        ZoomCameraMouse();
        ZoomCameraButtons();
    }

    private void PanCameraButtons()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _camera.transform.position += Vector3.up * panSensitivity;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _camera.transform.position += Vector3.down * panSensitivity;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _camera.transform.position += Vector3.left * panSensitivity;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _camera.transform.position += Vector3.right * panSensitivity;
        }
    }

    private void PanCameraMouse()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _dragOrigin = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 difference = _dragOrigin - _camera.ScreenToWorldPoint(Input.mousePosition);
            _camera.transform.position += difference;
        }
    }

    private void ZoomCameraButtons()
    {
        if (Input.GetKey(KeyCode.Minus))
        {
            _camera.orthographicSize = Mathf.Max(1, _camera.orthographicSize + 0.2f);
        }
        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.Equals))
        {
            _camera.orthographicSize = Mathf.Max(1, _camera.orthographicSize - 0.2f);
        }
    }

    private void ZoomCameraMouse()
    {
        var beforeMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        var scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta != 0)
        {
            _camera.orthographicSize = Mathf.Max(1, _camera.orthographicSize - scrollDelta);
            var afterMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _camera.transform.position += beforeMousePosition - afterMousePosition;
        }
    }
}
