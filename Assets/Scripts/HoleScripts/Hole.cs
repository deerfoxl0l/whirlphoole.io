using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    #region Hole Variables
    private HoleData _hole_data;
    #endregion

    /*
    #region Colliders
    [SerializeField] private CircleCollider2D _outer_collider;
    [SerializeField] private CircleCollider2D _inner_collider;
    #endregion
    */

    void Start()
    {
        _hole_data = new HoleData(new Color(255, 0, 0));
    
    }

    public void ColliderEnter(Collider2D collision, string colliderType)
    {
        switch (colliderType)
        {
            case HoleDictionary.OUTER_COLLIDER:

                break;
            case HoleDictionary.INNER_COLLIDER:

                break;
        }
    }
}
