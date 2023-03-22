using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBootup : MonoBehaviour, IBootstrapper
{
    public void Awake()
    {
        LoadSingletonsAndDependencies();
    }
    public void LoadSingletonsAndDependencies()
    {
        GameManager.Instance.GameValues = ScriptableObjectsHelper.GetScriptableObject<GameValues>(FileNames.GAME_VALUES);

        PlayerHandler.Instance.Initialize();
        HoleHandler.Instance.Initialize();
        PropHandler.Instance.Initialize();
        VFXHandler.Instance.Initialize();



        if (PlayerHandler.Instance.IsDoneInitializing
            && HoleHandler.Instance.IsDoneInitializing
            && PropHandler.Instance.IsDoneInitializing
            && VFXHandler.Instance.IsDoneInitializing        )
        {
            Debug.Log(SceneNames.GAME_SCENE + " initialized!");
            
            //EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
        }
    }
}
