using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLifetime : MonoBehaviour
{
    [SerializeField] private PropSO tempPropSO;
    [SerializeField] private ObjectPooling _prop_obj_pool;

    [SerializeField] private GameValues _game_values;

    private float _time_elapsed = 0f;


    public void Initialize()
    {
        if (_prop_obj_pool is null)
            _prop_obj_pool = GetComponent<PropOP>();

        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;
    }

    void Update()
    {
        if (GameManager.Instance.GameState == GameState.PROGRAM_START ||
            GameManager.Instance.GameState == GameState.PAUSED)
            return;

        if (_time_elapsed >= _game_values.PropSpawnRate)
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
