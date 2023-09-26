using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitLevelScreen : BaseScreen
{
    public Button closeBtn;
    public Button stayBtn;
    public Button quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(CloseBtn_OnClick);
        stayBtn.onClick.AddListener(StayBtn_OnClick);
        quitBtn.onClick.AddListener(QuitBtn_OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseBtn_OnClick()
    {
        if(UIManager.ins.quitLevelScreenClone != null)
        {
            UIManager.ins.QuitLevelScreen.Hide(UIManager.ins.quitLevelScreenClone);
        }
    }
    public void StayBtn_OnClick()
    {
        if (UIManager.ins.quitLevelScreenClone != null)
        {
            UIManager.ins.QuitLevelScreen.Hide(UIManager.ins.quitLevelScreenClone);
        }
    }
    public void QuitBtn_OnClick()
    {
        GameManager.ins.gameStates = GameStates.None;

        if (UIManager.ins.inGameScreenClone != null)
        {
            UIManager.ins.InGameScreen.Hide(UIManager.ins.inGameScreenClone);
        }

        if (UIManager.ins.inGamePauseScreenClone != null)
        {
            UIManager.ins.InGamePauseScreen.Hide(UIManager.ins.inGamePauseScreenClone);
        }

        if (UIManager.ins.quitLevelScreenClone != null)
        {
            UIManager.ins.QuitLevelScreen.Hide(UIManager.ins.quitLevelScreenClone);
        }

        UIManager.ins.sceneStates = SceneStates.MainMenu;
    }
}
