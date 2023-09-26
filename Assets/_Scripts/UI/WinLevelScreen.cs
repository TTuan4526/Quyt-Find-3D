using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinLevelScreen : BaseScreen
{
    public Image star1, star2, star3;
    public Sprite starFill;
    public TextMeshProUGUI totalTime;
    public Button continueBtn;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.ins.totalStarEarn == 3)
        {
            star1.sprite = starFill;
            star2.sprite = starFill;
            star3.sprite = starFill;
        }
        else if(GameManager.ins.totalStarEarn == 2)
        {
            star1.sprite = starFill;
            star2.sprite = starFill;
        }
        else if(GameManager.ins.totalStarEarn == 1)
        {
            star1.sprite = starFill;
        }

        TimeDisplay();

        continueBtn.onClick.AddListener(ContinueBtn_OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TimeDisplay()
    {
        totalTime.text = GameManager.ins.totalTimeInLevel.ToString();

        int minutes = int.Parse(totalTime.text) / 60;
        int seconds = int.Parse(totalTime.text) % 60;

        string timeText = System.String.Format("{0:D2}:{1:D2}", minutes, seconds);
        totalTime.text = timeText;
    }

    public void ContinueBtn_OnClick()
    {
        GameManager.ins.gameStates = GameStates.None;

        if (UIManager.ins.inGameScreenClone != null)
        {
            UIManager.ins.InGameScreen.Hide(UIManager.ins.inGameScreenClone);
        }

        if (UIManager.ins.winLevelScreenClone != null)
        {
            UIManager.ins.WinLevelScreen.Hide(UIManager.ins.winLevelScreenClone);
        }

        UIManager.ins.sceneStates = SceneStates.LevelSelect;
    }
}
