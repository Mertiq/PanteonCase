using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private List<T> pool = new();
    private T prefab;
    private Transform parent;

    public ObjectPool(T prefab, Transform parent, int initialSize)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (var i = 0; i < initialSize; i++)
        {
            var obj = Object.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public T GetObject()
    {
        var obj = pool.FirstOrDefault(x => !x.gameObject.activeInHierarchy);

        if (obj is null)
        {
            var newObj = Object.Instantiate(prefab, parent);
            newObj.gameObject.SetActive(true);
            pool.Add(newObj);
            return newObj;
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReleaseObject(T obj) => obj.gameObject.SetActive(false);
}