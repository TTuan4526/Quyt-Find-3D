using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPopupScreen : BaseScreen
{
    public TextMeshProUGUI levelTitle;
    public Button playBtn;
    public Button closeBtn;
    // Start is called before the first frame update
    void Start()
    {
        levelTitle.text = "Level " + UIManager.ins.levelSelectScreenClone.GetComponent<LevelSelectScreen>().levelID;

        playBtn.onClick.AddListener(PlayBtn_OnClick);

        closeBtn.onClick.AddListener(CloseBtn_OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBtn_OnClick()
    {
        GameManager.ins.gameStates = GameStates.LoadLevelData;
    }

    public void CloseBtn_OnClick()
    {
        UIManager.ins.sceneStates = SceneStates.LevelSelect;
    }

   
}
