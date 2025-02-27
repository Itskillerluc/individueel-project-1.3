using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCatcher : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            foreach (var prop in FindObjectsByType<Prop>(FindObjectsSortMode.None))
            {
                prop.SetSelected(false);
            }  
        }
    }
}
