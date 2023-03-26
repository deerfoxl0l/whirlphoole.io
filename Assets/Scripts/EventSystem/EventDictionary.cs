public class EventDictionary
{
    

}

public class EventKeys{
    
    // SYSTEM EVENT KEYS
    public const string START_MENU = "PROGRAM_START";
    public const string START_GAME = "GAME_START";
    public const string PAUSE_GAME = "GAME_PAUSE";

    // GAME EVENT KEYS

    public const string PLAYER_SO_UPDATE = "PLAYER_SO_UPDATE";
    public const string PLAYER_SCORE_UPDATE = "PLAYER_SCORE_UPDATE";

    public const string OUTER_ENTER_PROP = "OUTER_ENTER_PROP";
    public const string OUTER_STAY_PROP = "OUTER_STAY_PROP";
    public const string OUTER_EXIT_PROP = "OUTER_EXIT_PROP";

    public const string INNER_ENTER_PROP = "INNER_ENTER_PROP";
    public const string INNER_STAY_PROP = "INNER_STAY_PROP";
    public const string INNER_EXIT_PROP = "INNER_EXIT_PROP";

    public const string PROP_ABSORBED = "PROP_ABSORBED";

    public const string OUTER_ENTER_HOLE= "OUTER_ENTER_HOLE";
    public const string OUTER_EXIT_HOLE = "OUTER_EXIT_HOLE";

    public const string INNER_ENTER_HOLE = "INNER_ENTER_HOLE";
    public const string INNER_EXIT_HOLE = "INNER_EXIT_HOLE";

    public const string HOLE_ABSORBED = "HOLE_ABSORBED";

    public const string HOLE_LEVEL_UP = "HOLE_LEVEL_UP";


    //UI EVENT KEYS
    public const string PLAY_PRESSED = "PLAY_BUTTON_PRESSED";
}

public class EventParamKeys
{
    public const string GAME_MODE_PARAM = "GAME_MODE";

    public const string PLAYER_PARAM = "PLAYER";
    public const string PLAYER_SCORE_PARAM = "PLAYER_SCORE_PARAM";
    public const string PLAYER_LVL_PARAM = "PLAYER_LVL_PARAM";

    public const string HOLE_PARAM = "HOLE";
    public const string HOLE_PARAM_2 = "HOLE_2";

    public const string PROP_PARAM = "PROP";

    // UI EVENT PARAM KEYS
    public const string NAME_FIELD_ONE = "NAME_FIELD_ONE";
    public const string NAME_FIELD_TWO= "NAME_FIELD_TWO";

}
