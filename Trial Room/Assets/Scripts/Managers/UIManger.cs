using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UIManger : MonoBehaviour
{
   [SerializeField] private GameObject MenuItemPrefab;
   [SerializeField] private GameObject TopWearsContent;
   [SerializeField] private GameObject BottomWearsContent;
   [SerializeField] private GameObject SheosContent;
   [SerializeField] private GameObject CapsContent;
   [SerializeField] private MenuItemInfoPanelScript MenuItemInfo;
   [SerializeField] private Transform Canvas;



    // Start is called before the first frame update
    void Start()
    {
      // PopulateTheMenuItems();
    }

    public void PopulateTheMenuItems(string _TypeWanted)
    {  
      Debug.Log("PopulaPopulateTheMenuItemste");
      List<Sprite> TempList = new List<Sprite>();
      
      var data = GetTheDesiredCategoryList(_TypeWanted);

      TempList = data.Item1;
      GameObject ScrollContent = data.Item2;
       foreach(Sprite sprite in TempList)
       {
         Debug.Log(sprite);
         Debug.Log(TempList.Count);
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

   
   private Tuple<List<Sprite>, GameObject> GetTheDesiredCategoryList(string _TypeWanted)
   {
      List<Sprite> TempList = new List<Sprite>();
      var result = Tuple.Create(TempList, TopWearsContent); 

      if(_TypeWanted == "TopWears")
      {
         if (GameManager.Instance.MenuItemsDict.TryGetValue(_TypeWanted, out TempList)) 
         {
            result = Tuple.Create(TempList, TopWearsContent);
            return result; 
         }
        // else { Debug.Log(_TypeWanted + "not present in the Dictionary");}

      }

      else if(_TypeWanted == "BottomWears")
      {
         if (GameManager.Instance.MenuItemsDict.TryGetValue(_TypeWanted, out TempList)) 
         {
            result = Tuple.Create(TempList, BottomWearsContent);
            return result;
         }
       //  else { Debug.Log(_TypeWanted + "not present in the Dictionary");}

      }
      else if(_TypeWanted == "Sheos")
      {
         if (GameManager.Instance.MenuItemsDict.TryGetValue(_TypeWanted, out TempList)) 
         {
            result = Tuple.Create(TempList, SheosContent);
            return result;
         }
        // else { Debug.Log(_TypeWanted + "not present in the Dictionary");}

      }
      else if(_TypeWanted == "Caps")
      {
         if (GameManager.Instance.MenuItemsDict.TryGetValue(_TypeWanted, out TempList)) 
         {
            result = Tuple.Create(TempList, CapsContent);
            return result;
         }
        // else { Debug.Log(_TypeWanted + "not present in the Dictionary");}

      }

            result = Tuple.Create(TempList, TopWearsContent);
            return result;
 
   }

  
}
