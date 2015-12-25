/*

**************************************
************ POOL MASTER *************
**************************************
______________________________________

VERSION: 2.0
FILE:    POOLMASTER.CS
AUTHOR:  CODY JOHNSON
COMPANY: HAMSTERBYTE, LLC
EMAIL:   HAMSTERBYTELLC@GMAIL.COM
WEBSITE: WWW.HAMSTERBYTE.COM
SUPPORT: WWW.HAMSTERBYTE.COM/POOL-MASTER

COPYRIGHT © 2014-2015 HAMSTERBYTE, LLC
ALL RIGHTS RESERVED

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using hamsterbyte.PoolMaster;

public static class PoolMaster
{

    private static ObjectPool _instanceRef;

    public static ObjectPool instance
    {
        get
        {
            if (_instanceRef == null)
            {
                _instanceRef = Object.FindObjectOfType<ObjectPool>();
                if (_instanceRef == null)
                    Debug.LogError("No object pool present in scene");
            }

            return _instanceRef;
        }
    }

    public static void PreloadAll()
    {
        //Preload all objects in the object pool instance. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.PreloadAll();
    }

    public static void Preload(string poolName)
    {
        if (instance != null) instance.Preload(poolName);
    }

    public static void Spawn(string poolName, string objName)
    {
        //Spawn an object in the object pool instance. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.Spawn(poolName, objName);
    }

    public static void Spawn(string poolName, string objName, Vector3 position)
    {
        //Spawn an object in the object pool instance. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.Spawn(poolName, objName, position);
    }

    public static void Spawn(string poolName, string objName, Vector3 position, Quaternion rotation)
    {
        //Spawn an object in the object pool instance. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.Spawn(poolName, objName, position, rotation);
    }

    public static GameObject SpawnReference(string poolName, string objName)
    {
        //Spawn an object in the object pool instance. Further comments can be found in the ObjectPool script.
        //returns reference to the object spawned
        if (instance != null) return instance.SpawnReference(poolName, objName); else return null;
    }

    public static GameObject SpawnReference(string poolName, string objName, Vector3 position)
    {
        //Spawn an object in the object pool instance. Further comments can be found in the ObjectPool script.
        //returns reference to the object spawned
        if (instance != null) return instance.SpawnReference(poolName, objName, position); else return null;
    }

    public static GameObject SpawnReference(string poolName, string objName, Vector3 position, Quaternion rotation)
    {
        //Spawn an object in the object pool instance. Further comments can be found in the ObjectPool script.
        //returns reference to the object spawned
        if (instance != null) return instance.SpawnReference(poolName, objName, position, rotation); else return null;
    }

    public static void SpawnRandom(string poolName, Vector3 position)
    {
        //Spawn a random object in the object pool instance. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.SpawnRandom(poolName, position);
    }

    public static void SpawnRandom(List<string> poolNames, Vector3 position)
    {
        //Spawn a random object from a list of pools. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.SpawnRandom(poolNames, position);
    }

    public static void SpawnRandom(string[] poolNames, Vector3 position)
    {
        //Spawn a random object from an array of pools. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.SpawnRandom(poolNames, position);
    }

    public static void SpawnRandom(string poolName, Vector3 position, Quaternion rotation)
    {
        //Spawn a random object in the object pool instance. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.SpawnRandom(poolName, position, rotation);
    }

    public static void SpawnRandom(List<string> poolNames, Vector3 position, Quaternion rotation)
    {
        //Spawn a random object from a list of pools. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.SpawnRandom(poolNames, position, rotation);
    }

    public static void SpawnRandom(string[] poolNames, Vector3 position, Quaternion rotation)
    {
        //Spawn a random object from an array of pools. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.SpawnRandom(poolNames, position, rotation);
    }

    public static GameObject SpawnRandomReference(string poolName, Vector3 position)
    {
        //Spawn a random object in the object pool instance. Further comments can be found in the ObjectPool script.
        //returns reference to the object spawned
        if (instance != null) return instance.SpawnRandomReference(poolName, position); else return null;
    }

    public static GameObject SpawnRandomReference(List<string> poolNames, Vector3 position)
    {
        //Spawn a random object from a list of given pools. Further comments can be found in the ObjectPool script.
        //returns reference to the object spawned
        if (instance != null) return instance.SpawnRandomReference(poolNames, position); else return null;
    }

    public static GameObject SpawnRandomReference(string[] poolNames, Vector3 position)
    {
        //Spawn a random object from an array of given pools. Further comments can be found in the ObjectPool script.
        //returns reference to the object spawned
        if (instance != null) return instance.SpawnRandomReference(poolNames, position); else return null;
    }

    public static GameObject SpawnRandomReference(string poolName, Vector3 position, Quaternion rotation)
    {
        //Spawn a random object in the object pool instance. Further comments can be found in the ObjectPool script.
        //returns reference to the object spawned
        if (instance != null) return instance.SpawnRandomReference(poolName, position, rotation); else return null;
    }

    public static GameObject SpawnRandomReference(List<string> poolNames, Vector3 position, Quaternion rotation)
    {
        //Spawn a random object from a list of given pools. Further comments can be found in the ObjectPool script.
        //returns reference to the object spawned
        if (instance != null) return instance.SpawnRandomReference(poolNames, position, rotation); else return null;
    }

    public static GameObject SpawnRandomReference(string[] poolNames, Vector3 position, Quaternion rotation)
    {
        //Spawn a random object from an array of given pools. Further comments can be found in the ObjectPool script.
        //returns reference to the object spawned
        if (instance != null) return instance.SpawnRandomReference(poolNames, position, rotation); else return null;
    }

    public static void Despawn(GameObject g)
    {
        //Despawn an object in the object pool instance. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.Despawn(g);
    }

    public static void Despawn(string poolName)
    {
        //Despawn an entire pool at once
        if (instance != null) instance.Despawn(poolName);
    }

    public static void Despawn(string[] poolNames)
    {
        //Despawn an array of pools
        if (instance != null) instance.Despawn(poolNames);
    }

    public static void Despawn(List<string> poolNames)
    {
        //Despawn an list of pools
        if (instance != null) instance.Despawn(poolNames);
    }

    public static void Destroy(GameObject g)
    {
        //Destroy an object in the object pool instance. Further comments can be found in the ObjectPool script.
        if (instance != null) instance.Destroy(g);
    }

    public static void Destroy(string poolName)
    {
        //Destroy an entire pool at once
        if (instance != null) instance.Destroy(poolName);
    }

    public static void Destroy(string[] poolNames)
    {
        //Destroy an array of pools
        if (instance != null) instance.Destroy(poolNames);
    }

    public static void Destroy(List<string> poolNames)
    {
        //Destroy an list of pools
        if (instance != null) instance.Destroy(poolNames);
    }

    public static Pool GetPool(int index)
    {
        //return pool located at specified index in master pool
        if (instance != null) return instance.GetPool(index); else return null;
    }

    public static Pool GetPool(string poolName)
    {
        //return pool with specified name in master pool
        if (instance != null) return instance.GetPool(poolName); else return null;
    }

}
