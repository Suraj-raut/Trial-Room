using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManger : MonoBehaviour
{
   [SerializeField] private GameObject MenuItemPrefab;
   [SerializeField] private GameObject ScrollContent;
   [SerializeField] private MenuItemInfoPanelScript MenuItemInfo;
   [SerializeField] private Transform Canvas;



    // Start is called before the first frame update
    void Start()
    {
      // PopulateTheMenuItems();
    }

    public void PopulateTheMenuItems()
    {  
      Debug.Log("PopulaPopulateTheMenuItemste");
       foreach(Sprite sprite in GameManager.Instance.MenuItemsList)
       {
          GameObject newItem = Instantiate<GameObject>(MenuItemPrefab, ScrollContent.transform);
          newItem.GetComponent<Image>().sprite = sprite;

          var _button = newItem.GetComponent<Button>();
          
          _button.onClick.AddListener(()=>
          {
           
                MenuItemInfoPanelScript MenuItemInfoScript = Instantiate<MenuItemInfoPanelScript>(MenuItemInfo, Canvas.transform);
                MenuItemInfoScript.ItemImage.sprite = sprite;
            
               Debug.Log("OpenItemInfo --> Show item details");

          } );

       }

    }

   //  public void OpenItemInfo()
   //  {
   //    if(MenuItemInfoPrefab != null)
   //    {
   //       Instantiate(MenuItemInfoPrefab, new Vector3(0, 0, 0), Quaternion.identity);
   //    }
   //    Debug.Log("OpenItemInfo --> Show item details");
   //  }  
}
