using UnityEngine;

namespace Transform
{
    public class Transforms : MonoBehaviour
    {
        private UnityEngine.Transform _transform;

        public void SetEffectedObject(UnityEngine.Transform transform)
        {
            _transform = transform;
            foreach (var item in GetComponentsInChildren<MoveTransform>())
            {
                item.EffectedObject = transform;
            }
            foreach (var item in GetComponentsInChildren<ScaleTransform>())
            {
                item.EffectedObject = transform;
            }
            foreach (var item in GetComponentsInChildren<RotationTransform>())
            {
                item.EffectedObject = transform;
            }
        }

        private void Update()
        {
            transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z - 1);
        }
    }
}
