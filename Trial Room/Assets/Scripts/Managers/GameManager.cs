using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get;private set;}
    void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

   public Dictionary<string,List<Sprite>> MenuItemsDict = new Dictionary<string,List<Sprite>>();
    
    public  List<Sprite> TopWearsList = new List<Sprite>();
    public  List<Sprite> BottomWearsList = new List<Sprite>();
    public  List<Sprite> SheosList = new List<Sprite>();
    public  List<Sprite> CapsList = new List<Sprite>();

 //   public static Action OnClickCategoriesOption;
  //  public string TypeWanted = "";
}
