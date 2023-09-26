using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] BoxCollider leftWall;
    [SerializeField] BoxCollider rightWall;

    private float screenX;
    private void Start()
    {
        screenX = GameManager.ins.screenX;

        rightWall.transform.position = new Vector3(-screenX - (rightWall.size.x / 2f) + 1.01f, 0f, 0f);
        leftWall.transform.position = new Vector3(screenX + (leftWall.size.x / 2f) - 1.01f, 0f, 0f);

    }

}
