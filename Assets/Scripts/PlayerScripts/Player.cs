using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    private GameValues _game_values;

    #region Player Variables

    [SerializeField] private PlayerScriptableObject _player_so;
    [SerializeField] private PlayerController _player_controller;
    [SerializeField] private InputHandler _input_handler;

    #endregion

    [SerializeField] private CameraScript _player_camera;

    private Hole _hole;
    public int PlayerHoleLevel
    {
        get { return _hole.HoleLevel; }
    }


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

        if (_input_handler is null)
            _input_handler = GetComponent<InputHandler>();

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
    private void movePlayerKeyboard(float decreaseSpeed)
    {//movespeed = _player_base_speed - (_player_level * _player_speed_decrease_multiplier)
        _player_controller.MoveKB(_input_handler.UserKeyInput, _game_values.PlayerBaseSpeed - decreaseSpeed );
    }
    private void movePlayerMouse(float decreaseSpeed)
    {
        _player_controller.MoveM(_input_handler.UserCursorInput, _game_values.PlayerBaseSpeed - decreaseSpeed);
    }

    public void ZoomOutPlayerCamera()
    {
        _player_camera.ZoomOutCamera();
    }

}
