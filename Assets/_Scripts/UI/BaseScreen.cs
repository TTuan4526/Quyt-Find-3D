using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public void Hide(GameObject ui)
    {
        Destroy(ui);
    }
}
