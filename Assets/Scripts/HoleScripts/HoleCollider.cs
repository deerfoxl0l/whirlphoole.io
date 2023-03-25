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
        if (_hole_parent == null) { 
            _hole_parent = GetComponentInParent<Hole>();
            Debug.Log("Got your parent hole " + _hole_parent);
        }
        _collider_type = _collider_option == 0 ? HoleDictionary.OUTER_COLLIDER : HoleDictionary.INNER_COLLIDER;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // initial singular collision with spawn bounds
        if (collision.CompareTag(TagNames.PROP_BOUNDS))
            return;

        // initial singular collision with self
        if (GameObject.ReferenceEquals(collision.transform.parent.gameObject, this.transform.parent.gameObject)) 
            return;

        if (collision.CompareTag(TagNames.PROP) )
        {
            _hole_parent.EnterColliderProp(collision, _collider_type);
            return;
        }

        if (collision.CompareTag(TagNames.HOLE))
        {
            _hole_parent.EnterColliderHole(collision, _collider_type);
            return;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(TagNames.PROP))
        {
            _hole_parent.StayColliderProp(collision, _collider_type);
            return;
        }

        if (collision.CompareTag(TagNames.HOLE))
        {
            //_hole_parent.StayColliderHole(collision, _collider_type);
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagNames.PROP))
        {
            _hole_parent.ExitColliderProp(collision, _collider_type);
            return;
        }

        if (collision.CompareTag(TagNames.HOLE))
        {
            _hole_parent.ExitColliderHole(collision, _collider_type);
            return;
        }

    }
}
