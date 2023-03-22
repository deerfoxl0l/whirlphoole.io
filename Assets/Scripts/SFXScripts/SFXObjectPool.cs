using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXObjectPool : ObjectPooling
{
    [SerializeField] private GameObject _points_sfx;
    [SerializeField] private Transform _points_transform;

    public GameObject getPointsSFX()
    {
        this.ObjectTemplate = _points_sfx;
        this.ObjectTransform = _points_transform;

        return this.GameObjectPool.Get();
    }
}
