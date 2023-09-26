using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScreen : BaseScreen
{
    public Button closeBtn;
    public Button musicBtn;
    public Button soundBtn;
    public Button notificationBtn;
    public Button languageBtn;
    public Button supportBtn;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(CloseBtn_OnClick);
        musicBtn.onClick.AddListener(MusicBtn_OnClick);
        soundBtn.onClick.AddListener(SoundBtn_OnClick);
        notificationBtn.onClick.AddListener(NotificationBtn_OnClick);
        languageBtn.onClick.AddListener(LanguageBtn_OnClick);
        supportBtn.onClick.AddListener(SupportBtn_OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseBtn_OnClick()
    {
        if(UIManager.ins.settingScreenClone != null)
        {
            UIManager.ins.SettingScreen.Hide(UIManager.ins.settingScreenClone);
        }
    }

    public void MusicBtn_OnClick()
    {

    }

    public void SoundBtn_OnClick()
    {

    }

    public void NotificationBtn_OnClick()
    {

    }

    public void LanguageBtn_OnClick()
    {

    }

    public void SupportBtn_OnClick()
    {

    }
}
