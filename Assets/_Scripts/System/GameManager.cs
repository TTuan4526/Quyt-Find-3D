    using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;

    public GameStates gameStates;

    /// <summary>
    /// Core Game Properties
    /// </summary>
    /// 
    [Header("GameObjects")]
    public GameObject board;
    public GameObject walls;
    public GameObject spawnPos;
    //private int curLevel;

    /// <summary>
    /// Wall
    /// </summary>
    /// 
    [Header("Walls")]
    [HideInInspector] public Wall wall;
    [HideInInspector] public float screenX;
    [HideInInspector] public float screenZ;
    [HideInInspector] public float minX, minZ;
    [HideInInspector] public float maxX, maxZ;

    /// <summary>
    /// Clone
    /// </summary>
    [HideInInspector] public GameObject boardClone, wallsClone, spawnPosClone;

    [Header("Level")]
    public bool loadLevel;
    public bool startLevel;
    public bool isWinLevel;
    public bool isLoseLevel;
    public int levelIDChosen;

    //Check Star and Time Condition;
    public int totalStarEarn;
    public int totalTimeInLevel;
    public bool getTotalDone;


    [Header("Fan Ability")]
    /// <summary>
    /// using Abilitiy Fan
    /// </summary>
    /// 
    public float upForce = 5.0f;
    public float maxTiltAngle = 20.0f;
    public float minForce = 1.0f;
    public float maxForce = 3.0f;
    public bool isUsingFan;

    [Header("Hint Ability")]
    /// <summary>
    /// using Ability Hint
    /// </summary>
    /// 
    public bool isUsingHint;
    public GameObject hintEffect;

    [Header("Find Boost Ability")]
    public bool isUsingFindBoost;
    public GameObject findBoostEffect;

    [Header("Magnet Ability")]
    public bool isUsingMagnet;
    public GameObject magnetEffect;
    public float magnetForce;
    public bool movingUpDone;
    Vector3 upTargerPos;
    GameObject magnetEffectClone;

    [Header("FreezeTime Ability")]
    public bool isUsingFreezeTime;
    public float freezeTime = 10f;
    public float freezeTimeCounter = 0;
    public bool getFreezeTime;
    public bool stopTime;

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

        screenX = Camera.main.ScreenToWorldPoint(new Vector3(screenX, 0, 0)).x;
        screenZ = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, screenZ)).z;

        minX = new Vector3(screenX, 0, 0).x;
        maxX = new Vector3(-screenX, 0, 0).x;


        minZ = new Vector3(0, 0, screenZ + 4f).z;
        maxZ = new Vector3(0, 0, -screenZ - 6.5f).z;
    }

    private void Start()
    {
        gameStates = GameStates.None;
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (gameStates)
        {
            case GameStates.None:
                Init();
                break;
            case GameStates.LoadLevelData:
                LoadLevelData();
                break;
            case GameStates.NormalPlay:
                NormalPlay();
                break;
            case GameStates.CheckStarAndTime:
                CheckStarAndTime();
                break;
            case GameStates.WinLevel:
                WinLevel();
                break;
            case GameStates.LoseLevel:
                LoseLevel();
                break;
        }
    }

    private void Init()
    {
        if (boardClone != null)
        {
            Destroy(boardClone);
        }
        if (wallsClone != null)
        {
            Destroy(wallsClone);
        }
        if (spawnPosClone != null)
        {

            Destroy(spawnPosClone);
        }

        GameObject[] boardObjsLeft = GameObject.FindGameObjectsWithTag("BoardObject");
        GameObject[] goalObjsLeft = GameObject.FindGameObjectsWithTag("GoalObject");
        if (goalObjsLeft.Length != null || boardObjsLeft.Length != null)
        {
            GameObject[] allObjects = new GameObject[goalObjsLeft.Length + boardObjsLeft.Length];
            goalObjsLeft.CopyTo(allObjects, 0);
            boardObjsLeft.CopyTo(allObjects, goalObjsLeft.Length);
            foreach (var obj in allObjects)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
        }

        isWinLevel = false;
        isLoseLevel = false;
        getTotalDone = false;

        Time.timeScale = 1;
        levelIDChosen = 0;

        DataManager.ins.goals.Clear();
        DataManager.ins.boards.Clear();
    }

    private void LoadLevelData()
    {
        levelIDChosen = UIManager.ins.levelSelectScreenClone.GetComponent<LevelSelectScreen>().levelID;
        DataManager.ins.LoadDataInLevel(levelIDChosen);

        gameStates = GameStates.NormalPlay;
    }

    private void NormalPlay()
    {
        UIManager.ins.sceneStates = SceneStates.InGameScreen;

        if (boardClone == null)
        {
            boardClone = Instantiate(board, transform);
        }
        if (wallsClone == null)
        {
            wallsClone = Instantiate(walls, transform);
        }
        if (spawnPosClone == null)
        {
            spawnPosClone = Instantiate(spawnPos, transform);
        }

        if (isWinLevel)
        {
            gameStates = GameStates.CheckStarAndTime;
        }

        if (isLoseLevel)
        {
            gameStates = GameStates.LoseLevel;
        }
    }

    private void CheckStarAndTime()
    {
        if (getTotalDone)
        {
            gameStates = GameStates.WinLevel;
        }
    }

    private void WinLevel()
    {
        UIManager.ins.sceneStates = SceneStates.WinLevelScreen;
    }

    private void LoseLevel()
    {
        Time.timeScale = 0;

        if (UIManager.ins.levelFailedScreenClone == null)
        {
            UIManager.ins.levelFailedScreenClone = UIManager.ins.SpawnUI(UIManager.ins.levelFailedScreen);
        }
    }

    public void UsingFan()
    {
        // Tìm và lọc các GameObject có tag là "GoalObject"
        GameObject[] goalObjects = GameObject.FindGameObjectsWithTag("GoalObject");

        // Tìm và lọc các GameObject có tag là "BoardObject"
        GameObject[] boardObjects = GameObject.FindGameObjectsWithTag("BoardObject");

        // Gộp các mảng vào mảng chung (nếu cần)
        GameObject[] allObjects = new GameObject[goalObjects.Length + boardObjects.Length];
        goalObjects.CopyTo(allObjects, 0);
        boardObjects.CopyTo(allObjects, goalObjects.Length);

        if (isUsingFan)
        {
            foreach (var obj in allObjects)
            {
                Rigidbody objRb = obj.GetComponent<Rigidbody>();
                if (objRb != null)
                {
                    Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(minForce, maxForce);
                    objRb.AddForce(randomForce * upForce, ForceMode.Impulse);

                    Vector3 randomTilt = new Vector3(Random.Range(-maxTiltAngle, maxTiltAngle), Random.Range(-maxTiltAngle, maxTiltAngle), Random.Range(-maxTiltAngle, maxTiltAngle));
                    objRb.AddTorque(randomTilt, ForceMode.Impulse);

                    objRb.useGravity = true;
                }
            }
        }
    }

    public void UsingHint()
    {
        if (isUsingHint)
        {
            string objNameToHint = DataManager.ins.goalCards[DataManager.ins.goalCards.Count - 1].objInCard.name;

            GameObject objToHint = GameObject.Find(objNameToHint + "(Clone)");

            GameObject hintEffectClone = Instantiate(hintEffect, objToHint.transform.position + new Vector3(0, 2f, 0), Quaternion.identity);

            StartCoroutine(DestroyEffect(hintEffectClone, 3f));
        }
    }

    public void UsingFindBoost()
    {
        if (isUsingFindBoost)
        {
            string objNameToHint = DataManager.ins.goalCards[DataManager.ins.goalCards.Count - 1].objInCard.name;

            // Tìm tất cả các gameobject có cùng tên
            GameObject[] objectsWithSameName = GameObject.FindObjectsOfType<GameObject>()
                .Where(obj => obj.name.StartsWith(objNameToHint))
                .ToArray();

            List<GameObject> firstThreeObjects = new List<GameObject>();

            // Lấy 3 phần tử đầu tiên hoặc ít hơn nếu không đủ 3
            for (int i = 0; i < Mathf.Min(3, objectsWithSameName.Length); i++)
            {
                firstThreeObjects.Add(objectsWithSameName[i]);
            }

            foreach (var obj in firstThreeObjects)
            {
                GameObject hintEffectClone = Instantiate(findBoostEffect, obj.transform.position + new Vector3(0, 2f, 0), Quaternion.identity);

                StartCoroutine(DestroyEffect(hintEffectClone, 3f));
            }
        }
    }

    public void UsingMagnet()
    {
        if (isUsingMagnet)
        {
            string objNameToHint = DataManager.ins.goalCards[DataManager.ins.goalCards.Count - 1].objInCard.name;

            GameObject objToHint = GameObject.Find(objNameToHint + "(Clone)");

            GameObject collector = boardClone.transform.GetChild(1).gameObject;

            if(magnetEffectClone == null)
            {
                magnetEffectClone = Instantiate(magnetEffect, collector.transform.position, collector.transform.rotation);

                magnetEffectClone.transform.position = new Vector3(collector.transform.position.x,2,collector.transform.position.z);

                magnetEffectClone.transform.rotation = Quaternion.Euler(90,0,0);

                magnetEffectClone.transform.SetParent(collector.transform);
            }

            Rigidbody objRb = objToHint.GetComponent<Rigidbody>();

            objRb.isKinematic = true;

            Quaternion targetRotation = Quaternion.Euler(-120, 0, -180);

            objToHint.transform.rotation = targetRotation;

            float distanceThreshold = 0.01f; // Ngưỡng khoảng cách để ngừng di chuyển

            if (upTargerPos == Vector3.zero)
            {
                upTargerPos = objToHint.transform.position + new Vector3(0, 2f, 0);
            }

            if (!movingUpDone)
            {
                float distanceToTarget = Vector3.Distance(objToHint.transform.position, upTargerPos);
                if (distanceToTarget >= distanceThreshold)
                {
                    objToHint.transform.position = Vector3.MoveTowards(objToHint.transform.position, upTargerPos, magnetForce * Time.deltaTime);
                }
                else
                {
                    // Đã đạt đến vị trí gần đúng với upTargerPos
                    // Bạn có thể thực hiện các hành động cần thiết ở đây
                    movingUpDone = true; // Đánh dấu rằng đã hoàn thành việc di chuyển
                }
            }

            if (movingUpDone)
            {
                objToHint.transform.position = Vector3.MoveTowards(objToHint.transform.position, collector.transform.position, magnetForce * Time.deltaTime);

                if(objToHint.transform.position == collector.transform.position)
                {
                    movingUpDone = false;

                    isUsingMagnet = false;
                    upTargerPos = Vector3.zero;

                    if(magnetEffectClone != null)
                    {
                        StartCoroutine(DestroyEffect(magnetEffectClone, 1f));
                    }
                }
            }
        }
    }

    public void UsingFreezeTime()
    {
        if (isUsingFreezeTime)
        {
            if (!getFreezeTime)
            {
                freezeTimeCounter = freezeTime;

                getFreezeTime = true;
            }

            freezeTimeCounter -= Time.deltaTime;

            if (freezeTimeCounter > 0)
            {
                stopTime = true;
            }
            else
            {
                stopTime = false;

                isUsingFreezeTime = false;

                getFreezeTime = false;
            }
        }
    }
    private IEnumerator DestroyEffect(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(obj);

        isUsingHint = false;
        isUsingFindBoost = false;
    }
}


