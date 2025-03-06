using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuObject : MonoBehaviour
{
    public bool isBackground;
    private Tween _animation;
    
    private void Start()
    {
        var eventTrigger = gameObject.AddComponent<EventTrigger>();
        var clickTrigger = new EventTrigger.TriggerEvent();
        clickTrigger.AddListener(_ => OnClick());
        eventTrigger.triggers.Add(new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown,
            callback = clickTrigger
        });
        var enterTrigger = new EventTrigger.TriggerEvent();
        enterTrigger.AddListener(_ => MouseEnter());
        eventTrigger.triggers.Add(new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter,
            callback = enterTrigger
        });
        var exitTrigger = new EventTrigger.TriggerEvent();
        exitTrigger.AddListener(_ => MouseExit());
        eventTrigger.triggers.Add(new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit,
            callback = exitTrigger
        });
    }

    public void OnClick()
    {
        var propList = GetComponentInParent<PropList>();
        var childrenList = propList.GetComponentsInChildren<MenuObject>();
        var index = Array.IndexOf(childrenList, this);
        var instance = Instantiate(propList.prefabs[index]);
        propList.AddObject(instance, isBackground);

        if (_animation != null)
        {
            _animation.Kill();
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void MouseEnter()
    {
        if (_animation != null) _animation.Kill();
        _animation = this.transform.DOScale(1.2f, 0.4f);
    }

    public void MouseExit()
    {
        if (_animation != null) _animation.Kill();
        _animation = this.transform.DOScale(1f, 0.4f);
    }
}
