using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    private GameValues _game_values;
    private Hole _hole;

    #region Player Variables

    [SerializeField] private PlayerScriptableObject _player_so;
    [SerializeField] private PlayerController _player_controller;
    [SerializeField] private PlayerHandler _player_handler;
    [SerializeField] private InputHandler _input_handler;

    #endregion

    void Start()
    {
        _game_values = GameManager.Instance.GameValues;
        if(_hole is null)
            _hole = GetComponentInParent<Hole>();
        //Debug.Log("got your parent hole: " + _hole);

        if (_player_so is null)
            _player_so = ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(FileNames.PLAYER_SO);

        if(_player_controller is null)
            _player_controller = GetComponent<PlayerController>();

        if (_player_handler is null)
            _player_handler = GetComponent<PlayerHandler>();

        if (_input_handler is null)
            _input_handler = GetComponent<InputHandler>();

        _player_handler.Initialize();
        _input_handler.Initialize();
    }
    private void Update()
    {
        
        if (_input_handler.UserKeyHold)
        {
            movePlayerKeyboard(_hole.HoleLevel * _game_values.PlayerSpeedDecreaseMultiplier);
        }

        movePlayerMouse(_hole.HoleLevel * _game_values.PlayerSpeedDecreaseMultiplier);
    }
    public void movePlayerKeyboard(float decreaseSpeed)
    {//movespeed = _player_base_speed - (_player_level * _player_speed_decrease_multiplier)
        _player_controller.MoveKB(_input_handler.UserKeyInput, _game_values.PlayerBaseSpeed - decreaseSpeed );
    }
    public void movePlayerMouse(float decreaseSpeed)
    {
        _player_controller.MoveM(_input_handler.UserCursorInput, _game_values.PlayerBaseSpeed - decreaseSpeed);

    }

}
