using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    #region System Values ScriptableO's
    [SerializeField] private GameValues _game_values;
    [SerializeField] private VisualValues _visual_values;
    #endregion

    #region Player Variables

    [SerializeField] private PlayerScriptableObject _player_so;
    [SerializeField] private PlayerController _player_controller;
    [SerializeField] private InputHandler _input_handler;

    #endregion

    [SerializeField] private CameraScript _player_camera;
    public CameraScript PlayerCamera
    {
        set { _player_camera = value; }
    }
    [SerializeField] private TextMesh _text_mesh;

    [SerializeField] private Hole _hole;
    public int PlayerHoleLevel
    {
        get { return _hole.HoleLevel; }
    }

    public string PlayerName
    {
        get { return _player_so.PlayerName; }
    }

    public void InitializePlayer(PlayerScriptableObject playerSo)
    {
        this._player_so = playerSo;


    }
    private void Start()
    {
        if (_game_values == null)
            _game_values = GameManager.Instance.GameValues;

        if (_visual_values == null)
            _visual_values = GameManager.Instance.VisualValues;

        if (_hole == null)
            _hole = GetComponentInParent<Hole>();

        if (_player_controller == null)
            _player_controller = GetComponent<PlayerController>();

        if (_input_handler == null)
            _input_handler = GetComponent<InputHandler>();

        _input_handler.Initialize();

        if (_text_mesh == null)
            _text_mesh = GetComponent<TextMesh>();
        _text_mesh.fontSize = _visual_values.NameTagBaseFontSize;
        SetTextMesh("" + _player_so.PlayerName + " | Lvl " + _hole.HoleLevel);
    }
    private void Update()
    {
        
        if (_input_handler.UserKeyHold)
        {
            movePlayerKeyboard(_hole.HoleLevel * _game_values.PlayerSpeedDecreaseMultiplier);
        }

        movePlayerMouse(_hole.HoleLevel * _game_values.PlayerSpeedDecreaseMultiplier);
    }

    public void SetTextMesh(string text)
    {
        _text_mesh.text = text;
    }
    public void UpdateLevel()
    {
        this.transform.localScale -= new Vector3(_visual_values.NameTagBalancing, _visual_values.NameTagBalancing, 1);
        SetTextMesh("" + _player_so.PlayerName + " | Lvl " + _hole.HoleLevel);
    }
    public void ZoomOutPlayerCamera()
    {
        _player_camera.ZoomOutCamera();
    }

    private void movePlayerKeyboard(float decreaseSpeed)
    {//movespeed = _player_base_speed - (_player_level * _player_speed_decrease_multiplier)
        _player_controller.MoveKB(_input_handler.UserKeyInput, _game_values.PlayerBaseSpeed - decreaseSpeed );
    }
    private void movePlayerMouse(float decreaseSpeed)
    {
        // just a big block of text making sure it doesn't jitter at cursor point
        if( ((_input_handler.UserCursorInput.x - _game_values.PlayerCursorOffset) < this.transform.parent.transform.localPosition.x && this.transform.parent.transform.localPosition.x < (_input_handler.UserCursorInput.x + _game_values.PlayerCursorOffset)) && (((_input_handler.UserCursorInput.y - _game_values.PlayerCursorOffset) < this.transform.parent.transform.localPosition.y) &&(this.transform.parent.transform.localPosition.y < (_input_handler.UserCursorInput.y + _game_values.PlayerCursorOffset))))
            return;

        _player_controller.MoveM(_input_handler.UserCursorInput, _game_values.PlayerBaseSpeed - decreaseSpeed);
    }


}
