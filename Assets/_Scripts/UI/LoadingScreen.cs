using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : BaseScreen
{
    public Image LoadingBar;
    public float fillSpeed = 0.5f;

    private float currentFill = 0f;
    public bool loadingComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetLoadingBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (!loadingComplete && currentFill < 1f)
        {
            currentFill += fillSpeed * Time.deltaTime;

            currentFill = Mathf.Clamp01(currentFill);

            LoadingBar.fillAmount = currentFill;
        }

        if(currentFill == 1f)
        {
            loadingComplete = true;
        }

        if (loadingComplete)
        {
            UIManager.ins.LoadingScreen.Hide(UIManager.ins.loadingScreenClone);

            UIManager.ins.sceneStates = SceneStates.MainMenu    ;
        }
    }

    private void ResetLoadingBar()
    {
        currentFill = 0f;
        LoadingBar.fillAmount = currentFill;
    }

   
}
