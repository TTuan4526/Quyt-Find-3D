using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFailedScreen : BaseScreen
{
    public Button tryAgainBtn;
    // Start is called before the first frame update
    void Start()
    {
        tryAgainBtn.onClick.AddListener(TryAgainBtn_OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryAgainBtn_OnClick()
    {
        GameManager.ins.gameStates = GameStates.None;

        if (UIManager.ins.inGameScreenClone != null)
        {
            UIManager.ins.InGameScreen.Hide(UIManager.ins.inGameScreenClone);
        }

        if (UIManager.ins.levelFailedScreenClone != null)
        {
            UIManager.ins.LevelFailedScreen.Hide(UIManager.ins.levelFailedScreenClone);
        }

        UIManager.ins.sceneStates = SceneStates.LevelSelect;
    }
}
