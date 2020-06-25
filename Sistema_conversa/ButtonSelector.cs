using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonSelector : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    ConversationHandler ch;
    private void Start()
    {
        ch = GameObject.Find("ConversationHandler").GetComponent<ConversationHandler>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ch.SetPensamento(this.gameObject);

    }
    public void OnSelect(BaseEventData eventData)
    {
        ch.SetPensamento(this.gameObject);
    }
}

