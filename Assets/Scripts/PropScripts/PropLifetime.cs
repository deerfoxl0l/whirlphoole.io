using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLifetime : MonoBehaviour
{
    [SerializeField] private GameValues _game_values;

    [SerializeField] private ObjectPooling _prop_obj_pool;
    [SerializeField] private List<PropSO> _props_size_1s;
    [SerializeField] private List<PropSO> _props_size_2s;
    [SerializeField] private List<PropSO> _props_size_3s;
    [SerializeField] private List<PropSO> _props_size_4s;
    [SerializeField] private List<PropSO> _props_size_5s;

    [SerializeField] private List<List<PropSO>> _props_lists;

    private float _time_elapsed = 0f;

    #region Cache Values
    private int propChoice;
    #endregion

    public void Initialize()
    {
        if (_prop_obj_pool is null)
            _prop_obj_pool = GetComponent<PropOP>();

        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;

        _props_lists = new List<List<PropSO>>();

        _props_lists.Add(_props_size_1s);
        _props_lists.Add(_props_size_2s);
        _props_lists.Add(_props_size_3s);
        _props_lists.Add(_props_size_4s);
        _props_lists.Add(_props_size_5s);
    }

    void Update()
    {
        if (GameManager.Instance.GameState == GameState.PROGRAM_START ||
            GameManager.Instance.GameState == GameState.PAUSED)
            return;

        if (_time_elapsed >= _game_values.PropSpawnRate)
        {
            if(GameManager.Instance.CurrentBiggestHole<5)
                propChoice = Random.Range(0, GameManager.Instance.CurrentBiggestHole + 1);

            cloneProp (_props_lists[propChoice][Random.Range(0, _props_lists[propChoice].Count)] );

            /*
            switch (propChoice)
            {
                case 1:
                    cloneProp(_props_size_1s[Random.Range(0, _props_size_1s.Count)]);
                    break;
                case 2:
                    cloneProp(_props_size_2s[Random.Range(0, _props_size_2s.Count)]);
                    break;
                case 3:
                    cloneProp(_props_size_3s[Random.Range(0, _props_size_3s.Count)]);
                    break;
                case 4:
                    cloneProp(_props_size_4s[Random.Range(0, _props_size_4s.Count)]);
                    break;
                case 5:
                    cloneProp(_props_size_5s[Random.Range(0, _props_size_5s.Count)]);
                    break;
            }*/
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
