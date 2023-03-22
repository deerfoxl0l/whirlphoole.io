using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour
{
    [SerializeField] private Hole _hole_parent;

    // 0 == outer, 1 == inner

    [SerializeField] [Range(0, 1)] private int _collider_option; 
    private string _collider_type;

    private void Start()
    {
        if (_hole_parent is null) { 
            _hole_parent = GetComponentInParent<Hole>();
            Debug.Log("Got your parent hole " + _hole_parent);
        }
        _collider_type = _collider_option == 0 ? HoleDictionary.OUTER_COLLIDER : HoleDictionary.INNER_COLLIDER;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter at " + _collider_type);
        
        if (collision.CompareTag(TagNames.PROP_BOUNDS))
            return;

        if (collision.CompareTag(TagNames.PROP))
        {
            _hole_parent.ColliderEnter(collision, _collider_type);
            return;
        }
    }
}
