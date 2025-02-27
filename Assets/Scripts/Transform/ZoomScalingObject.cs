using UnityEngine;

namespace Transform
{
    public class ZoomScalingObject : MonoBehaviour
    {
        public float ScaleFactor;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            transform.localScale = new Vector3(_camera.orthographicSize, _camera.orthographicSize, 1) * ScaleFactor;
        }
    }
}
