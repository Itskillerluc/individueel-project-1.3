using UnityEngine;

namespace Transform
{
    public class Transforms : MonoBehaviour
    {
        private UnityEngine.Transform _transform;

        public void SetEffectedObject(UnityEngine.Transform objectTransform)
        {
            _transform = objectTransform;
            foreach (var item in GetComponentsInChildren<MoveTransform>())
            {
                item.EffectedObject = objectTransform;
            }
            foreach (var item in GetComponentsInChildren<ScaleTransform>())
            {
                item.EffectedObject = objectTransform;
            }
            foreach (var item in GetComponentsInChildren<RotationTransform>())
            {
                item.EffectedObject = objectTransform;
            }
        }

        private void Update()
        {
            transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z - 1);
        }
    }
}
