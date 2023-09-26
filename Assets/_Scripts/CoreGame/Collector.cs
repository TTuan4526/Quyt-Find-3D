using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collector : MonoBehaviour
{
    public GameObject napCong;
    private Animator napCongAnim;

    private void Start()
    {
        napCongAnim = napCong.GetComponent<Animator>();
    }

    private void RotateObject(GameObject other)
    {
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        // Lấy vị trí của napCong
        Vector3 napCongPosition = napCong.transform.position;

        // Gán vị trí của other bằng vị trí của napCong
        other.transform.position = napCongPosition;

        Quaternion targetRotation = Quaternion.Euler(-120, 0, -180);
        float rotationDuration = 1.0f;

        // Sử dụng Dotween để thực hiện xoay từ từ
        other.transform.DORotateQuaternion(targetRotation, rotationDuration).OnComplete(OnRotationComplete);

        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnRotationComplete()
    {
        //Debug.Log("Rotation complete!"); // Hoặc thực hiện các hành động khác sau khi hoàn thành xoay
    }

    private IEnumerator BlackHole(GameObject other)
    {
        yield return new WaitForSeconds(0.5f);

        Transform targetTransform = other.transform;

        targetTransform.DOScale(Vector3.zero, 0.5f);

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(CheckGoalObj(other));

        napCongAnim.SetBool("Active", false);
    }


    private IEnumerator PushingAway(GameObject other)
    {
        yield return new WaitForSeconds(1f);

        Rigidbody otherRb = other.GetComponent<Rigidbody>();

        if(otherRb != null)
        {
            Vector3 randomForce = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(10f, 30f));

            otherRb.AddForce(randomForce, ForceMode.Impulse);
        }
    }

    private IEnumerator CheckGoalObj(GameObject other)
    {
        string goalIdCheck = other.gameObject.GetComponent<GoalID>().goalID;

        if(goalIdCheck == UIManager.ins.inGameScreenClone.GetComponent<InGameScreen>().lastCardRef)
        {
            int currentValue = int.Parse(UIManager.ins.inGameScreenClone.GetComponent<InGameScreen>().lastCardImage.transform.GetChild(1).gameObject.
                 GetComponent<TMPro.TextMeshProUGUI>().text);

            currentValue--;

            UIManager.ins.inGameScreenClone.GetComponent<InGameScreen>().lastCardImage.transform.GetChild(1).gameObject.
                 GetComponent<TMPro.TextMeshProUGUI>().text = currentValue.ToString();


            yield return new WaitForSeconds(1f);
            if(currentValue == 0)
            {
                UIManager.ins.inGameScreenClone.GetComponent<InGameScreen>().lastCardDestroy = true;
            }
        }

        Destroy(other);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<MoveController>().isDragging)
        {
            if (other.gameObject.CompareTag("GoalObject"))
            {
                string goalIdCheck = other.gameObject.GetComponent<GoalID>().goalID;

                if (goalIdCheck == UIManager.ins.inGameScreenClone.GetComponent<InGameScreen>().lastCardRef)
                {
                    napCongAnim.SetBool("Active", true);
                    RotateObject(other.gameObject);
                    StartCoroutine(BlackHole(other.gameObject));
                }
                else
                {
                    RotateObject(other.gameObject);

                    StartCoroutine(PushingAway(other.gameObject));
                }
            }

            if (other.gameObject.CompareTag("BoardObject"))
            {
                RotateObject(other.gameObject);

                StartCoroutine(PushingAway(other.gameObject));
            }
        }
    }
}
