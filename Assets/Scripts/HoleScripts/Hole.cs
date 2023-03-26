using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hole : Poolable, IPullable, IAbsorbable
{
    #region Hole Variables
    [SerializeField] private HoleData _hole_data;

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
    public int HoleNxtLvl
    {
        get { return _hole_data.HoleCurrentExpThreshold; }
    }
    #endregion

    [SerializeField] private GameValues _game_values;
    [SerializeField] private Player _player; // optional if just a hole, required if has player component

    public Player PlayerHole
    {
        get { return _player; }
    }

    #region EventParameters
    EventParameters holeParams;
    #endregion

    #region Coroutines
    IEnumerator _growing_hole;

    private IEnumerator _pulling_hole;
    private IEnumerator _absorbing;
    private IEnumerator _stop_absorbing;
    #endregion

    float holeScale;
    private bool _is_absorbing;

    void Start()
    {
        if(_game_values == null)
            _game_values = GameManager.Instance.GameValues;
        if (_player == null)
            _player = GetComponentInChildren<Player>();

        if(_hole_data == null)
        {
            _hole_data = GetComponent<HoleData>();
        }
        _hole_data.InitializeHoleData(new Color(0, 255, 0));

        _hole_data.HoleCurrentExpThreshold = _game_values.HoleExpBaseThreshold;
        holeScale = _game_values.HoleBaseSize;
        this.transform.localScale = new Vector3(holeScale, holeScale, 1);

        Physics2D.IgnoreCollision(_outer_collider, _inner_collider);
        Physics2D.IgnoreCollision(_outer_collider, PropHandler.Instance.PropStaff.PropHelper.PropSpawnBoundsCollider);
        Physics2D.IgnoreCollision(_inner_collider, PropHandler.Instance.PropStaff.PropHelper.PropSpawnBoundsCollider);

        holeParams = new EventParameters();
        holeParams.AddParameter(EventParamKeys.HOLE_PARAM, this);
        if (_player is not null) // if hole has no player component attached i.e. not controlled by player
            holeParams.AddParameter(EventParamKeys.PLAYER_PARAM, _player);
    }

    public void AddHoleExperience(int exp)
    {
        if( _hole_data.AddHoleExp(exp, _game_values.HoleExpBaseThreshold, _game_values.HoleExpMultiplier))
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.HOLE_LEVEL_UP, holeParams);
        }
    }
    public void IncreaseHoleSize()
    {
        holeScale = _hole_data.HoleLevel * _game_values.HoleBaseSize * _game_values.HoleSizeMultiplier;

        _growing_hole = growHole(holeScale);
        StartCoroutine(_growing_hole);
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


    #region IPullable
    public void Pull(Transform target)
    {
        if (!this.gameObject.activeInHierarchy)
            return;

        if (_pulling_hole is not null)
            StopCoroutine(_pulling_hole);

        /*
        propPosition = _child_prop.transform.localPosition == this.transform.localPosition ? this.transform.position : propPosition = _child_prop.transform.position;

        this.transform.localPosition = target.transform.localPosition;
        _child_prop.transform.position = propPosition;
        */
        _pulling_hole = Pulling();
        StartCoroutine(_pulling_hole);

    }
    public IEnumerator Pulling()
    {
        /*
        float current = 0;
        
        while (_child_prop.transform.localPosition.x != 0 && _child_prop.transform.localPosition.y != 0)
        {

            current = Mathf.MoveTowards(current, 1, _game_values.HolePullStrengthProp * Time.deltaTime);

            // prop pulling translation
            _child_prop.transform.localPosition = Vector2.Lerp(_child_prop.transform.localPosition, Vector2.zero, current);

            yield return null;
        }*/

        yield break;
    }
    public IEnumerator PullingAnchor(Transform target)
    {
        yield break;
    }

    public void PullStop()
    {
        StopCoroutine(_pulling_hole);
    }

    #endregion

    #region IAbsorbable
    public void Absorb(EventParameters param)
    {
        if (!this.gameObject.activeInHierarchy || _is_absorbing)
            return;

        if (_stop_absorbing is not null)
            StopCoroutine(_stop_absorbing);

        _absorbing = Absorbing(param);
        StartCoroutine(_absorbing);
    }
    public IEnumerator Absorbing(EventParameters param)
    {
        _is_absorbing = true;
        
        while (this.transform.localScale.x > _game_values.HoleScaleDespawn)
        {
            Debug.Log("absorbing hole!");
            this.transform.localScale = Vector2.Lerp(this.transform.localScale, Vector2.zero, _game_values.HoleAbsorbStrengthHole * Time.deltaTime);

            yield return null;
        }

        // TEMPORARY, EVENTUALLY FIND A WAY TO NOT HAVE TO GET PARAMS FROM HANDLER
        EventBroadcaster.Instance.PostEvent(EventKeys.HOLE_ABSORBED, param);

        _absorbing = null;
        _is_absorbing = false;
        yield break;
    }

    public void AbsorbStop()
    {
        if (!this.gameObject.activeInHierarchy || _absorbing == null)
        {
            return;
        }

        if (!_is_absorbing)
        {
            return;
        }

        StopCoroutine(_absorbing);

        _stop_absorbing = StopAbsorbing();
        Debug.Log("Absorbing hole stop!");
        StartCoroutine(_stop_absorbing);
    }
    public IEnumerator StopAbsorbing()
    {
        float current = 0;

        while (this.transform.localScale.x < holeScale)
        {
            Debug.Log("stopping absorbing!");
            current = Mathf.MoveTowards(current, 1, _game_values.HoleAbsorbStrengthHole * Time.deltaTime);
             this.transform.localScale = Vector2.Lerp(this.transform.localScale, new Vector2(holeScale, holeScale), current);
            yield return null;
        }
        _is_absorbing = false;
        _stop_absorbing = null;
        yield break;
    }

    #endregion


    private void addPropParam(Collider2D collision)
    {
        holeParams.AddParameter(EventParamKeys.PROP_PARAM, collision.transform.parent.GetComponent<Prop>());
    }
    private void addHoleParam(Collider2D collision)
    {
        holeParams.AddParameter(EventParamKeys.HOLE_PARAM, this);

        holeParams.AddParameter(EventParamKeys.HOLE_PARAM_2, collision.transform.parent.GetComponent<Hole>());
    }

    #region Enter Collider Methods ==============================================
    public void EnterColliderProp(Collider2D collision, string colliderType)
    {
        addPropParam(collision);

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
        addHoleParam(collision);

        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.OUTER_ENTER_HOLE, holeParams);
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
        addPropParam(collision);

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
        addHoleParam(collision);

        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:

                break;
            case HoleDictionary.INNER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.INNER_STAY_HOLE, holeParams);
                break;
        }
    }
    #endregion 

    #region Exit Collider Methods
    public void ExitColliderProp(Collider2D collision, string colliderType)
    {
        addPropParam(collision);

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
        addHoleParam(collision);

        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:
                break;
            case HoleDictionary.INNER_COLLIDER:
                EventBroadcaster.Instance.PostEvent(EventKeys.INNER_EXIT_HOLE, holeParams);
                break;
        }
    }
    #endregion

    #region Poolable Functions  
    public override void OnInstantiate()
    {
       // InitializeProp();
    }

    public override void OnActivate()
    {
        /*
        _child_prop.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;*/
    }

    public override void OnDeactivate()
    {
        _is_absorbing = false;
    }
    #endregion
}
