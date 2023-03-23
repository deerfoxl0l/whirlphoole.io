using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hole : MonoBehaviour
{
    #region Hole Variables
    private HoleData _hole_data;

    [SerializeField] private Collider2D _outer_collider;
    [SerializeField] private Collider2D _inner_collider;

    public int HoleLevel
    {
        get{ return _hole_data.HoleLevel; }
    }
    public int HoleExperience
    {
        get { return _hole_data.HoleExperience; }
    }
    #endregion

    [SerializeField] private GameValues _game_values;
    [SerializeField] private Player _player; // optional if just a hole, required if has player component

    [SerializeField] private int _initial_size; // TEMPORARY JUST FOR TESTING, PLEASE DELETE
    [SerializeField] private int _initial_exp;

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
        _hole_data.HoleLevel = _initial_size;
        _hole_data.HoleExperience = _initial_exp;

        this.transform.localScale = new Vector3(_game_values.HoleBaseSize, _game_values.HoleBaseSize, 1);

        Physics2D.IgnoreCollision(_outer_collider, _inner_collider);
        Physics2D.IgnoreCollision(_outer_collider, PropHandler.Instance.PropHelper.PropSpawnBoundsCollider);
        Physics2D.IgnoreCollision(_inner_collider, PropHandler.Instance.PropHelper.PropSpawnBoundsCollider);

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

    #region Enter Collider Methods ==============================================
    public void EnterColliderProp(Collider2D collision, string colliderType)
    {
        holeParams.AddParameter(EventParamKeys.PROP_PARAM, collision.GetComponent<Prop>());
      
        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.OUTER_ENTER_PROP, holeParams);
                break;
            case HoleDictionary.INNER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.INNER_ENTER_PROP, holeParams);
                break;
        }
    }

    public void EnterColliderHole(Collider2D collision, string colliderType)
    {
        holeParams.AddParameter(EventParamKeys.HOLE_PARAM_2, collision.transform.parent.GetComponent<Hole>());

        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:
                break;
            case HoleDictionary.INNER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.INNER_ENTER_HOLE, holeParams);
                break;
        }
    }
    #endregion

    #region Stay Collider Methods ======================================================
    public void StayColliderProp(Collider2D collision, string colliderType)
    {
        holeParams.AddParameter(EventParamKeys.PROP_PARAM, collision.GetComponent<Prop>());

        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.OUTER_STAY_PROP, holeParams);
                break;
            case HoleDictionary.INNER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.INNER_STAY_PROP, holeParams);
                break;
        }
    }
    public void StayColliderHole(Collider2D collision, string colliderType)
    {

    }
    #endregion 

    #region Exit Collider Methods
    public void ExitColliderProp(Collider2D collision, string colliderType)
    {
        holeParams.AddParameter(EventParamKeys.PROP_PARAM, collision.GetComponent<Prop>());

        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.OUTER_EXIT_PROP, holeParams);
                break;
            case HoleDictionary.INNER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.INNER_EXIT_PROP, holeParams);
                break;
        }
    }

    public void ExitColliderHole(Collider2D collision, string colliderType)
    {
        holeParams.AddParameter(EventParamKeys.HOLE_PARAM_2, collision.transform.parent.GetComponent<Hole>());

        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:
                break;
            case HoleDictionary.INNER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.INNER_ENTER_HOLE, holeParams);
                break;
        }
    }
    #endregion
}
