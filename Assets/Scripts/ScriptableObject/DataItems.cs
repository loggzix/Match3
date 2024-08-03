using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataItems", menuName = "ScriptableObject/Data Items")]
public class DataItems : ScriptableObject
{
   public List<GameObject> normalPrefabs;
   
   public List<GameObject> fishPrefabs;

   internal GameObject GetPrefabItem(GameManager.eTheme theme, int index)
   {
      var prefabs = GetThemePrefabs(theme);
      return prefabs[index];
   }

   private List<GameObject> GetThemePrefabs(GameManager.eTheme theme)
   {
      switch (theme)
      {
         case GameManager.eTheme.FISH:
            return fishPrefabs;
         default:
            return normalPrefabs;
      }
   }
   
}
