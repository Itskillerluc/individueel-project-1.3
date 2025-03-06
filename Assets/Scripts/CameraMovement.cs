using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float ScrollWidth = 200;
    
    public Camera _camera;
    public float panSensitivity = 0.5f;

    private Vector3 _dragOrigin;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        var room = RoomSingleton.Instance.Room;
        _camera.orthographicSize = Mathf.Max(room.height, room.width / 1.2f);
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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _camera.transform.position += Vector3.up * panSensitivity;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _camera.transform.position += Vector3.down * panSensitivity;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _camera.transform.position += Vector3.left * panSensitivity;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
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
        if (Input.mousePosition.x < ScrollWidth) return;
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
