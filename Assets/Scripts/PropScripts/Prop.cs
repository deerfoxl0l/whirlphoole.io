using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Poolable, IPullable, IAbsorbable
{
    [SerializeField] private GameValues _game_values;
    [SerializeField] private PropSO _prop_so;
    [SerializeField] private SpriteRenderer _prop_sr;

    #region Coroutines
    private IEnumerator _pulling_prop;
    private IEnumerator _pulling_prop_anchor;
    private IEnumerator _absorbing;
    private IEnumerator _stop_absorbing;
    #endregion

    #region Cache Variables
    private Vector2 propPosition;
    private float propSize;
    #endregion
    public int PropSize
    {
        get { return _prop_so.PropSize; }
    }
    public int PropPoints
    {
        get { return _prop_so.PropPoints; }
    }

    #region Event Parameters
    private EventParameters _prop_param;
    #endregion

    void Start() // TEMPORARY, EVENTUALLY PUT TO THE FIXED ONINSTANTIATE FROM POOLABLE 
    {
        /*_prop_param = new EventParameters();
        _prop_param.AddParameter(EventParamKeys.PROP_PARAM, this);*/
    }

    public void InitializeProp(PropSO propSO)
    {
        if (_prop_sr is null)
            _prop_sr = GetComponent<SpriteRenderer>();

        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;

        this.transform.localPosition = Vector3.zero;
        this.transform.parent.localRotation = Quaternion.identity;

        //Debug.Log("prop on instantiate");

        _prop_so = propSO;
        _prop_sr.sprite = _prop_so.PropSprite;

        this.transform.parent.transform.localPosition = PropHandler.Instance.PropHelper.getPropSpawnPoint(_prop_so.PropSpawnPoint);

        propSize = _prop_so.PropSize * _game_values.PropsBaseSize * _game_values.PropsSizeMultiplier;

        this.transform.parent.transform.localScale = new Vector3(propSize, propSize, 1);


    }

    #region IPullable
    public void Pull(Transform target)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            Debug.Log("object in't active dummy");
            return;
        }

        if (_pulling_prop is not null)
            StopCoroutine(_pulling_prop);


        propPosition = transform.localPosition == this.transform.parent.localPosition ? this.transform.parent.transform.position : propPosition = this.transform.position;

        this.transform.parent.localPosition = target.transform.localPosition;
        this.transform.position = propPosition;

        _pulling_prop = PullingProp();
        _pulling_prop_anchor = PullingPropAnchor(target);
        StartCoroutine(_pulling_prop);
        StartCoroutine(_pulling_prop_anchor);
        
    }
    public IEnumerator PullingProp()
    {
        //float currentLerp=0;

        while (this.transform.localPosition.x != 0 && this.transform.localPosition.y != 0)
        {
           // Debug.Log("pulling! x:" + this.transform.localPosition.x +" y: "+ this.transform.localPosition.y);
            //Debug.Log("prop pulling! ");

            // prop pulling translation
            //currentLerp = Mathf.MoveTowards(currentLerp, 1, _game_values.HolePullStrength * Time.deltaTime);
            this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, Vector2.zero, _game_values.HolePullStrength * Time.deltaTime);

            yield return null;
        }

        //Debug.Log("coroutine break " + currentLerp);
        yield break;
    }
    public IEnumerator PullingPropAnchor(Transform target)
    {
        //Debug.Log("pulling anchor target: " + target.transform.localPosition);
        //float currentLerp = 0;

        while (this.transform.parent.transform.localPosition.x != target.transform.localPosition.x*1.1f && this.transform.parent.transform.localPosition.y != target.transform.localPosition.y * 1.1f)
        {
            //Debug.Log("prop anchor pulling! ");
            // swirl rotation
            /*
            this.transform.parent.localRotation = Quaternion.Lerp(this.transform.parent.localRotation, targetRotation, _game_values.HoleWhirlStrength * Time.deltaTime);*/
            this.transform.parent.transform.Rotate(Vector3.forward, _game_values.HoleWhirlStrength * Time.deltaTime);

            // prop anchor pulling translation
            //currentLerp = Mathf.MoveTowards(currentLerp, 1, _game_values.HolePullStrength * Time.deltaTime);
            this.transform.parent.transform.localPosition = Vector2.Lerp(this.transform.parent.transform.localPosition, target.transform.localPosition, _game_values.HolePullStrength * Time.deltaTime);

            yield return null;
        }

        yield break;
    }

    public void PullStop()
    {
        //this.transform.localPosition = Vector3.zero;
        //Debug.Log("pulling stop!");
        StopCoroutine(_pulling_prop_anchor);
    }

    #endregion

    #region IAbsorbable
    public void Absorb(EventParameters param)
    {
        Debug.Log("Absorb()");
        if (!this.gameObject.activeInHierarchy)
        {
            Debug.Log("object in't active dummy");
            return;
        }
        if(_stop_absorbing is not null)
            StopCoroutine(_stop_absorbing);

        _absorbing = Absorbing(param);
        StartCoroutine(_absorbing);
    }
    public IEnumerator Absorbing(EventParameters param)
    {
        while (this.transform.parent.transform.localScale.x > _game_values.PropScaleDespawn)
        {/*
            Debug.Log("absoring: " + this.transform.parent.transform.localScale + " and " + Vector2.Lerp(this.transform.parent.transform.parent.transform.localScale, Vector2.zero, _game_values.HoleAbsorbStrength * Time.deltaTime));*/

            this.transform.parent.transform.localScale = Vector2.Lerp(this.transform.parent.transform.localScale, Vector2.zero, _game_values.HoleAbsorbStrength*Time.deltaTime);

            yield return null;
        }
        param.AddParameter(EventParamKeys.PROP_PARAM, this);
        EventBroadcaster.Instance.PostEvent(EventKeys.PROP_ABSORBED, param);
        _absorbing = null;
        yield break;
    }

    public void AbsorbStop()
    {
        if (!this.gameObject.activeInHierarchy || _absorbing is null)
        {
            Debug.Log("Absorb stop() "+ !this.gameObject.activeInHierarchy + " and "+ (_absorbing is null));
            return;
        }

        if(_absorbing is not null)
            StopCoroutine(_absorbing);

        //Debug.Log("Absorb stop!");
        
        _stop_absorbing = StopAbsorbing();
        StartCoroutine(_stop_absorbing);
    }
    public IEnumerator StopAbsorbing()
    {
        while (this.transform.parent.transform.localScale.x < propSize)
        {
            /*Debug.Log("stop absoring: " + this.transform.parent.transform.localScale + " and " + new Vector2(propSize, propSize));*/
            this.transform.parent.transform.localScale = Vector2.Lerp(transform.parent.transform.localScale, new Vector2(propSize, propSize), _game_values.HoleAbsorbStrength * Time.deltaTime);
            yield return null;
        }

        _stop_absorbing = null;
        yield break;
    }

    #endregion

    //TODO: Refactor to have (prop and prop.child) instead of (prop and prop.parent) to use these later
    #region Poolable Functions  
    public override void OnInstantiate()
    {
        /*
        
        if (_prop_sr is null)
            _prop_sr = GetComponent<SpriteRenderer>();

        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;

        this.transform.localPosition = Vector3.zero;
        */
    }

    public override void OnActivate()
    {

    }

    public override void OnDeactivate()
    {

    }
    #endregion
}
