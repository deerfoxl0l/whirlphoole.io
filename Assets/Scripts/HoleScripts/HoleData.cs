using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleData
{
    #region Hole Data Values
    private int _hole_level;
    public int HoleLevel
    {
        get { return _hole_level; }
        set { _hole_level = value; }
    }

    private int _hole_experience;
    public int HoleExperience
    {
        get { return _hole_experience; }
        set { _hole_experience = value; }
    }
    private float _hole_decrease_move_speed_multiplier;
    public float HoleSpeedMultiplier
    {
        get { return _hole_decrease_move_speed_multiplier; }

    }
    private Color _hole_color;
    #endregion

    #region Hole Data Methods
    public HoleData(Color color)
    {
        _hole_color = color;
        _hole_level = 0;
        _hole_experience = 0;
    }

    #endregion
}
