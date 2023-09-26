using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager ins;

    public SceneStates sceneStates;

    public bool isRunLevelComplete;

    [Header("References")]
    public StudioIntroScreen StudioIntroScreen;
    public LoadingScreen LoadingScreen;
    public MainMenuScreen MainMenuScreen;
    public SettingScreen SettingScreen;
    public LevelSelectScreen LevelSelectScreen;
    public LevelPopupScreen LevelPopupScreen;
    public InGameScreen InGameScreen;
    public InGamePauseScreen InGamePauseScreen;
    public WinLevelScreen WinLevelScreen;
    public LevelFailedScreen LevelFailedScreen;
    public QuitLevelScreen QuitLevelScreen;
    public GetMoreLiveScreen GetMoreLiveScreen;
    public WeeklyChallengeScreen WeeklyChallengeScreen;
    public LevelChestScreen LevelChestScreen;
    public StarChestScreen StarChestScreen;
    public LanguageScreen LanguageScreen;
    public SupportScreen SupportScreen;
    [Header("Spawn")]
    public GameObject studioIntroScreen;
    public GameObject loadingScreen;
    public GameObject mainMenuScreen;
    public GameObject settingScreen;
    public GameObject levelSelectScreen;
    public GameObject levelPopupScreen;
    public GameObject inGameScreen;
    public GameObject inGamePauseScreen;
    public GameObject winLevelScreen;
    public GameObject levelFailedScreen;
    public GameObject quitLevelScreen;
    public GameObject getMoreLiveScreen;
    public GameObject weeklyChallengeScreen;
    public GameObject levelChestScreen;
    public GameObject starChestScreen;
    public GameObject languageScreen;
    public GameObject supportScreen;
    [Header("Other")]
    [SerializeField] private GameObject UIParent;
    //Clone
    [HideInInspector] public GameObject studioIntroScreenClone, loadingScreenClone, mainMenuScreenClone, settingScreenClone,
        levelSelectScreenClone, levelPopupScreenClone, inGameScreenClone, inGamePauseScreenClone, winLevelScreenClone,
        levelFailedScreenClone, quitLevelScreenClone, getMoreLiveScreenClone, weeklyChallengeScreenClone, levelChestScreenClone
        , starChestScreenClone, languageScreenClone, supportScreenClone;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitUI()
    {
        studioIntroScreenClone = null;
        loadingScreenClone = null;
        mainMenuScreenClone = null;
        settingScreenClone = null;
        levelSelectScreenClone = null;
        levelPopupScreenClone = null;
        inGameScreenClone = null;
        inGamePauseScreenClone = null;
        winLevelScreenClone = null;
        levelFailedScreenClone = null;
        quitLevelScreenClone = null;
        getMoreLiveScreenClone = null;
        weeklyChallengeScreenClone = null;
        levelChestScreenClone = null;
        starChestScreenClone = null;
        languageScreenClone = null;
        supportScreenClone = null;
    }

    private void Start()
    {
        InitUI();
        sceneStates = SceneStates.StudioIntro;
    }

   

    private void Update()
    {
        UpdateSceneStates();
    }

    private void UpdateSceneStates()
    {
        switch (sceneStates)
        {
            case SceneStates.StudioIntro:
                StartCoroutine(StudioIntroUI());
                break;
            case SceneStates.LoadingScreen:
                LoadingScreenUI();
                break;
            case SceneStates.MainMenu:
                MainMenuUI();
                break;
            case SceneStates.Setting:
                break;
            case SceneStates.LevelSelect:
                LevelSelectUI();
                break;
            case SceneStates.LevelPopup:
                LevelPopupUI();
                break;
            case SceneStates.InGameScreen:
                InGameUI();
                break;
            case SceneStates.WinLevelScreen:
                WinLevelUI();
                break;
            case SceneStates.LevelFailed:
                break;
            case SceneStates.GetMoreLives:
                break;
            case SceneStates.WeekLyChallenge:
                break;
            case SceneStates.LevelChest:
                break;
            case SceneStates.StarChest:
                break;
            case SceneStates.Language:
                break;
            case SceneStates.Support:
                break;
        }
    }

    private IEnumerator StudioIntroUI()
    {
        if(studioIntroScreenClone == null)
        {
            studioIntroScreenClone = SpawnUI(studioIntroScreen);
        }

        yield return new WaitForSeconds(2f);

        StudioIntroScreen.Hide(studioIntroScreenClone);

        sceneStates = SceneStates.LoadingScreen;
    }

    private void LoadingScreenUI()
    {
        if(loadingScreenClone == null)
        {
            loadingScreenClone = SpawnUI(loadingScreen);
        }
    }

    private void MainMenuUI()
    {
        if (mainMenuScreenClone == null)
        {
            mainMenuScreenClone = SpawnUI(mainMenuScreen);
        }
    }

    private void LevelSelectUI()
    {
        if(levelPopupScreenClone != null)
        {
            LevelPopupScreen.Hide(levelPopupScreenClone);
        }

        if(levelSelectScreenClone == null)
        {
            levelSelectScreenClone = SpawnUI(levelSelectScreen);
        }
    }

    private void LevelPopupUI()
    {
        if(levelPopupScreenClone == null)
        {
            levelPopupScreenClone = SpawnUI(levelPopupScreen);
        }
    }

    private void InGameUI()
    {
        LevelPopupScreen.Hide(levelPopupScreenClone);
        LevelSelectScreen.Hide(levelSelectScreenClone);
        MainMenuScreen.Hide(mainMenuScreenClone);

        if(inGameScreenClone == null)
        {
            inGameScreenClone = SpawnUI(inGameScreen);
        }

    }

    private void WinLevelUI()
    {
        if (winLevelScreenClone == null)
        {
            winLevelScreenClone = SpawnUI(winLevelScreen);
        }
    }

    public GameObject SpawnUI(GameObject ui)
    {
        return Instantiate(ui, UIParent.transform);
    }
}
