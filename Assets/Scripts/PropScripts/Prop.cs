using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Poolable, IAbsorbable
{
    [SerializeField] private GameValues _game_values;
    [SerializeField] private PropSO _prop_so;
    [SerializeField] private SpriteRenderer _prop_sr;


    public int PropSize
    {
        get { return _prop_so.PropSize; }
    }
    public int PropPoints
    {
        get { return _prop_so.PropPoints; }
    }

    public void InitializeProp(PropSO propSO)
    {
        _prop_so = propSO;
        _prop_sr.sprite = _prop_so.PropSprite;

        transform.localPosition = PropHandler.Instance.PropHelper.getPropSpawnPoint(_prop_so.PropSpawnPoint);

        float size = _prop_so.PropSize * _game_values.PropsBaseSize * _game_values.PropsSizeMultiplier;

        transform.localScale = new Vector3(size, size, 1);


    }

    #region IAbsorbable
    public void Absorb(Vector2 holeLocation)
    {

    }
    #endregion

    #region Poolable Functions
    public override void OnInstantiate()
    {
        _prop_sr = GetComponent<SpriteRenderer>();
        _game_values = GameManager.Instance.GameValues;
        /*
        this._proj_controller = GetComponent<ProjectileController>();
        _proj_data = new ProjectileTraits();
        projectileDespawn = new EventParameters();*/
    }

    public override void OnActivate()
    {

    }

    public override void OnDeactivate()
    {
       // _proj_data.resetProjectile();
    }
    #endregion
}
