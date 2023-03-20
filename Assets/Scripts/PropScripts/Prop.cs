using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Poolable, IAbsorbable
{
    [SerializeField] private PropSO _prop_so;

    public void InitializeProp(PropSO propSO)
    {

    }

    #region IAbsorbable
    public void Absorb(Vector2 holeLocation)
    {

    }
    #endregion

    #region Poolable Functions
    public override void OnInstantiate()
    {
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
