using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataHolder : SingletonMonoBehaviour<GameDataHolder>
{
    public string buildingsDataPath;

    [HideInInspector] public List<BuildingData> buildingDatas;

    private void Awake() => buildingDatas = Resources.LoadAll<BuildingData>(buildingsDataPath).ToList();
}