using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMovableKB, IMovableM
{
    [SerializeField] private GameObject _player_game_object;

    Vector3 tempVec;
    void Start()
    {
        if (_player_game_object == null)
            _player_game_object = this.transform.parent.gameObject;

    }

    #region IMovableKB
    public void MoveKB(Vector2 inputs, float moveSpeed)
    {
        _player_game_object.transform.position = new Vector2(transform.position.x + (inputs.x * Time.deltaTime * moveSpeed), transform.position.y + (inputs.y * Time.deltaTime * moveSpeed));
    }
    #endregion

    #region IMovableM
    public void MoveM(Vector2 pointerLocation, float moveSpeed)
    {

        tempVec = getDirection(pointerLocation);
        _player_game_object.transform.position = new Vector3(
                _player_game_object.transform.position.x + (tempVec.x * Time.deltaTime * moveSpeed),
                _player_game_object.transform.position.y + (tempVec.y * Time.deltaTime * moveSpeed)
            );
    }
    #endregion
    private Vector3 getDirection(Vector2 pointerLocation)
    {
        
        return Vector3.Normalize(new Vector2(pointerLocation.x- _player_game_object.transform.localPosition.x, pointerLocation.y - _player_game_object.transform.localPosition.y));
    }


}
