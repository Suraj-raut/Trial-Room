using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CircularScrollMenu : MonoBehaviour
{
   public float rotationSpeed = 5f;

    private RectTransform scrollRect;

    [SerializeField] private DownloadAssetBundles _DownloadAssetBundles;

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

    public void OnClickItemsCategory(string _category)
    {
        Debug.Log("Category1: " + _category);
        if(_category != null && !GameManager.Instance.MenuItemsDict.ContainsKey(_category))
        { 
             Debug.Log("Category2: " + _category);
            _DownloadAssetBundles.GetTheUrlFromFirebaseServer(_category);
        }
        else
        {
            return;
           
        }
    }
}
