using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuHandler: MonoBehaviour
{
    [SerializeField] private GameObject _main_canvas;

    #region Panels
    [SerializeField] private GameObject _top_panel;
    [SerializeField] private GameObject _bottom_panel;
    #endregion

    #region TextFields
    [SerializeField] private TextMeshProUGUI _name_field;
    #endregion

    #region Buttons
    [SerializeField] private Button _play_btn;
    #endregion

    #region Event Parameters
    private EventParameters menuParams;
    #endregion

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        UIManager.Instance.MenuHandler = this;

        _play_btn = _play_btn.GetComponent<Button>();
        _play_btn.onClick .AddListener(OnPlayClicked);

        menuParams = new EventParameters();

        menuParams.AddParameter<string>(EventParamKeys.NAME_FIELD, _name_field.text);

        DontDestroyOnLoad(this.transform);
    }

    #region OnClick Functions
    private void OnPlayClicked()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.PLAY_PRESSED, menuParams);
    }
    #endregion


}
