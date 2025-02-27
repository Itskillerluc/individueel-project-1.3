using UnityEngine;
using UnityEngine.EventSystems;

namespace Transform
{
    public class ScaleTransform : MonoBehaviour, IPointerDownHandler
    {
        public UnityEngine.Transform EffectedObject;
        public Axis Axis;
        public float Sensitivity;

        private bool _dragging;
        private Vector3 _original;
        private Camera _mainCamera;

        private void Start()
        {
            //_effectedObject = transform.parent.parent;
        }
        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && _dragging) _dragging = false;
            if (_dragging)
            {
                var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                if (Axis == Axis.Vertical)
                {
                    var scale = Mathf.Max(0.1f, EffectedObject.localScale.y * (1 - (mousePosition.y - _original.y) * 0.01f) * Sensitivity);
                    EffectedObject.localScale = new Vector3(EffectedObject.localScale.x, scale, 1);
                }
                else if (Axis == Axis.Horizontal)
                {
                    var scale = Mathf.Max(0.1f, EffectedObject.localScale.x * (1 - (mousePosition.x - _original.x) * 0.01f) * Sensitivity);
                    EffectedObject.localScale = new Vector3(scale, EffectedObject.localScale.y, 1);
                }
                else if (Axis == Axis.Both)
                {
                    var sqrt = Mathf.Sqrt(2f) * 0.5f;
                    var i = new Vector2(sqrt, sqrt);
                    var j = new Vector2(sqrt, -sqrt);
                    var col1 = mousePosition.x * i;
                    var col2 = mousePosition.y * j;
                    var rotatedMouse = col1 + col2;
                    var col3 = _original.x * i;
                    var col4 = _original.y * j;
                    var rotatedOriginal = col3 + col4;
                    var scaleX = Mathf.Max(0.1f, EffectedObject.localScale.x * (1 - (rotatedMouse.x - rotatedOriginal.x) * 0.01f) * Sensitivity);
                    var scaleY = Mathf.Max(0.1f, EffectedObject.localScale.y * (1 - (rotatedMouse.x - rotatedOriginal.x) * 0.01f) * Sensitivity);

                    EffectedObject.localScale = new Vector3(scaleX, scaleY, 1);
                }
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
                _original = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}