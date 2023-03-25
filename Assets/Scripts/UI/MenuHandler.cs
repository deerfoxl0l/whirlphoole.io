using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuHandler: MonoBehaviour
{
    #region Panels/Rects
    [SerializeField] private GameObject _name_field_2_obj;
    #endregion

    #region TextFields
    [SerializeField] private TextMeshProUGUI _name_field_1;
    [SerializeField] private TextMeshProUGUI _name_field_2;
    #endregion

    #region Buttons
    [SerializeField] private Button _single_player_btn;
    [SerializeField] private Button _two_player_btn;

    [SerializeField] private Button _play;
    #endregion

    #region Images
    [SerializeField] private Image _single_player_img;
    [SerializeField] private Image _two_player_img;
    #endregion

    #region Event Parameters
    private EventParameters menuParams;
    #endregion

    public void Awake()
    {
        Initialize();

        OnSingleClicked();
    }

    public void Initialize()
    {
        _single_player_btn = _single_player_btn.GetComponent<Button>();
        _single_player_btn.onClick .AddListener(OnSingleClicked);

        _two_player_btn = _two_player_btn.GetComponent<Button>();
        _two_player_btn.onClick.AddListener(OnTwoClicked);

        _play = _play.GetComponent<Button>();
        _play.onClick.AddListener(OnPlayClicked);

        menuParams = new EventParameters();
    }

    #region OnClick Functions
    private void OnSingleClicked()
    {
        GameManager.Instance.GameModeHandler.SwitchState(GameMode.SINGLE_PLAYER);
        _single_player_img.color = Dictionary.SELECTED_BTN;
        _two_player_img.color = Dictionary.DESELECTED_BTN;

        _name_field_2_obj.SetActive(false);
    }
    private void OnTwoClicked()
    {
        GameManager.Instance.GameModeHandler.SwitchState(GameMode.TWO_PLAYER);

        _two_player_img.color = Dictionary.SELECTED_BTN;
        _single_player_img.color = Dictionary.DESELECTED_BTN;

        _name_field_2_obj.SetActive(true);

    }
    private void OnPlayClicked()
    {
        menuParams.AddParameter(EventParamKeys.NAME_FIELD_ONE, _name_field_1.text);

        if(GameManager.Instance.GameMode == GameMode.TWO_PLAYER)
            menuParams.AddParameter(EventParamKeys.NAME_FIELD_TWO, _name_field_2.text);

        EventBroadcaster.Instance.PostEvent(EventKeys.PLAY_PRESSED, menuParams);
    }
    #endregion


}
