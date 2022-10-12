using UnityEngine;
using System.Collections.Generic;
using  System;
using Photon.Pun;

public class SP_GenericPool : MonoBehaviourPun 
{
    
    [System.Serializable]
    public class Pool
    {
        [SerializeField] public string tag;
        [SerializeField] public int poolSize;
        [SerializeField] public GameObject objectToPool;
        [SerializeField] public GameObject objectFather;
    }
    public List<Pool> poolsList;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    public static SP_GenericPool Instance;
    protected virtual void Awake()
    {
        Instance = this;   
    }
    private void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in poolsList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i <pool.poolSize; i++)
            {
                GameObject obj = InstancePoolObject(pool.objectToPool, transform);
                if(pool.objectFather != null)
                { 
                    obj.transform.parent = pool.objectFather.transform;
                }
                print("Se instancia");
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag,Vector3 posToSpawn, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(tag)) // Chequeo de si la key existe
        {
            Debug.LogWarning("no existe pool con" + tag);
            return null;
        }
       
        GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
        if (!objectToSpawn.activeSelf)
        {
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = posToSpawn;
            objectToSpawn.transform.rotation = rotation;
        }
        else
        {
            var newObject = InstancePoolObject(objectToSpawn, objectToSpawn.transform.parent);
            PoolDictionary[tag].Enqueue(objectToSpawn);
            objectToSpawn = newObject;
        }

        
        
        IPooleable pooledObj = objectToSpawn.GetComponent<IPooleable>();
        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        PoolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
        
    }

    protected virtual GameObject InstancePoolObject(GameObject objectToSpawn, Transform desiredPos)
    {
        return Instantiate(objectToSpawn, desiredPos);
    }
}
