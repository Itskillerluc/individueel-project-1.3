using UnityEngine;
using UnityEngine.EventSystems;
namespace Transform
{
    public class RotationTransform : MonoBehaviour, IPointerDownHandler
    {
        public UnityEngine.Transform EffectedObject;

        private bool _dragging;
        private Vector3 _original;
        private Quaternion _originalRotation;
        private Camera _mainCamera;

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && _dragging) _dragging = false;
            if (!_dragging) return;
            var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            EffectedObject.rotation = Quaternion.Euler(0, 0, _originalRotation.eulerAngles.z + Vector2.SignedAngle(_original - transform.parent.position, mousePosition - transform.parent.position));
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _dragging = true;
            _original = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _originalRotation = EffectedObject.rotation;
        }
    }
}
