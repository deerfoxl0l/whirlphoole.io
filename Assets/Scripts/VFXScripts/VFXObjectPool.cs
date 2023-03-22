using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXObjectPool : ObjectPooling
{
    [SerializeField] private GameObject _points_vfx;
    [SerializeField] private Transform _points_transform;

    public GameObject getPointsVFX()
    {
        this.ObjectTemplate = _points_vfx;
        this.ObjectTransform = _points_transform;

        return this.GameObjectPool.Get();
    }
}
