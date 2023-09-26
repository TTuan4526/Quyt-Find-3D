using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class JSONManager : MonoBehaviour
{
    [System.Serializable]
    public class GoalItemJSON
    {
        public string id;
        public int count;
    }

    [System.Serializable]
    public class BoardItemJSON
    {
        public string id;
        public int count;
    }
    

    [System.Serializable]
    public class JSONData
    {
        public int duration;
        public string badge;
        public int assist;
        public int ease;
        public List<List<GoalItemJSON>> goals;
        public List<BoardItemJSON> board;
    }
}
