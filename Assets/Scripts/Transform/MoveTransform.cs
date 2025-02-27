using UnityEngine;
using UnityEngine.EventSystems;

namespace Transform
{
    public class MoveTransform : MonoBehaviour, IPointerDownHandler
    {
        public UnityEngine.Transform EffectedObject;
        public Axis Axis;

        private bool _dragging;
        private Vector3 _offset;
        private Camera _mainCamera;
        private Vector2 _objectSize;

        private Room _room;


        private void Start()
        {
            _room = FindAnyObjectByType<Room>();
            _objectSize = EffectedObject.GetComponent<SpriteRenderer>().size;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && _dragging) _dragging = false;
            if (!_dragging) return;
            var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            var rotation = EffectedObject.rotation.eulerAngles.z;
            
            var height = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * rotation) * _objectSize.y) + Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * rotation) * _objectSize.x);
            var width = Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * rotation) * _objectSize.y) + Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * rotation) * _objectSize.x);
            
            if (Axis == Axis.Vertical)
            {
                EffectedObject.position = new Vector3(EffectedObject.position.x, Mathf.Clamp(mousePosition.y + _offset.y, -(_room.height / 2) + height / 2, _room.height / 2 - height / 2), EffectedObject.position.z);
            }
            else if (Axis == Axis.Horizontal)
            {
                EffectedObject.position = new Vector3(Mathf.Clamp(mousePosition.x + _offset.x, -(_room.width / 2) + width / 2, _room.width / 2 - width / 2), EffectedObject.position.y, EffectedObject.position.z);
            }
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _dragging = true;
                _offset = EffectedObject.position - _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}
