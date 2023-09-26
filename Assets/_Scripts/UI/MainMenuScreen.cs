using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuScreen : BaseScreen
{
    #region MainMenu Screens Component
    [Header("Screens")]
    public GameObject MainScreen;
    public GameObject LivesBankScreen;
    public GameObject ShopScreen;
    public GameObject TeamScreen;
    public GameObject LeaderBoardScreen;
    #endregion

    #region Header Component
    [Header("Header")]
    public Button lifeAddBtn;
    public Button coinAddBtn;
    public Button settingBtn;

    public TextMeshProUGUI lifeShowTxt;
    public TextMeshProUGUI lifeIconTxt;
    public TextMeshProUGUI coinShowTxt;
    #endregion

    #region Footer Component
    [Header("Footer")]
    public Button homeBtn;
    public Button livesBankBtn;
    public Button shopBtn;
    public Button teamBtn;
    public Button leaderBoardBtn;

    public GameObject lightBG;
    public GameObject homeBGActive;

    public GameObject livesBankActive;

    public GameObject shopActive;

    public GameObject teamActive;

    public GameObject leaderBoardActive;
    #endregion

    #region MainMenu Component
    [Header("MainMenu")]
    public Button weeklyChallengeBtn;
    public Button starChestBtn;
    public Button levelChestBtn;
    public Button removeAdsBtn;
    public Button playBtn;
    #endregion

    //LivesBank

    //Shop

    //Teams

    //LeaderBoard


    private void Start()
    {
        //Header
        lifeAddBtn.onClick.AddListener(LifeAddBtn_OnClick);
        coinAddBtn.onClick.AddListener(CoinAddBtn_OnClick);
        settingBtn.onClick.AddListener(SettingBtn_OnClick);

        //Footer
        homeBtn.onClick.AddListener(HomeBtn_OnClick);
        livesBankBtn.onClick.AddListener(LivesBankBtn_OnClick);
        shopBtn.onClick.AddListener(ShopBtn_OnClick);
        teamBtn.onClick.AddListener(TeamBtn_OnClick);
        leaderBoardBtn.onClick.AddListener(LeaderBoardBtn_OnClick);

        //MainMenu
        weeklyChallengeBtn.onClick.AddListener(WeekLyChallengeBtn_OnClick);
        starChestBtn.onClick.AddListener(StarChestBtn_OnClick);
        levelChestBtn.onClick.AddListener(LevelChestBtn_OnClick);
        removeAdsBtn.onClick.AddListener(RemoveAdsBtn_OnClick);
        playBtn.onClick.AddListener(PlayBtn_OnClick);
    }

    #region Header Function
    public void LifeAddBtn_OnClick()
    {

    }

    public void CoinAddBtn_OnClick()
    {

    }

    public void SettingBtn_OnClick()
    {

    }
    #endregion

    #region Footer Function
    public void HomeBtn_OnClick()
    {
        homeBGActive.gameObject.SetActive(true);
        livesBankActive.gameObject.SetActive(false);
        shopActive.gameObject.SetActive(false);
        teamActive.gameObject.SetActive(false);
        leaderBoardActive.gameObject.SetActive(false);

        MainScreen.SetActive(true);
        LivesBankScreen.SetActive(false);
        ShopScreen.SetActive(false);
        TeamScreen.SetActive(false);
        LeaderBoardScreen.SetActive(false);
    }

    public void LivesBankBtn_OnClick()
    {
        lightBG.gameObject.SetActive(false);
        homeBGActive.gameObject.SetActive(false);
        livesBankActive.gameObject.SetActive(true);
        shopActive.gameObject.SetActive(false);
        teamActive.gameObject.SetActive(false);
        leaderBoardActive.gameObject.SetActive(false);

        MainScreen.SetActive(false);
        LivesBankScreen.SetActive(true);
        ShopScreen.SetActive(false);
        TeamScreen.SetActive(false);
        LeaderBoardScreen.SetActive(false);
    }

    public void ShopBtn_OnClick()
    {
        lightBG.gameObject.SetActive(false);
        homeBGActive.gameObject.SetActive(false);
        livesBankActive.gameObject.SetActive(false);
        shopActive.gameObject.SetActive(true);
        teamActive.gameObject.SetActive(false);
        leaderBoardActive.gameObject.SetActive(false);

        MainScreen.SetActive(false);
        LivesBankScreen.SetActive(false);
        ShopScreen.SetActive(true);
        TeamScreen.SetActive(false);
        LeaderBoardScreen.SetActive(false);
    }

    public void TeamBtn_OnClick()
    {
        lightBG.gameObject.SetActive(false);
        homeBGActive.gameObject.SetActive(false);
        livesBankActive.gameObject.SetActive(false);
        shopActive.gameObject.SetActive(false);
        teamActive.gameObject.SetActive(true);
        leaderBoardActive.gameObject.SetActive(false);

        MainScreen.SetActive(false);
        LivesBankScreen.SetActive(false);
        ShopScreen.SetActive(false);
        TeamScreen.SetActive(true);
        LeaderBoardScreen.SetActive(false);
    }

    public void LeaderBoardBtn_OnClick()
    {
        lightBG.gameObject.SetActive(false);
        homeBGActive.gameObject.SetActive(false);
        livesBankActive.gameObject.SetActive(false);
        shopActive.gameObject.SetActive(false);
        teamActive.gameObject.SetActive(false);
        leaderBoardActive.gameObject.SetActive(true);

        MainScreen.SetActive(false);
        LivesBankScreen.SetActive(false);
        ShopScreen.SetActive(false);
        TeamScreen.SetActive(false);
        LeaderBoardScreen.SetActive(true);
    }
    #endregion

    #region MainMenu Function

    public void WeekLyChallengeBtn_OnClick()
    {

    }

    public void StarChestBtn_OnClick()
    {

    }

    public void LevelChestBtn_OnClick()
    {

    }

    public void RemoveAdsBtn_OnClick()
    {

    }

    public void PlayBtn_OnClick()
    {
        UIManager.ins.sceneStates = SceneStates.LevelSelect;
    }

    public void DeleteDataBtn_OnClick()
    {
        DataManager.ins.ClearLevelData();
    }
    #endregion
}
