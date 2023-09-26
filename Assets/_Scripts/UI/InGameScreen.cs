using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InGameScreen : BaseScreen
{
    [Header("Level&Time")]
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI timeCountDown;
    private int timeCounter;
    private float elapsedTime = 0f;
    public bool isCounting = true;

    [Header("Cards")]
    public GameObject cardBG;
    public Image card;
    public Sprite backGroundCard;
    private List<Image> cardList = new List<Image>();
    public Image lastCardImage;
    public string lastCardRef;
    public bool lastCardDestroy;


    [Header("Button")]
    public Button pauseBtn;
    public Button chargeFanBtn;
    public Button hintBtn;
    public Button findBoostBtn;
    public Button magnetBtn;
    public Button freezeTimeBtn;

    [Header("Fan")]
    public Image shuffleProgressDone;
    public RectTransform rotatePart;
    public GameObject usingDonePart;
    public GameObject mainPart;
    public float fanBGFillSpeed;
    public float fanBGMinusSpeed;
    public float currentFanBGFill;
    private bool loadingFanBGDone;
    public float rotationAmount;
    public float duration;
    public bool notClickFan;
    public bool notShowFan;

    [Header("FreezeTime")]
    public GameObject freezeTimeBG;
    public RectTransform freezeIcon1, freezeIcon2;
    public float rotationSpeed = 30.0f; // Tốc độ xoay

    void Start()
    {
        pauseBtn.onClick.AddListener(PauseBtn_OnClick);
        chargeFanBtn.onClick.AddListener(ChargeFanBtn_OnClick);
        hintBtn.onClick.AddListener(HintBtn_OnClick);
        findBoostBtn.onClick.AddListener(FindBoostBtn_OnClick);
        magnetBtn.onClick.AddListener(MagnetBtn_OnClick);
        freezeTimeBtn.onClick.AddListener(FreezeTimeBtn_OnClick);

        currentLevel.text = "Level " + DataManager.ins.levelID.ToString();
        timeCounter = DataManager.ins.duration;
        UpdateTimeDisplay();


        for (int i = 0; i < DataManager.ins.numberOfCards; i++)
        {
            Image cardBGChilds = Instantiate(card, cardBG.transform);
            cardList.Add(cardBGChilds);
        }

        if (cardList.Count > 0)
        {
            lastCardImage = cardList[cardList.Count - 1];
            RotateLastCardImage(lastCardImage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LastCardDestroy();
        TimeCountDown();
        IdentiyIsWinLevel();
        IdentifyIsLoseLevel();
        
        if(GameManager.ins.gameStates == GameStates.CheckStarAndTime)
        {
            IdentifyStarAndTimeEarn();
        }

        ControllFanBG();
        ControlHint();
        ControlFindBoost();
        ControlMagnet();
        ControlFreezeTime();
    }

    #region Identify lose/win event

    private void IdentiyIsWinLevel()
    {
        if(cardList.Count == 0)
        {
            GameManager.ins.isWinLevel = true;
        }
    }

    private void IdentifyIsLoseLevel()
    {
        if(timeCounter == 0)
        {
            GameManager.ins.isLoseLevel = true;
        }
    }

    private void IdentifyStarAndTimeEarn()
    {
        Time.timeScale = 0;

        if(cardList.Count == 0 && timeCounter >= timeCounter * 0.5)
        {
            GameManager.ins.totalStarEarn = 3;
        }
        else if(cardList.Count == 0 && timeCounter >= timeCounter * 0.3)
        {
            GameManager.ins.totalStarEarn = 2;
        }
        else if(cardList.Count == 0)
        {
            GameManager.ins.totalStarEarn = 1;
        }

        GameManager.ins.totalTimeInLevel = timeCounter;

        DataManager.ins.SaveLevelData(DataManager.ins.levelID, GameManager.ins.totalStarEarn, GameManager.ins.totalTimeInLevel);

        GameManager.ins.getTotalDone = true;
    }
    #endregion

    public void PauseBtn_OnClick()
    {
        Time.timeScale = 0;
        if(UIManager.ins.inGamePauseScreenClone == null)
        {
            UIManager.ins.inGamePauseScreenClone = UIManager.ins.SpawnUI(UIManager.ins.inGamePauseScreen);
        }
    }


    #region ChargeFan Functions
    public void ChargeFanBtn_OnClick()
    {
        chargeFanBtn.GetComponent<Image>().enabled = false;
        rotatePart.gameObject.SetActive(true);
        mainPart.SetActive(true);

        rotatePart.DORotate(new Vector3(0f, 0f, rotationAmount), duration, RotateMode.FastBeyond360)
          .SetEase(Ease.OutQuart).OnComplete(OnCompleteRotateFan);

        GameManager.ins.isUsingFan = true;
        GameManager.ins.UsingFan();
    }

    private void OnCompleteRotateFan()
    {
        rotatePart.gameObject.SetActive(false);
        mainPart.gameObject.SetActive(false);
        usingDonePart.gameObject.SetActive(true);

        notShowFan = true;
    }

    private void ControllFanBG()
    {
        if (GameManager.ins.isUsingFan)
        {
            if (!loadingFanBGDone && currentFanBGFill > 0f)
            {
                currentFanBGFill -= fanBGMinusSpeed * Time.deltaTime;

                currentFanBGFill = Mathf.Clamp01(currentFanBGFill);

                shuffleProgressDone.fillAmount = currentFanBGFill;

                notClickFan = true;
            }

            if (currentFanBGFill == 0f)
            {
                loadingFanBGDone = true;

                GameManager.ins.isUsingFan = false;
            }
        }

        if (notClickFan)
        {
            chargeFanBtn.gameObject.GetComponent<Button>().interactable = false;

            if(loadingFanBGDone && currentFanBGFill < 1f && notShowFan)
            {
                currentFanBGFill += fanBGFillSpeed * Time.deltaTime;

                currentFanBGFill = Mathf.Clamp01(currentFanBGFill);

                shuffleProgressDone.fillAmount = currentFanBGFill;
            }

            if(currentFanBGFill == 1f)
            {
                loadingFanBGDone = false;

                usingDonePart.gameObject.SetActive(false);
                chargeFanBtn.GetComponent<Image>().enabled = true;

                notShowFan = false;

                notClickFan = false;
            }
        }

        if (!notClickFan)
        {
            chargeFanBtn.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    #endregion

    #region Hint Functions
    public void HintBtn_OnClick()
    {
        GameManager.ins.isUsingHint = true;
        GameManager.ins.UsingHint();
    }

    private void ControlHint()
    {
        if (GameManager.ins.isUsingHint)
        {
            hintBtn.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            hintBtn.gameObject.GetComponent<Button>().interactable = true;
        }
    }
    #endregion

    #region Find Boost Functions
    public void FindBoostBtn_OnClick()
    {
        GameManager.ins.isUsingFindBoost = true;
        GameManager.ins.UsingFindBoost();
    }

    private void ControlFindBoost()
    {
        if (GameManager.ins.isUsingFindBoost)
        {
            findBoostBtn.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            findBoostBtn.gameObject.GetComponent<Button>().interactable = true;
        }
    }
    #endregion

    #region Magnet Functions

    public void MagnetBtn_OnClick()
    {
        GameManager.ins.isUsingMagnet = true;
    }

    private void ControlMagnet()
    {
        if (GameManager.ins.isUsingMagnet)
        {
            magnetBtn.GetComponent<Button>().interactable = false;
            GameManager.ins.UsingMagnet();
        }
        else
        {
            magnetBtn.GetComponent<Button>().interactable = true;
        }
    }

    #endregion

    #region Freeze Time Functions
    public void FreezeTimeBtn_OnClick()
    {
        GameManager.ins.isUsingFreezeTime = true;
    }

    private void ControlFreezeTime()
    {
        if (GameManager.ins.isUsingFreezeTime)
        {
            freezeTimeBtn.GetComponent<Button>().interactable = false;
            GameManager.ins.UsingFreezeTime();

            freezeTimeBG.SetActive(true);

            freezeIcon1.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);

            freezeIcon2.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
        else
        {
            freezeTimeBtn.GetComponent<Button>().interactable = true;

            freezeTimeBG.SetActive(false);
        }
    }
    #endregion

    #region Card Functions

    private void RotateLastCardImage(Image lastCard)
    {
        lastCard.rectTransform.DORotate(new Vector3(0f, -1080f, 0f), 0.5f, RotateMode.FastBeyond360)
        .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {

            lastCard.gameObject.GetComponent<Image>().sprite = backGroundCard;
            lastCard.gameObject.GetComponent<Image>().color = new Color(135, 135, 135, 255);
            lastCard.transform.GetChild(0).gameObject.SetActive(true);
            lastCard.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = DataManager.ins.goalCards[DataManager.ins.goalCards.Count - 1].objInCard;
            lastCard.transform.GetChild(1).gameObject.SetActive(true);
            lastCard.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = DataManager.ins.goalCards[DataManager.ins.goalCards.Count - 1].numberOfObj.ToString();
            lastCardRef = DataManager.ins.goalCards[DataManager.ins.goalCards.Count - 1].objRef;
        });
    }

    private void RemoveLast()
    {
        // Lấy chiều dài hiện tại của mảng
        int currentLength = DataManager.ins.goalCards.Count;

        // Kiểm tra nếu mảng có ít nhất một phần tử
        if (currentLength > 0)
        {
            // Điều chỉnh kích thước mảng để loại bỏ phần tử cuối cùng
            DataManager.ins.goalCards.RemoveAt(currentLength - 1);
        }
    }

    private void LastCardDestroy()
    {
        if (lastCardDestroy)
        {
            Destroy(lastCardImage.gameObject);

            cardList.Remove(cardList[cardList.Count - 1]);

            lastCardRef = null;

            RemoveLast();

            if (cardList.Count > 0)
            {
                lastCardImage = cardList[cardList.Count - 1];
                RotateLastCardImage(lastCardImage);
            }

            lastCardDestroy = false;
        }
    }
    #endregion


    #region Time Function
    private void TimeCountDown()
    {
        if (isCounting)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 1f)
            {
                elapsedTime = 0f;
                if (!GameManager.ins.stopTime)
                {
                    timeCounter--;
                }

                if (timeCounter <= 0)
                {
                    timeCounter = 0;
                    isCounting = false;
                }

                UpdateTimeDisplay();
            }
        }
    }

    private void UpdateTimeDisplay()
    {
        int minutes = timeCounter / 60;
        int seconds = timeCounter % 60;

        string timeText = System.String.Format("{0:D2}:{1:D2}", minutes, seconds);
        timeCountDown.text = timeText;
    }

    #endregion
}
