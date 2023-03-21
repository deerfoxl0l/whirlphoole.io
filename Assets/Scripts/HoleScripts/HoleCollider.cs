using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour
{
    [SerializeField] private Hole _hole_parent;
    //[SerializeField] private CircleCollider2D _circle_collider;
    [SerializeField] [Range(0, 1)] private int _collider_option; // 0 == outer, 1 == inner
    private string _collider_type;

    private void Start()
    {
        //_circle_collider = GetComponent<CircleCollider2D>();

        _collider_type = _collider_option == 0 ? HoleDictionary.OUTER_COLLIDER : HoleDictionary.INNER_COLLIDER;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("OnTriggerEnter at " + _collider_type);
        _hole_parent.ColliderEnter(collision, _collider_type);
        /*
        if (collision.CompareTag(TagNames.))
        {
            return;
        }*/


    }
}
