using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator2 : ObjectCreator<BuildingType, BuildingData, Building>
{
     private new BuildingType GetObjectType(BuildingData data)
    {
        throw new NotImplementedException("You need to implement this method in a derived class.");
    }
    
}