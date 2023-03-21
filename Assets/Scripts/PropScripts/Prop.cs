using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Poolable, IAbsorbable
{
    [SerializeField] private PropSO _prop_so;
    [SerializeField] private SpriteRenderer _prop_sr;

    [SerializeField] private float size_multiplier; // DELETE THIS LATER

    public void InitializeProp(PropSO propSO)
    {
        _prop_so = propSO;
        _prop_sr.sprite = _prop_so.PropSprite;

        transform.localPosition = PropHandler.Instance.PropHelper.getPropSpawnPoint(_prop_so.PropSpawnPoint);

        transform.localScale *= size_multiplier;

        
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
