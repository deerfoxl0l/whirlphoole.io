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
    [SerializeField] private TMP_InputField _name_field_1;
    [SerializeField] private TextMeshProUGUI _name_field_1_placeholder;
    [SerializeField] private TMP_InputField _name_field_2;
    [SerializeField] private TextMeshProUGUI _name_field_2_placeholder;
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

    public void Start()
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

        _name_field_1.text = string.Empty;
        _name_field_2.text = string.Empty;

        PlayerScriptableObject playerSO = ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(FileNames.PLAYER_SO_1);
        if (!string.IsNullOrWhiteSpace(playerSO.PlayerName))
        {
            _name_field_1.text = playerSO.PlayerName;
        }

        playerSO = ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(FileNames.PLAYER_SO_2);
        if (!string.IsNullOrWhiteSpace(playerSO.PlayerName))
        {
            _name_field_2.text = playerSO.PlayerName;
        }


        _name_field_1_placeholder.text = "Cardo Dalisay";
        _name_field_2_placeholder.text = "Tanggol Quiapo";

        menuParams = new EventParameters();
    }

    #region OnClick Functions
    private void OnSingleClicked()
    {/*
        Debug.Log("game maanger? " + GameManager.Instance);
        Debug.Log("game mode? " + GameManager.Instance.GameModeHandler);*/
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
        if (string.IsNullOrWhiteSpace(_name_field_1.text))
        {
            _name_field_1_placeholder.text = "Enter a name";
            return;
        }
        menuParams.AddParameter(EventParamKeys.NAME_FIELD_ONE, _name_field_1.text);

        if(GameManager.Instance.GameMode == GameMode.TWO_PLAYER)
        {
            menuParams.AddParameter(EventParamKeys.NAME_FIELD_TWO, _name_field_2.text);
            if (string.IsNullOrWhiteSpace(_name_field_2.text))
            {
                _name_field_2_placeholder.text = "Enter a name";
                return;
            }
        }

        EventBroadcaster.Instance.PostEvent(EventKeys.PLAY_PRESSED, menuParams);
    }
    #endregion


}
