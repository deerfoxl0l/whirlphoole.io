using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameValue", menuName = "ScriptableObjects/GameValues")]
public class GameValues : ScriptableObject
{
    #region Game Values

    [SerializeField] [Range(0.1f, 20f)] public float PlayerBaseSpeed;

    // USAGE: movespeed = _player_base_speed - (_player_level * _player_speed_decrease_multiplier)
    [SerializeField] [Range(0.1f, 1.0f)] public float PlayerSpeedDecreaseMultiplier;

    // USAGE: replaces hole transform.localScale;
    [SerializeField] [Range(0.1f, 1.5f)] public float HoleBaseSize;

    [SerializeField] [Range(0.1f, 10f)] public float HoleGrowSpeed;
    
    // USAGE: size += _hole_level* _hole_base_size*_hole_size_multiplier
    [SerializeField][Range(0.1f, 1.5f)] public float HoleSizeMultiplier;

    [SerializeField] [Range(1f, 10f)] public float HolePullStrength;
    [SerializeField] [Range(10f, 100f)] public float HoleWhirlStrength;
    [SerializeField] [Range(1f, 30f)] public float HoleAbsorbStrength;
    [SerializeField] [Range(1, 100)] public int HoleExpThreshold;

    //USAGE: _hole_base_exp_threshold  + (_hole_base_exp_threshold *(_hole_level -1)* _hole_exp_threshold_multiplier)
    [SerializeField][Range(0.5f, 1.5f)] public float HoleExpThresholdMultiplier;

    [SerializeField] [Range(1, 5)] public int HoleAbsorbDifference;

    // the multiplier to be multiplied to the exp points from a hole absorbed by a bigger hole
    [SerializeField] [Range(0.1f, 1.5f)] public float HoleCannibalExpMultiplier;

    [SerializeField] [Range(0.1f, 2f)] public float PropSpawnRate;

    // USAGE: replaces prop transform.localScale;
    [SerializeField] [Range(0.1f, 2f)] public float PropsBaseSize;
    [SerializeField] [Range(0.01f, 1.0f)] public float PropScaleDespawn;

    // USAGE: size += _prop_size* _props_base_size*_props_size_multiplier
    [SerializeField] [Range(0.1f, 1.5f)] public float PropsSizeMultiplier;

    [SerializeField] [Range(0f, 5f)] public float CameraZoomAmount;
    [SerializeField] [Range(0f, 10f)] public float CameraZoomSpeed;

    #endregion
}
