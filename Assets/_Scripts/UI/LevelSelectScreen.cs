using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class LevelSelectScreen : BaseScreen
{
    public Button backBtn;
    public Button[] allBtns;
    public List<Button> levelBtns;
    public Sprite starFill;
    public int levelID;
    int sd = 1;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.ins.LoadLevelData();

        backBtn.onClick.AddListener(BackBtn_OnClick);

        allBtns = gameObject.GetComponentsInChildren<Button>();

        levelBtns = new List<Button>();

        foreach(Button button in allBtns)
        {
            if (button.name.Contains("Level"))
            {
                levelBtns.Add(button);

                button.gameObject.AddComponent<LevelID>();

                button.gameObject.GetComponent<LevelID>().levelID = sd;

                sd++;

                Debug.Log(button.gameObject.GetComponent<LevelID>().levelID);
            }
        }

        for(int i = 0; i < levelBtns.Count; i++)
        {
            for (int j = 0; j < DataManager.ins.levelCompletedList.levels.Count; j++)
            {
                if (levelBtns[i].GetComponent<LevelID>().levelID == DataManager.ins.levelCompletedList.levels[j].levelId)
                {
                    if (DataManager.ins.levelCompletedList.levels[i].totalStars == 3)
                    {
                        levelBtns[i].transform.GetChild(1).GetComponent<Image>().sprite = starFill;
                        levelBtns[i].transform.GetChild(2).GetComponent<Image>().sprite = starFill;
                        levelBtns[i].transform.GetChild(3).GetComponent<Image>().sprite = starFill;
                    }
                    else if (DataManager.ins.levelCompletedList.levels[j].totalStars == 2)
                    {
                        levelBtns[i].transform.GetChild(1).GetComponent<Image>().sprite = starFill;
                        levelBtns[i].transform.GetChild(2).GetComponent<Image>().sprite = starFill;
                    }
                    else if (DataManager.ins.levelCompletedList.levels[j].totalStars == 1)
                    {
                        levelBtns[i].transform.GetChild(1).GetComponent<Image>().sprite = starFill;
                    }

                    if (levelBtns[i + 1].transform.Find("Lock"))
                    {
                        Transform lockObj = levelBtns[i + 1].transform.Find("Lock");

                        Destroy(lockObj.gameObject);
                    }

                    Debug.Log(levelBtns[i]);
                    int curLevel = int.Parse(levelBtns[i].GetComponentInChildren<TextMeshProUGUI>().text);
                    Debug.Log(curLevel);
                    levelBtns[i].onClick.AddListener(() => LevelBtn_OnClick(curLevel));

                    if (!levelBtns[i + 1].transform.Find("LevelText").gameObject.activeSelf)
                    {
                        levelBtns[i + 1].transform.Find("LevelText").gameObject.SetActive(true);
                    }

                    int nextLevel = int.Parse(levelBtns[i + 1].GetComponentInChildren<TextMeshProUGUI>().text);
                    levelBtns[i + 1].onClick.AddListener(() => LevelBtn_OnClick(nextLevel));
                }
                else
                {
                    if (levelBtns[i].transform.Find("Lock"))
                    {
                        levelBtns[i].onClick.AddListener(LevelBtn_Lock);
                    }
                }
            }

            if(DataManager.ins.levelCompletedList.levels.Count == 0)
            {
                if (levelBtns[i].transform.Find("Lock"))
                {
                    levelBtns[i].onClick.AddListener(LevelBtn_Lock);
                }
                else
                {
                    int availableLevel = int.Parse(levelBtns[i].GetComponentInChildren<TextMeshProUGUI>().text);
                    levelBtns[i].onClick.AddListener(() => LevelBtn_OnClick(availableLevel));
                }
            }
        }

        Debug.Log(DataManager.ins.levelCompletedList.levels.Count);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void LevelBtn_OnClick(int levelId)
    {
        levelID = levelId;

        UIManager.ins.sceneStates = SceneStates.LevelPopup;
    }

    public void LevelBtn_Lock()
    {
        Debug.Log("Is Locked");
    }

    public void BackBtn_OnClick()
    {
        if(UIManager.ins.levelSelectScreenClone != null)
        {
            UIManager.ins.LevelSelectScreen.Hide(UIManager.ins.levelSelectScreenClone);
        }

        UIManager. ins.sceneStates = SceneStates.MainMenu;
    }
}
