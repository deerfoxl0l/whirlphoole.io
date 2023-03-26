using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLifetime : MonoBehaviour, IPoolHandler
{
    [SerializeField] private GameValues _game_values;

    [SerializeField] private PropOP _prop_obj_pool;


    private float _time_elapsed = 0f;

    #region Cache Values

    #endregion

    public void Initialize()
    {
        if (_prop_obj_pool == null)
            _prop_obj_pool = GetComponent<PropOP>();

        if (_game_values == null)
            _game_values = GameManager.Instance.GameValues;

    }

    void Update()
    {
        if (! (GameManager.Instance.GameState == GameState.INGAME))
            return;

        if (_time_elapsed >= _game_values.PropSpawnRate)
        {
            if (_game_values.PropSpawnSizeFloor > getSizeCeiling())
                Debug.Log("Uhh, the floor is higher than the ceiling.");
            else
                cloneProp(Random.Range(_game_values.PropSpawnSizeFloor, getSizeCeiling()));

            _time_elapsed = 0;
            return;
        }
        _time_elapsed += Time.deltaTime;
    }

    private int getSizeCeiling()
    {
        return _game_values.PropSpawnSizeCeiling == -1 ? GameManager.Instance.CurrentBiggestHole + 2 : _game_values.PropSpawnSizeCeiling;
    }

    public void cloneProp(int propSize)
    {
        Prop tempProp = _prop_obj_pool.generateRandomObject().GetComponent<Prop>();

        tempProp.InitializeProp(propSize);

    }

    public Prop cloneProp(PropMovable prop)
    {
        PropMovable tempProp = _prop_obj_pool.GameObjectPool.Get().GetComponent<PropMovable>();

        //tempProp.initProj(projData);

        return tempProp;
    }

    public void DeactivateObject(GameObject obj)
    {
        _prop_obj_pool.GameObjectPool.Release(obj);
    }
}
