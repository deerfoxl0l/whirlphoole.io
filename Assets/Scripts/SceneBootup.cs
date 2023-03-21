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
        PlayerHandler.Instance.Initialize();
        InputHandler.Instance.Initialize();
        PropHandler.Instance.Initialize();

        if (PlayerHandler.Instance.IsDoneInitializing &&
            InputHandler.Instance.IsDoneInitializing &&
            PropHandler.Instance.IsDoneInitializing)
        {
            Debug.Log(SceneNames.GAME_SCENE + " initialized!");
            //EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
        }
    }
}
