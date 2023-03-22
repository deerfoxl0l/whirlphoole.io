using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hole : MonoBehaviour
{
    #region Hole Variables
    private HoleData _hole_data;

    public int HoleLevel
    {
        get{ return _hole_data.HoleLevel; }
    }
    #endregion

    [SerializeField] private GameValues _game_values;
    [SerializeField] private Player _player; // optional


    #region EventParameters
    EventParameters holeParams;
    #endregion

    #region Coroutines
    IEnumerator _growing_hole;
    #endregion
    void Start()
    {
        if(_game_values is null)
            _game_values = GameManager.Instance.GameValues;
        if (_player is null)
            _player = GetComponentInChildren<Player>();

        _hole_data = new HoleData(new Color(255, 255, 255));
            _hole_data.HoleCurrentExpThreshold = _game_values.HoleExpThreshold;

        this.transform.localScale = new Vector3(_game_values.HoleBaseSize, _game_values.HoleBaseSize, 1);


        holeParams = new EventParameters();
        holeParams.AddParameter(EventParamKeys.HOLE_PARAM, this);
        if (_player is not null)
            holeParams.AddParameter(EventParamKeys.PLAYER_PARAM, _player);
    }
    public void AddHoleExperience(int exp)
    {
        if( _hole_data.AddHoleExp(exp, _game_values.HoleExpThreshold, _game_values.HoleExpThresholdMultiplier))
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.HOLE_LEVEL_UP, holeParams);
        }
    }
    public void IncreaseHoleSize()
    {
        float size = _hole_data.HoleLevel * _game_values.HoleBaseSize * _game_values.HoleSizeMultiplier;

        _growing_hole = growHole(size);
        StartCoroutine(_growing_hole);

        //this.transform.localScale +=new Vector3(size, size, 1);
    }

    private IEnumerator growHole(float targetSize)
    {
        float tempVect = 0;
        while (this.transform.localScale.x < targetSize)
        {
            tempVect = Mathf.MoveTowards(this.transform.localScale.x, targetSize, _game_values.HoleGrowSpeed * Time.deltaTime);
            this.transform.localScale = new Vector3(tempVect, tempVect, 1);
            yield return null;
        }

        StopCoroutine("growHole");
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
