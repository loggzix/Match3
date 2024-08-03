using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<eStateGame> StateChangedAction = delegate { };
    
    public enum  eTheme
    {
        NORMAL,
        FISH
    }

    public enum eLevelMode
    {
        TIMER,
        MOVES
    }

    public enum eStateGame
    {
        SETUP,
        MAIN_MENU,
        GAME_STARTED,
        PAUSE,
        GAME_OVER,
    }
    private eLevelMode m_mode;

    private eStateGame m_state;
    public eStateGame State
    {
        get { return m_state; }
        private set
        {
            m_state = value;

            StateChangedAction(m_state);
        }
    }

    public DataItems DataItems;

    private GameSettings m_gameSettings;

    private BoardController m_boardController;

    private UIMainManager m_uiMenu;

    private LevelCondition m_levelCondition;

    public static GameManager Instance;

    #region Unity Functions
    
    private void Awake()
    {
        State = eStateGame.SETUP;

        m_gameSettings = Resources.Load<GameSettings>(Constants.GAME_SETTINGS_PATH);

        m_uiMenu = FindObjectOfType<UIMainManager>();
        m_uiMenu.Setup(this);

        Pooling();

        if (!Instance)
        {
            Instance = this;
        }
    }

    void Start()
    {
        State = eStateGame.MAIN_MENU;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_boardController != null) m_boardController.Update();
    }
    
    #endregion
    
    private void Pooling()
    {
        GameObject prefabBG = Resources.Load<GameObject>(Constants.PREFAB_CELL_BACKGROUND);
        var fastPool = FastPoolManager.CreatePool(prefabBG, false, m_gameSettings.BoardSizeX*m_gameSettings.BoardSizeY); //tao ra cac bg trc
    }

    internal void SetState(eStateGame state)
    {
        State = state;

        if(State == eStateGame.PAUSE)
        {
            DOTween.PauseAll();
        }
        else
        {
            DOTween.PlayAll();
        }
    }

    public void LoadLevel(eLevelMode mode)
    {
        m_mode = mode;
        // m_boardController = new GameObject("BoardController").AddComponent<BoardController>();
        var fastPool = FastPoolManager.GetPool(88, null, false);
        m_boardController = fastPool.FastInstantiate<BoardController>();
        
        m_boardController.StartGame(this, m_gameSettings);

        if (mode == eLevelMode.MOVES)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelMoves>();
            m_levelCondition.Setup(m_gameSettings.LevelMoves, m_uiMenu.GetLevelConditionView(), m_boardController);
        }
        else if (mode == eLevelMode.TIMER)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelTime>();//init
            m_levelCondition.Setup(m_gameSettings.LevelMoves, m_uiMenu.GetLevelConditionView(), this);
        }

        m_levelCondition.ConditionCompleteEvent += GameOver;

        State = eStateGame.GAME_STARTED;
    }
    
    public void RestartLevel()
    {
        m_boardController.RestartLevel();
        if (m_mode == eLevelMode.MOVES)
        {
            m_levelCondition.Reset(m_uiMenu.GetLevelConditionView());
        }
        else if (m_mode == eLevelMode.TIMER)
        {
            m_levelCondition.Reset(m_uiMenu.GetLevelConditionView());
        }
        Debug.Log("Restar Level!!!");
    }

    public void GameOver()
    {
        StartCoroutine(WaitBoardController());
    }

    internal void ClearLevel()
    {
        if (m_boardController)
        {
            m_boardController.Clear();
            Destroy(m_boardController.gameObject);
            m_boardController = null;
        }
    }

    private IEnumerator WaitBoardController()
    {
        while (m_boardController.IsBusy)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);

        State = eStateGame.GAME_OVER;

        if (m_levelCondition != null)
        {
            m_levelCondition.ConditionCompleteEvent -= GameOver;

            Destroy(m_levelCondition);
            m_levelCondition = null;
        }
    }
}
