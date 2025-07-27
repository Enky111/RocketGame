using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    private List<T> _pool;

    public ObjectPool(T prefab, int count)
    {
        Prefab = prefab;
        CreatePool(count);
    }
    public void CreatePool(int count)
    {
        _pool = new List<T>();
        for (int i = 0; i < count; i++)
            CreateObject();
    }

    public T CreateObject(bool isActive = false)
    {
        var CreatedObject = Object.Instantiate(Prefab);
        CreatedObject.gameObject.SetActive(isActive);
        _pool.Add(CreatedObject);
        return CreatedObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            element.gameObject.SetActive(true);
            return element;
        }
        else
            return CreateObject(true);
    }
}
