using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpeedTracker : MonoBehaviour
{
    private Vector3 previousTouchPosition;
    private float touchSpeed;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector3 currentTouchPosition = Input.GetTouch(0).position;

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchSpeed = (currentTouchPosition - previousTouchPosition).magnitude / Time.deltaTime / 1000;
            }

            previousTouchPosition = currentTouchPosition;
        }
    }

    public float GetTouchSpeed()
    {
        return touchSpeed;
    }
}
