using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public GameObject parent;

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
    }

    private void OnClick()
    {
        foreach (var item in parent.GetComponentsInChildren<ChoiceButton>())
        {
            item.GetComponent<Button>().interactable = true;
        }
        GetComponent<Button>().interactable = false;
    }
}
