using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static JSONManager;

[System.Serializable]
public class SkillSegmentation
{
    public string skill;
    public float time;
}

[System.Serializable]
public class GoalCard
{
    public Sprite objInCard;
    public int numberOfObj;
    public string objRef;
}

[System.Serializable]
public class Goal
{
    public string goalId;
    public GameObject goalObj;
    public int goalCount;
}

[System.Serializable]
public class Board
{
    public string boardId;
    public GameObject boardObj;
    public int boardCount;
}

[System.Serializable]
public class LevelCompleted
{
    public int levelId;
    public int totalStars;
    public int totalTimes;
}

[System.Serializable]
public class LevelCompletedList
{
    public List<LevelCompleted> levels = new List<LevelCompleted>();
}


public class DataManager : MonoBehaviour
{
    public static DataManager ins;

    public LevelCompletedList levelCompletedList;

    public bool firstTimePlay = true;

    public string jsonFilesDirectory = "levels/easy";

    // Khai báo các biến dữ liệu cần thiết trong Game
    public int levelID;
    public int duration;
    public int numberOfCards;
    //public SkillSegmentation[] segmentationOverrides;
    public List<GoalCard> goalCards = new List<GoalCard>();
    public List<Goal> goals = new List<Goal>();
    public List<Board> boards = new List<Board>();

    public List<LevelCompleted> levelsCompleted = new List<LevelCompleted>();


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

    }

    //public void LoadDataInLevel(int currentLevelIndex)
    //{
    //    string jsonFilesDirectory = "Assets/_Assets/Resources/levels/easy";

    //    string[] jsonFiles = Directory.GetFiles(jsonFilesDirectory, "*.json");

    //    string jsonContent = File.ReadAllText(jsonFiles[currentLevelIndex - 1]);

    //    JObject jobject = JObject.Parse(jsonContent);

    //    JSONData jsonData = JsonConvert.DeserializeObject<JSONData>(jobject.ToString());

    //    //Truyền dữ liệu từ currentLevelData cho các biến cần thiết trong GameManager
    //    levelID = currentLevelIndex;
    //    duration = jsonData.duration;

    //    //segmentationOverrides = ;
    //    int index = 0;
    //    for (int i = 0; i < jsonData.goals.Count; i++)
    //    {
    //        for (int j = 0; j < jsonData.goals[i].Count; j++)
    //        {
    //            index++;
    //            numberOfCards = index;

    //            Goal goalList = new Goal(); // Tạo một đối tượng mới ở mỗi lần lặp
    //            goalList.goalId = jsonData.goals[i][j].id;
    //            goalList.goalCount = jsonData.goals[i][j].count;

    //            string prefabsPath = "items/adorable3d/prefabs/" + jsonData.goals[i][j].id;

    //            GameObject prefab = Resources.Load<GameObject>(prefabsPath);
    //            if (prefab != null)
    //            {
    //                goalList.goalObj = prefab;
    //                goals.Add(goalList);
    //            }
    //            else
    //            {
    //                Debug.LogError("Prefab not found at path: " + prefabsPath);
    //            }

    //            GoalCard goalCardList = new GoalCard();
    //            goalCardList.numberOfObj = jsonData.goals[i][j].count;
    //            goalCardList.objRef = jsonData.goals[i][j].id;

    //            string spritePath = "items/adorable3d/portraits/" + jsonData.goals[i][j].id;

    //            Sprite sprite = Resources.Load<Sprite>(spritePath);
    //            if (sprite != null)
    //            {
    //                goalCardList.objInCard = sprite;
    //                goalCards.Add(goalCardList);
    //            }
    //            else
    //            {
    //                Debug.LogError("Sprite not found at path: " + spritePath);
    //            }

    //        }


    //    }

    //    for (int i = 0; i < jsonData.board.Count; i++)
    //    {
    //        Board boardList = new Board();
    //        boardList.boardId = jsonData.board[i].id;
    //        boardList.boardCount = jsonData.board[i].count;

    //        string prefabsPath = "items/adorable3d/prefabs/" + jsonData.board[i].id;

    //        GameObject prefab = Resources.Load<GameObject>(prefabsPath);
    //        if (prefab != null)
    //        {
    //            boardList.boardObj = prefab;
    //            boards.Add(boardList);
    //        }
    //        else
    //        {
    //            Debug.LogError("Prefab not found at path: " + prefabsPath);
    //        }
    //    }

    //    //Sau khi đã truyền dữ liệu, bạn có thể thực hiện các thao tác cần thiết để bắt đầu level.
    //}

    public void LoadDataInLevel(int currentLevelIndex)
    {

        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>(jsonFilesDirectory);

        TextAsset jsonAsset = jsonFiles[currentLevelIndex - 1];

        if (jsonAsset != null)
        {
            string jsonContent = jsonAsset.text;

            JSONData jsonData = JsonConvert.DeserializeObject<JSONData>(jsonContent);

            // Các biến khác
            levelID = currentLevelIndex;
            duration = jsonData.duration;

            for (int i = 0; i < jsonData.goals.Count; i++)
            {
                for (int j = 0; j < jsonData.goals[i].Count; j++)
                {
                    int index = i * jsonData.goals[i].Count + j + 1;
                    numberOfCards = index;

                    Goal goalList = new Goal();
                    goalList.goalId = jsonData.goals[i][j].id;
                    goalList.goalCount = jsonData.goals[i][j].count;

                    string prefabsPath = "items/adorable3d/prefabs/" + jsonData.goals[i][j].id;

                    GameObject prefab = Resources.Load<GameObject>(prefabsPath);
                    if (prefab != null)
                    {
                        goalList.goalObj = prefab;
                        goals.Add(goalList);
                    }
                    else
                    {
                        Debug.LogError("Prefab not found at path: " + prefabsPath);
                    }

                    GoalCard goalCardList = new GoalCard();
                    goalCardList.numberOfObj = jsonData.goals[i][j].count;
                    goalCardList.objRef = jsonData.goals[i][j].id;

                    string spritePath = "items/adorable3d/portraits/" + jsonData.goals[i][j].id;

                    Sprite sprite = Resources.Load<Sprite>(spritePath);
                    if (sprite != null)
                    {
                        goalCardList.objInCard = sprite;
                        goalCards.Add(goalCardList);
                    }
                    else
                    {
                        Debug.LogError("Sprite not found at path: " + spritePath);
                    }
                }
            }

            for (int i = 0; i < jsonData.board.Count; i++)
            {
                Board boardList = new Board();
                boardList.boardId = jsonData.board[i].id;
                boardList.boardCount = jsonData.board[i].count;

                string prefabsPath = "items/adorable3d/prefabs/" + jsonData.board[i].id;

                GameObject prefab = Resources.Load<GameObject>(prefabsPath);
                if (prefab != null)
                {
                    boardList.boardObj = prefab;
                    boards.Add(boardList);
                }
                else
                {
                    Debug.LogError("Prefab not found at path: " + prefabsPath);
                }
            }
        }
        else
        {
            Debug.LogError("JSON Asset not found for level index: " + currentLevelIndex);
        }

        // Các thao tác khác sau khi tải dữ liệu
    }


    public void SaveLevelData(int levelNumber, int totalStars, int totalTimes)
    {
        LevelCompleted levelCompleted = new LevelCompleted();

        levelCompleted.levelId = levelNumber;
        levelCompleted.totalStars = totalStars;
        levelCompleted.totalTimes = totalTimes;

        levelCompletedList.levels.Add(levelCompleted);

        string json = JsonUtility.ToJson(levelCompletedList);
        string jsonFolderPath = Path.Combine(Application.persistentDataPath, "JSON");

        if (!Directory.Exists(jsonFolderPath))
        {
            Directory.CreateDirectory(jsonFolderPath);
        }

        string filePath = Path.Combine(jsonFolderPath, "LevelCompletedData.json");
        File.WriteAllText(filePath, json);
    }

    public void LoadLevelData()
    {
        string jsonFolderPath = Path.Combine(Application.persistentDataPath, "JSON");
        string filePath = Path.Combine(jsonFolderPath, "LevelCompletedData.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            levelCompletedList = JsonUtility.FromJson<LevelCompletedList>(json);
        }
        else
        {
            levelCompletedList = new LevelCompletedList();
        }
    }




    public void ClearLevelData()
    {
        levelCompletedList.levels.Clear();

        string json = JsonUtility.ToJson(levelCompletedList);
        string filePath = Path.Combine(Application.dataPath, "JSON/LevelCompletedData.json");
        File.WriteAllText(filePath, json);
    }
    
}
