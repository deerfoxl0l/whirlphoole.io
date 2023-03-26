using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBootup : MonoBehaviour, IBootstrapper
{
    [SerializeField] private GameValues _game_values;
    [SerializeField] private VisualValues _visual_values;

    [SerializeField] private PlayerUI _player_ui_template;
    [SerializeField] private GameObject _player_canvases;


    [SerializeField] private CameraStaff _camera_staff;
    [SerializeField] private VFXObjectPool _vfx_op;
    [SerializeField] private PlayerStaff _player_staff;
    [SerializeField] private PropStaff _prop_staff;

    public void Awake()
    {
        LoadSingletonsAndDependencies();
    }
    public void LoadSingletonsAndDependencies()
    {
        //if left blank in the serialized fields,, will get default values
        // if not left blank, assign to gamemanager
        GameManager.Instance.GameValues = _game_values == null ? ScriptableObjectsHelper.GetScriptableObject<GameValues>(FileNames.GAME_VALUES) : _game_values;

        GameManager.Instance.VisualValues = _visual_values == null ? ScriptableObjectsHelper.GetScriptableObject<VisualValues>(FileNames.VISUAL_VALUES) : _visual_values;

        UIManager.Instance.Initialize();
        UIManager.Instance.SetPlayerUI(_player_ui_template, _player_canvases);

        CameraHandler.Instance.CameraStaff = _camera_staff;
        CameraHandler.Instance.Initialize();

        VFXHandler.Instance.VFXObjectPool = _vfx_op;
        VFXHandler.Instance.Initialize();

        InputHandler.Instance.Initialize();
        PlayerHandler.Instance.PlayerStaff = _player_staff;
        PlayerHandler.Instance.Initialize();

        HoleHandler.Instance.Initialize();
        PropHandler.Instance.PropStaff = _prop_staff;
        PropHandler.Instance.Initialize();

        if (PlayerHandler.Instance.IsDoneInitializing
            && HoleHandler.Instance.IsDoneInitializing
            && PropHandler.Instance.IsDoneInitializing
            && VFXHandler.Instance.IsDoneInitializing
            && CameraHandler.Instance.IsDoneInitializing)
        {
            Debug.Log(SceneNames.GAME_SCENE + " initialized!");
            
            EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
        }
    }
}
