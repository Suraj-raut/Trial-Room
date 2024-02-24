using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CircularScrollMenu : MonoBehaviour
{
   public float rotationSpeed = 5f;

    private RectTransform scrollRect;

    void Start()
    {
        scrollRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate rotation based on touch movement
        float rotation = -eventData.delta.x * rotationSpeed;

        // Rotate the scroll view
        scrollRect.Rotate(Vector3.forward, rotation);
    }
}
