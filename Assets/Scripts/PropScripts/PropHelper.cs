using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHelper : MonoBehaviour
{
    [SerializeField] private Collider2D _prop_spawn_collider;
    private Bounds _spawn_bounds;

    public void Initialize()
    {
        _spawn_bounds = _prop_spawn_collider.bounds;
    }

    public Vector2 getPropSpawnPoint(string propSpawn)
    {
        if (propSpawn == "RANDOM")  // CHANGE DELETE MODIFY LATER
        {
            return new Vector2(_spawn_bounds.center.x, _spawn_bounds.center.y)
                + new Vector2(
                    Random.Range(-_spawn_bounds.extents.x, _spawn_bounds.extents.x),
                    Random.Range(-_spawn_bounds.extents.y, _spawn_bounds.extents.y));
        }

        return Vector2.zero;
    }
}
