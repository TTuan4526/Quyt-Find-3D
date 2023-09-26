using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private Rigidbody rb;
    private MouseSpeedTracker mouseSpeedTracker;
    public bool isDragging = false;
    public float throwForceMultiplier = 1.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mouseSpeedTracker = FindObjectOfType<MouseSpeedTracker>();
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if (transform.position.x < GameManager.ins.minX || transform.position.x > GameManager.ins.maxX)
            {
                // Set new position only along the Z axis
                objPosition = new Vector3(objPosition.x, 1.7f, Mathf.Clamp(objPosition.z, GameManager.ins.minZ, GameManager.ins.maxZ));
            }
            else if (transform.position.z < GameManager.ins.minZ || transform.position.z > GameManager.ins.maxZ)
            {
                // Set new position only along the X axis
                objPosition = new Vector3(Mathf.Clamp(objPosition.x, GameManager.ins.minX, GameManager.ins.maxX), 1.7f, objPosition.z);
            }
            else
            {
                // Set new position for both X and Z axes
                objPosition = new Vector3(Mathf.Clamp(objPosition.x, GameManager.ins.minX, GameManager.ins.maxX), 1.7f, Mathf.Clamp(objPosition.z, GameManager.ins.minZ, GameManager.ins.maxZ));
            }

            rb.MovePosition(objPosition);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        rb.isKinematic = true;

        // Calculate offset for dragging
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;

        float mouseSpeed = mouseSpeedTracker.GetTouchSpeed();
        Vector3 throwDirection = (transform.position - Camera.main.transform.position).normalized;
        throwDirection.y = 0;
        rb.AddForce(throwDirection * mouseSpeed * throwForceMultiplier, ForceMode.Impulse);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPoint.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
