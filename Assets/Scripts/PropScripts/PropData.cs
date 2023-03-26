using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropData : MonoBehaviour
{
    [SerializeField] private GameValues _game_values;

    #region PropSO Values

    [SerializeField] [Range(1, 20)] private int _prop_size;
    public int PropSize
    {
        get { return _prop_size; }
    }

    [SerializeField] private int _prop_points;
    public int PropPoints
    {
        get { return _prop_points; }
    }

    [SerializeField] private string _prop_spawn_point = PropDictionary.PROP_SPAWN_PREDETERMINED;
    public string PropSpawnPoint
    {
        get { return _prop_spawn_point; }
    }

    #endregion

    #region PropMovable Values

    [SerializeField] private float _prop_move_speed;
    public float PropMoveSpeed
    {
        get { return _prop_move_speed; }
    }

    [SerializeField] private string _prop_move_path;
    public string PropMovePath
    {
        get { return _prop_move_path; }
    }

    #endregion
    void Start()
    {
        if (_game_values == null)
            _game_values = GameManager.Instance.GameValues;
    }

    public PropData(int size)
    {
        InitializeData(size);
    }
    public void InitializeData(int size)
    {
        if (_game_values == null)
            _game_values = GameManager.Instance.GameValues;

        _prop_size = size;
        _prop_points = _game_values.PropsPointsBase + ((size-1) * _game_values.PropsPointsMultiplier);

        _prop_spawn_point = PropDictionary.PROP_SPAWN_RANDOM;

    }
}
