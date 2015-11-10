using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using revisual.pipit;

public class MouseController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IProgressCompleteReciever
{
    public BookModel model;
    public bool active = false;
    public float sensitiviy = 0.05f;

    private float _previousX;
    // private float _downX;
    private float _pageIncrease;
    private RectTransform _image;

    public void Awake()
    {
        _image = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!active) return;
        float delta = eventData.position.x - _previousX;
        _previousX = eventData.position.x;
        _pageIncrease = delta / (_image.rect.width * sensitiviy);
        model.PageIncrement(_pageIncrease);


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!active) return;
        _previousX = eventData.position.x;
        model.killVelocity();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!active) return;
        _previousX = 0;
        // _downX = 0;
    }

    public void OnProgressComplete()
    {
        active = true;
    }
}
