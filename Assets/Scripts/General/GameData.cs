using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : SingletonMonoBehaviour<GameData>
{
    public DataPathHolder dataPathHolder;

    [HideInInspector] public List<BuildingData> buildingDatas;

    private void Awake()
    {
        buildingDatas = Resources.LoadAll<BuildingData>(dataPathHolder.buildingsDataPath).ToList();
    }
}