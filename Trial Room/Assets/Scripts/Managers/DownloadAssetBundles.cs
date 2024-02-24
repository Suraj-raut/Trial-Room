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
    private bool isDownloading;
    private FirebaseStorage storage;
    private StorageReference storageReference;

    // Start is called before the first frame update
    void Awake()
    {
        isDownloading = false;

        if(!isDownloading)
           GetTheUrlFromFirebaseServer();
         // StartCoroutine(DownloadAssetBundlesFromServer());
        else
          return;   

    }

    private void GetTheUrlFromFirebaseServer()
    {
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://trial-room-59e19.appspot.com");
        
        StorageReference AllItemsAssetBundle = storageReference.Child("clothingmenuicons");

        AllItemsAssetBundle.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if(!task.IsFaulted && !task.IsCanceled)
            {
               StartCoroutine(DownloadAssetBundlesFromServer(Convert.ToString(task.Result)));
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });

    }

    private IEnumerator DownloadAssetBundlesFromServer(string downloadUrl)
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

                        yield return asset;  
                       // List<Sprite> MenuItemsList = new List<Sprite>();                  

                        foreach (Sprite sprite in asset.allAssets)
                        {
                            GameManager.Instance.MenuItemsList.Add(sprite);
                        }
                        //GameManager.Instance.MenuItemsDict.Add(spriteName, MenuItemsList);
                    }
                bundle.Unload(false);
                yield return new WaitForEndOfFrame(); 
                Debug.Log("Asset downloaded");
                isDownloading = true;
               uiManager.PopulateTheMenuItems();
            }
            www.Dispose();
        }
    }

}
