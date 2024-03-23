using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

public class DownloadAssetBundles : MonoBehaviour
{
    public UIManger uiManager;
    public bool isDownloaded;
    private FirebaseStorage storage;
    private StorageReference storageReference;

   // private AssetBundleRequest asset;

    enum CategoryWanted
    {
        TopWears,
        BottomWears,
        Sheos,
        Caps
    }
    // Start is called before the first frame update
    void Awake()
    {
        isDownloaded = false; 

    }


    public void GetTheUrlFromFirebaseServer(string _TypeWanted)
    {

        Debug.Log("GetTheUrlFromFirebaseServer 1--->" + _TypeWanted);
       if(GameManager.Instance.MenuItemsDict.ContainsKey(_TypeWanted))
       {
             uiManager.PopulateTheMenuItems(_TypeWanted);

       }
       else
       {

        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://trial-room-59e19.appspot.com");
      //  string TypeWanted = GameManager.TypeWanted;
       // StorageReference AllItemsAssetBundle = storageReference.Child("clothingmenuicons");
       string _category = "/MenuItemsCategories" + "/" + _TypeWanted + "/" + _TypeWanted.ToLower();

        StorageReference AllItemsAssetBundle = storageReference.Child(_category);


        AllItemsAssetBundle.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if(!task.IsFaulted && !task.IsCanceled)
            {
               StartCoroutine(DownloadAssetBundlesFromServer(Convert.ToString(task.Result), _TypeWanted));
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });

        }
      

    }

    private IEnumerator DownloadAssetBundlesFromServer(string downloadUrl, string _TypeWanted)
    {
       
       // string url = "https://drive.usercontent.google.com/u/0/uc?id=1rK2ZudWDo4-eNYRaDbO_jvaIyyLczFa5&export=download";
     
        using(UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(downloadUrl))
        {
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error occured while downloading at: " + downloadUrl + " " + www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                string[] names = bundle.GetAllAssetNames();

                    foreach (string spriteName in names)
                    {
                        AssetBundleRequest asset = bundle.LoadAssetWithSubAssetsAsync<Sprite>(spriteName);
                        Debug.Log(" DownloadAssetBundlesFromServer : Sprites name : " + spriteName);
                        yield return asset; 
   
                   if(!isDownloaded)
                   {
                           foreach (Sprite sprite in asset.allAssets)
                           {
                             if(_TypeWanted == CategoryWanted.TopWears.ToString() && 
                                 GameManager.Instance.TopWearsList.Count != names.Length)
                             { 
                                 GameManager.Instance.TopWearsList.Add(sprite);
                             }
                             else if(_TypeWanted == CategoryWanted.BottomWears.ToString() && 
                                      GameManager.Instance.BottomWearsList.Count != names.Length)
                             {
                                GameManager.Instance.BottomWearsList.Add(sprite);
                             }
                             else if(_TypeWanted == CategoryWanted.Caps.ToString() &&
                                      GameManager.Instance.CapsList.Count != names.Length)
                             {
                                GameManager.Instance.CapsList.Add(sprite);
                             }
                             else if(_TypeWanted == CategoryWanted.Sheos.ToString() &&
                                     GameManager.Instance.SheosList.Count != names.Length)
                             {
                                 GameManager.Instance.SheosList.Add(sprite);
                             }

                             Debug.Log("Asset downloaded1 : The TopWears list has : " + GameManager.Instance.TopWearsList.Count);                         

                           }
                   }


                          switch(_TypeWanted)
                          {
                            case "TopWears":
                            if(!GameManager.Instance.MenuItemsDict.ContainsKey(CategoryWanted.TopWears.ToString()))
                                 { GameManager.Instance.MenuItemsDict.Add(CategoryWanted.TopWears.ToString(), GameManager.Instance.TopWearsList); }
                                  break;
                            case "BottomWears":
                            if(!GameManager.Instance.MenuItemsDict.ContainsKey(CategoryWanted.BottomWears.ToString()))
                                 {  GameManager.Instance.MenuItemsDict.Add(CategoryWanted.BottomWears.ToString(), GameManager.Instance.BottomWearsList);}
                                  break;
                            case "Caps":
                             if(!GameManager.Instance.MenuItemsDict.ContainsKey(CategoryWanted.Caps.ToString()))        
                                 {  GameManager.Instance.MenuItemsDict.Add(CategoryWanted.Caps.ToString(), GameManager.Instance.CapsList); }
                                  break;
                            case "Sheos":
                            if(!GameManager.Instance.MenuItemsDict.ContainsKey(CategoryWanted.Sheos.ToString()))
                                 { GameManager.Instance.MenuItemsDict.Add(CategoryWanted.Sheos.ToString(), GameManager.Instance.SheosList); }
                                  break;
                            default:
                                 break;

                          }

                    }
                      

                bundle.Unload(false);
                yield return new WaitForEndOfFrame(); 
                Debug.Log("Asset downloaded");
              //  isDownloaded = true;
               uiManager.PopulateTheMenuItems(_TypeWanted);
            }
            www.Dispose();
        }
    }

}
