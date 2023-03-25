using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBootup : MonoBehaviour, IBootstrapper
{
    [SerializeField] private GameValues _game_values;
    [SerializeField] private VisualValues _visual_values;
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


        CameraHandler.Instance.Initialize();
        VFXHandler.Instance.Initialize();
        InputHandler.Instance.Initialize();

        PlayerHandler.Instance.Initialize();
        HoleHandler.Instance.Initialize();
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
