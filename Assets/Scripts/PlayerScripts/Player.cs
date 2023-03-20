using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    #region Player Variables
    [SerializeField] private PlayerScriptableObject _player_so;
    private PlayerController _player_controller;
    #endregion

    /*
    #region Event Parameters
    private EventParameters playerHitEvent;
    #endregion*/

    #region Game Values
    //[SerializeField] [Range(1, 3)] private int PlayerIntColor = 1;

    [SerializeField] private float MoveSpeed = 5f;
    #endregion

    void Start()
    {
        if(_player_so is null)
        {
            _player_so = ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(FileNames.PLAYER_SO);
        }
        _player_controller = GetComponent<PlayerController>();

       // playerHitEvent = new EventParameters();

        //playerHitEvent.AddParameter(EventParamKeys.playerParam, this);


    }
    private void Update()
    {
        
        if (InputHandler.Instance.UserKeyHold)
        {
            movePlayerKeyboard();
        }

        movePlayerMouse();
    }
    public void movePlayerKeyboard()
    {
        _player_controller.MoveKB(InputHandler.Instance.UserKeyInput, MoveSpeed);
    }
    public void movePlayerMouse()
    {
        _player_controller.MoveM(InputHandler.Instance.UserCursorInput, MoveSpeed);

    }

}
