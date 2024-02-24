using System;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle
{
   [MenuItem("Assets/Create Assets Bundles")] 
   private static void BuildAllAssetsBundle()
   {
      string  assetBundleDirectoryPath = Application.dataPath +  "/../AssetBundles";
      
      try
      {
        BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
      }
      catch(Exception e)
      {
        Debug.LogWarning(e);
      }

   }

}
