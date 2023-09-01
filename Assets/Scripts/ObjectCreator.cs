using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator<TType, TData, TObject> : MonoBehaviour
    where TData : GameData where TObject : Component, ISetupable
{
    private Transform objectHolder;
    private List<CustomKeyValuePair<TType, TObject>> objectPrefabs;
    private Dictionary<TType, ObjectPool<TObject>> objectPools = new();

    public void Setup(Transform holder, List<CustomKeyValuePair<TType, TObject>> prefabs)
    {
        objectHolder = holder;
        objectPrefabs = prefabs;

        objectPrefabs.ForEach(obj =>
            objectPools.Add(obj.key, new ObjectPool<TObject>(obj.value, objectHolder, 1)));
    }

    public void CreateObject(params object[] args)
    {
        var data = (TData)args[0];
        var type = GetObjectType(data);

        var obj = objectPools[type].GetObject();

        obj.Setup(data);
    }

    public void ReleaseObject(TObject obj)
    {
        obj.ResetView();

        var type = GetObjectType(obj);
        objectPools[type].ReleaseObject(obj);
    }

    protected TType GetObjectType(TData data)
    {
        throw new NotImplementedException("You need to implement this method in a derived class.");
    }

    protected TType GetObjectType(TObject obj)
    {
        throw new NotImplementedException("You need to implement this method in a derived class.");
    }
}