using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePauseScreen : BaseScreen
{
    public Button xBtn;
    public Button resumeBtn;
    public Button quitBtn;
    public Button settingBtn;

    // Start is called before the first frame update
    void Start()
    {
        xBtn.onClick.AddListener(XBtn_OnClick);
        resumeBtn.onClick.AddListener(ResumeBtn_OnClick);
        quitBtn.onClick.AddListener(QuitBtn_OnClick);
        settingBtn.onClick.AddListener(SettingBtn_OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void XBtn_OnClick()
    {
        if(UIManager.ins.inGamePauseScreenClone != null)
        {
            UIManager.ins.InGamePauseScreen.Hide(UIManager.ins.inGamePauseScreenClone);
        }

        Time.timeScale = 1;
    }

    public void ResumeBtn_OnClick()
    {
        if (UIManager.ins.inGamePauseScreenClone != null)
        {
            UIManager.ins.InGamePauseScreen.Hide(UIManager.ins.inGamePauseScreenClone);
        }

        Time.timeScale = 1;
    }

    public void QuitBtn_OnClick()
    {
        if(UIManager.ins.quitLevelScreenClone == null)
        {
            UIManager.ins.quitLevelScreenClone = UIManager.ins.SpawnUI(UIManager.ins.quitLevelScreen);
        }
    }

    public void SettingBtn_OnClick()
    {
        if(UIManager.ins.settingScreenClone == null)
        {
            UIManager.ins.settingScreenClone = UIManager.ins.SpawnUI(UIManager.ins.settingScreen);
        }
    }
}
