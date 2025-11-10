using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColliderButton : MonoBehaviour, IPointerEnterHandler
{
    private Image _Image;
    private EventTrigger _eventtrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Image = GetComponent<Image>();
        _eventtrigger = GetComponent<EventTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _eventtrigger.enabled = false;
    }
}
