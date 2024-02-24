using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemInfoPanelScript : MonoBehaviour
{
   public Image ItemImage;

   public void CloseTheItemPrefab()
   {
      Destroy(this.gameObject);

   }
}
