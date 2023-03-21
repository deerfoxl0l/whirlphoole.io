using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameValue", menuName = "ScriptableObjects/GameValues")]
public class GameValues : ScriptableObject
{
    #region Game Values

    [SerializeField] private float _player_base_speed;
    public float PlayerBaseSpeed
    {
        get { return _player_base_speed; }
    }

    [SerializeField] private float _player_speed_multiplier;
    public float PlayerSpeedMultiplier
    {
        get { return _player_speed_multiplier; }
    }

    #endregion
}
