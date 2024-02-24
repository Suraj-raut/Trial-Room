using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

   // public Dictionary<string,List<Sprite>> MenuItemsDict = new Dictionary<string,List<Sprite>>();
    public  List<Sprite> MenuItemsList = new List<Sprite>();    
}
