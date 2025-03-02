using DG.Tweening;
using Transform;
using UnityEngine;
using UnityEngine.EventSystems;

public class Prop : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TransformHandles;
    public GameObject TransformHandlesInstance;

    private const float ScrollWidth = 200;

    private bool _dragging;
    private bool _hover;
    private bool _selected;
    private Vector3 _offset;
    private static Camera _mainCamera;

    private Tween _animation;

    private Room _room;

    public void SetDragging()
    {
        _dragging = true;
    }

    private void Start()
    {
        _room = FindAnyObjectByType<Room>();
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            _selected = true;
            var outline = GetComponent<SpriteOutline>();
            if (TransformHandlesInstance == null)
            {
                TransformHandlesInstance = Instantiate(TransformHandles, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.01f), Quaternion.identity);
            }

            TransformHandlesInstance.GetComponent<Transforms>().SetEffectedObject(transform);

            outline.enabled = true;
        }
        else
        {
            _selected = false;
            var outline = GetComponent<SpriteOutline>();
            if (TransformHandlesInstance != null) Destroy(TransformHandlesInstance);
            outline.enabled = false;
        }
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && _hover) MouseUp();
        if (!_dragging) return;
        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x + _offset.x, mousePosition.y + _offset.y, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _dragging = true;
            _offset = transform.position - _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void MouseUp()
    {
        _dragging = false;
        if (DestroyIfOutside()) return;
        transform.position = new Vector3(transform.position.x, transform.position.y, 100);
        if (_selected) return;
        BobAnimation();
    }

    private void OnDestroy()
    {
        if (TransformHandlesInstance != null)
        {
            Destroy(TransformHandlesInstance);
        }
    }

    private bool DestroyIfOutside()
    {
        
        var trueMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);


        if (Input.mousePosition.x < ScrollWidth ||
            trueMousePosition.x < -(_room.width / 2) ||
            trueMousePosition.x > _room.width / 2 ||
            trueMousePosition.y < -(_room.height / 2) ||
            trueMousePosition.y > _room.height / 2)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    private void BobAnimation()
    {
        var sequence = DOTween.Sequence();
        _animation?.Kill();
        var scale = transform.localScale;
        _animation = this.transform.DOScale(0.9f * scale, 0.1f);
        _animation.SetEase(Ease.Linear);
        sequence.Append(_animation);
        _animation = this.transform.DOScale(1f * scale, 0.1f);
        _animation.SetEase(Ease.Linear);
        sequence.Append(_animation);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            foreach (var prop in FindObjectsByType<Prop>(FindObjectsSortMode.None))
            {
                if (prop == this) continue;
                prop.SetSelected(false);
            }
            SetSelected(true);
            BobAnimation();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hover = false;
    }
}