using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameValue", menuName = "ScriptableObjects/GameValues")]
public class GameValues : ScriptableObject
{
    #region Game Values

    [SerializeField] [Range(0.1f, 20f)] private float _player_base_speed;
    public float PlayerBaseSpeed
    {
        get { return _player_base_speed; }
    }

    // USAGE: movespeed = _player_base_speed - (_player_level * _player_speed_decrease_multiplier)
    [SerializeField] [Range(0.1f, 1.0f)] private float _player_speed_decrease_multiplier;
    public float PlayerSpeedDecreaseMultiplier
    {
        get { return _player_speed_decrease_multiplier; }
    }

    // USAGE: replaces hole transform.localScale;
    [SerializeField] [Range(0.1f, 1.5f)] private float _hole_base_size;
    public float HoleBaseSize
    {
        get { return _hole_base_size; }
    }

    [SerializeField] [Range(0.1f, 10f)] private float _hole_grow_speed;
    public float HoleGrowSpeed
    {
        get { return _hole_grow_speed; }
    }

    // USAGE: size += _hole_level* _hole_base_size*_hole_size_multiplier
    [SerializeField][Range(0.1f, 1.5f)] private float _hole_size_multiplier;
    public float HoleSizeMultiplier
    {
        get { return _hole_size_multiplier; }
    }
    [SerializeField] [Range(0.1f, 2f)] private float _hole_absorb_strength;
    public float HoleAbsorbStrength
    {
        get { return _hole_absorb_strength; }
    }

    [SerializeField] [Range(1, 100)] private int _hole_base_exp_threshold;
    public int HoleExpThreshold
    {
        get { return _hole_base_exp_threshold; }
    }
    //USAGE: _hole_base_exp_threshold  + (_hole_base_exp_threshold *(_hole_level -1)* _hole_exp_threshold_multiplier)
    [SerializeField][Range(0.5f, 1.5f)] private float _hole_exp_threshold_multiplier;
    public float HoleExpThresholdMultiplier
    {
        get { return _hole_exp_threshold_multiplier; }
    }

    [SerializeField] [Range(0.1f, 2f)] private float _props_spawn_rate;
    public float PropSpawnRate
    {
        get { return _props_spawn_rate; }
    }

    // USAGE: replaces prop transform.localScale;
    [SerializeField] [Range(0.1f, 2f)] private float _props_base_size;
    public float PropsBaseSize
    {
        get { return _props_base_size; }
    }

    // USAGE: size += _prop_size* _props_base_size*_props_size_multiplier
    [SerializeField] [Range(0.1f, 1.5f)] private float _props_size_multiplier;
    public float PropsSizeMultiplier
    {
        get { return _props_size_multiplier; }
    }

    [SerializeField] [Range(0f, 5f)] private float _camera_zoom_amount;
    public float CameraZoomAmount
    {
        get { return _camera_zoom_amount; }
    }
    [SerializeField] [Range(0f, 10f)] private float _camera_zoom_speed;
    public float CameraZoomSpeed
    {
        get { return _camera_zoom_speed; }
    }
    #endregion
}
