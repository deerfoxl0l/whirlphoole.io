using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prop", menuName = "ScriptableObjects/Prop")]
public class PropSO: ScriptableObject
{
    #region PropSO Values

    [SerializeField] private int _prop_size;
    public int PropSize
    {
        get { return _prop_size; }
    }

    [SerializeField] private int _prop_points;
    public int PropPoints
    {
        get { return _prop_points; }
    }

    [SerializeField] private Sprite _prop_sprite;
    public Sprite PropSprite
    {
        get { return _prop_sprite; }
    }

    [SerializeField] private string _prop_spawn_point = "RANDOM"; // MODIFY THIS EVENTUALLY
    public string PropSpawnPoint
    {
        get { return _prop_spawn_point; }
    }

    #endregion

    #region PropMovableSO Values

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
}
