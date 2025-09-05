using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] T prefabObj;
    [SerializeField] int size;
    private Queue<T> poolQueue;


    void Awake()
    {
        // Create Pool Queue
        poolQueue = new();
        for (int i = 0; i < size; i++)
        {
            T obj = Instantiate(prefabObj, transform);
            obj.gameObject.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public T GetObject()
    {
        if (poolQueue.Count > 0)
        {
            T obj = poolQueue.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            return Instantiate(prefabObj, transform);
        }
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.gameObject.transform.position = transform.position;
        poolQueue.Enqueue(obj);
    }
}
