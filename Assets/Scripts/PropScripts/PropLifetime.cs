using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLifetime : MonoBehaviour
{
    [SerializeField] private ObjectPooling _prop_obj_pool;


    // TESTING VARIABLES, PLEASE DELETE LATER
    [SerializeField] private float _spawn_time = .5f;
    private float _time_elapsed = 0f;


    [SerializeField] private PropSO tempPropSO;

    public void Initialize()
    {
        _prop_obj_pool = GetComponent<PropOP>();
    }

    void Update()
    {
        if (GameManager.Instance.GameState == GameState.PROGRAM_START ||
            GameManager.Instance.GameState == GameState.PAUSED)
            return;

        if (_time_elapsed >= _spawn_time)
        {
            cloneProp(tempPropSO);
            _time_elapsed = 0;
            return;
        }
        _time_elapsed += Time.deltaTime;
    }

    public void cloneProp(PropSO propSO)
    {
        Prop tempProp = _prop_obj_pool.GameObjectPool.Get().GetComponent<Prop>();

        tempProp.InitializeProp(propSO);

    }
    public Prop cloneProp(PropMovable prop)
    {
        PropMovable tempProp = _prop_obj_pool.GameObjectPool.Get().GetComponent<PropMovable>();

        //tempProp.initProj(projData);

        return tempProp;
    }

    public void deactivateProp(GameObject obj)
    {
        _prop_obj_pool.GameObjectPool.Release(obj);
    }
}
