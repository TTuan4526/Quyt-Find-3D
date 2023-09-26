using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    public GameObject[] goalPrefabs; // Mảng chứa các Prefabs tương ứng với các vật thể cần tìm
    public int[] goalQuantities; // Mảng chứa số lượng tương ứng với các vật thể cần tìm

    public GameObject[] boardPrefabs; // Mảng chứa các Prefabs tương ứng với các vật thể ngoại lai
    public int[] boardQuantities; // Mảng chứa số lượng tương ứng với các vật thể ngoại lai

    private void Start()
    {
        PopulateGoalPrefabs();
        PopulateBoardPrefabs();
        SpawnObjects();
    }

    private void PopulateGoalPrefabs()
    {
        goalPrefabs = new GameObject[DataManager.ins.goals.Count];
        for (int i = 0; i < DataManager.ins.goals.Count; i++)
        {
            goalPrefabs[i] = DataManager.ins.goals[i].goalObj;

            if (goalPrefabs[i].GetComponent<GoalID>() == null)
            {
                goalPrefabs[i].AddComponent<GoalID>().goalID = DataManager.ins.goals[i].goalId;
            }

            if (goalPrefabs[i].GetComponent<MouseSpeedTracker>() == null)
            {
                goalPrefabs[i].AddComponent<MouseSpeedTracker>();
            }
        }
    }

    private void PopulateBoardPrefabs()
    {
        boardPrefabs = new GameObject[DataManager.ins.boards.Count];
        for (int i = 0; i < DataManager.ins.boards.Count; i++)
        {
            boardPrefabs[i] = DataManager.ins.boards[i].boardObj;

            if (boardPrefabs[i].GetComponent<MouseSpeedTracker>() == null)
            {
                boardPrefabs[i].AddComponent<MouseSpeedTracker>();
            }
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < DataManager.ins.goals.Count; i++)
        {
           StartCoroutine(SpawnObjectsOfType(DataManager.ins.goals[i].goalObj, DataManager.ins.goals[i].goalCount, "GoalObject"));
        }

        for (int i = 0; i < DataManager.ins.boards.Count; i++)
        {
            StartCoroutine(SpawnObjectsOfType(DataManager.ins.boards[i].boardObj, DataManager.ins.boards[i].boardCount, "BoardObject"));
        }
    }

    private IEnumerator SpawnObjectsOfType(GameObject objectPrefab, int quantity, string tag)
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-3f,3f), 2f, Random.Range(-1f, 2f));
            GameObject spawnObj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
            spawnObj.tag = tag;
        }
    }
}
