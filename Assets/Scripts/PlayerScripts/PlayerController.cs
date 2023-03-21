using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMovableKB, IMovableM
{
    [SerializeField] private GameObject _player_game_object;
    

    void Start()
    {
        if (_player_game_object == null)
            _player_game_object = this.gameObject;

    }

    #region IMovableKB
    public void MoveKB(Vector2 inputs, float moveSpeed)
    {
        transform.position = new Vector2(transform.position.x + (inputs.x * Time.deltaTime * moveSpeed),
                                          transform.position.y + (inputs.y * Time.deltaTime * moveSpeed));
    }
    #endregion

    #region IMovableM
    public void MoveM(Vector2 dragLocation, float moveSpeed)
    {
        _player_game_object.transform.rotation = getPlayerRotation(dragLocation, new Vector2 (_player_game_object.transform.position.x, this.transform.position.y));

        _player_game_object.transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
    }
    #endregion

    private Quaternion getPlayerRotation(Vector2 dragLocation, Vector2 playerLocation)
    {
        return Quaternion.Euler(0, 0, getAngle(dragLocation, playerLocation));
    }
    private float getAngle(Vector2 dragDirection, Vector2 playerLocation)
    {
        return Mathf.Atan2((dragDirection - playerLocation).y,
                            (dragDirection - playerLocation).x) * Mathf.Rad2Deg;
    }
}
/*
 public void MoveM(Vector2 location, float moveSpeed)
    {
        //this.transform.rotation = getPlayerRotation(dragLocation, new Vector2 (this.transform.position.x, this.transform.position.y));


        transform.position  = getNormalizedDirection(location) * Time.deltaTime * moveSpeed;
    }
    #endregion
    private Vector2 getNormalizedDirection(Vector2 location)
    {

        return new Vector2();
    }
 */
