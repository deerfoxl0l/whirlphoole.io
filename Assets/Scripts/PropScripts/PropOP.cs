using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PropOP : ObjectPooling
{
    public List<GameObject> ObjectTemplates;

    public GameObject generateSpecificObject(string propType)
    {
        foreach(GameObject obj in ObjectTemplates)
        {
            if(obj.name == propType)
            {
                ObjectTemplate = obj;
            }
        }

        return _obj_pool.Get();
    }

    public GameObject generateRandomObject()
    {
        ObjectTemplate = ObjectTemplates[Random.Range(0, ObjectTemplates.Count)];

        return _obj_pool.Get();
    }

}
