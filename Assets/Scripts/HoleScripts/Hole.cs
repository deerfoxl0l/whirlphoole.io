using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Hole : MonoBehaviour
{
    #region Hole Variables
    private HoleData _hole_data;

    public int HoleLevel
    {
        get{ return _hole_data.HoleLevel; }
    }
    #endregion

    private GameValues _game_values;

    [SerializeField] private CinemachineVirtualCamera _camera;

    #region EventParameters
    EventParameters holeParams;
    #endregion

    void Start()
    {
        _game_values = GameManager.Instance.GameValues;

        _hole_data = new HoleData(new Color(255, 255, 255));
            _hole_data.HoleCurrentExpThreshold = _game_values.HoleExpThreshold;

        this.transform.localScale = new Vector3(_game_values.HoleBaseSize, _game_values.HoleBaseSize, 1);

        holeParams = new EventParameters();
        holeParams.AddParameter(EventParamKeys.HOLE_PARAM, this);
    }
    public void AddHoleExperience(int exp)
    {
        if( _hole_data.AddHoleExp(exp, _game_values.HoleExpThreshold, _game_values.HoleExpThresholdMultiplier))
        {
            _camera.m_Lens.OrthographicSize++;
            increaseHoleSize();
        }
    }
    private void increaseHoleSize()
    {
        float size = _hole_data.HoleLevel * _game_values.HoleBaseSize * _game_values.HoleSizeMultiplier;

        this.transform.localScale +=new Vector3(size, size, 1);
    }

    public void ColliderEnter(Collider2D collision, string colliderType)
    {
        holeParams.AddParameter(EventParamKeys.PROP_PARAM, collision.GetComponent<Prop>());

        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:
                break;
            case HoleDictionary.INNER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.INNER_HOLE_ENTER, holeParams);
                break;
        }
    }
}
