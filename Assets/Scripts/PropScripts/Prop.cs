using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Poolable, IPullable, IAbsorbable
{
    [SerializeField] private GameValues _game_values;
    [SerializeField] private Transform _child_prop;
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

    public void InitializeProp(PropSO propSO)
    {        

        _prop_so = propSO;
        _prop_sr.sprite = _prop_so.PropSprite;

        this.transform.localPosition = PropHandler.Instance.PropHelper.getPropSpawnPoint(_prop_so.PropSpawnPoint);

        propSize = _prop_so.PropSize * _game_values.PropsBaseSize * _game_values.PropsSizeMultiplier;

        this.transform.localScale = new Vector3(propSize, propSize, 1);


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


        propPosition = _child_prop.transform.localPosition == this.transform.localPosition ? this.transform.position : propPosition = _child_prop.transform.position;

        this.transform.localPosition = target.transform.localPosition;
        _child_prop.transform.position = propPosition;

        _pulling_prop = PullingProp();
        _pulling_prop_anchor = PullingPropAnchor(target);
        StartCoroutine(_pulling_prop);
        StartCoroutine(_pulling_prop_anchor);
        
    }
    public IEnumerator PullingProp()
    {

        while (_child_prop.transform.localPosition.x != 0 && _child_prop.transform.localPosition.y != 0)
        {
            // prop pulling translation
            _child_prop.transform.localPosition = Vector2.Lerp(_child_prop.transform.localPosition, Vector2.zero, _game_values.HolePullStrength * Time.deltaTime);

            yield return null;
        }

        yield break;
    }
    public IEnumerator PullingPropAnchor(Transform target)
    {

        while (this.transform.localPosition.x != target.transform.localPosition.x*_game_values.PropAnchorAim && this.transform.localPosition.y != target.transform.localPosition.y * _game_values.PropAnchorAim)
        {
            // swirl rotation
            this.transform.Rotate(Vector3.forward, _game_values.HoleWhirlStrength * Time.deltaTime);

            this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, target.transform.localPosition, _game_values.HolePullStrength * Time.deltaTime);

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
        if (!this.gameObject.activeInHierarchy)
            return;

        if(_stop_absorbing is not null)
            StopCoroutine(_stop_absorbing);

        _absorbing = Absorbing(param);
        StartCoroutine(_absorbing);
    }
    public IEnumerator Absorbing(EventParameters param)
    {
        while (this.transform.localScale.x > _game_values.PropScaleDespawn)
        {
            this.transform.localScale = Vector2.Lerp(this.transform.localScale, Vector2.zero, _game_values.HoleAbsorbStrength*Time.deltaTime);

            yield return null;
        }

        // TEMPORARY, EVENTUALLY FIND A WAY
        param.AddParameter(EventParamKeys.PROP_PARAM, this);
        EventBroadcaster.Instance.PostEvent(EventKeys.PROP_ABSORBED, param);
        _absorbing = null;
        yield break;
    }

    public void AbsorbStop()
    {
        if (!this.gameObject.activeInHierarchy || _absorbing is null)
            return;
        

        if(_absorbing is not null)
            StopCoroutine(_absorbing);

        _stop_absorbing = StopAbsorbing();
        StartCoroutine(_stop_absorbing);
    }
    public IEnumerator StopAbsorbing()
    {
        while (this.transform.localScale.x < propSize)
        {
            this.transform.localScale = Vector2.Lerp(this.transform.localScale, new Vector2(propSize, propSize), _game_values.HoleAbsorbStrength * Time.deltaTime);
            yield return null;
        }

        _stop_absorbing = null;
        yield break;
    }

    #endregion

    #region Poolable Functions  
    public override void OnInstantiate()
    {
        if (_prop_sr is null)
            _prop_sr = _child_prop.GetComponent<SpriteRenderer>();

        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;

        //Debug.Log("On Instantiate!");
    }

    public override void OnActivate()
    {
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }

    public override void OnDeactivate()
    {

    }
    #endregion
}
