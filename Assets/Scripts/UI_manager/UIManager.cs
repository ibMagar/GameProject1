using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{

    #region singleton declaration
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    [Header("UI")]
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] TMP_Text nextLevelText;
    [SerializeField] Image progressFillImage;
    [SerializeField] TMP_Text levelCompleteText;

    [Header("Variables")]
    [SerializeField] int currentLevel;
    [SerializeField] Transform obstaclesParent;
    [SerializeField] Transform magnetParent;
    [SerializeField] Transform camoParent;
    public int totalObstaclesNo;
    public int currentObstacleCount;
    [SerializeField] float fadeImageRate;

    [Header("coroutines")]
    [SerializeField] float ProgressBarUpdateRate;
    [Header("prefabs")]
    [Space]
    [SerializeField] GameObject pauseMenu;
    private void Start()
    {
        //cameraManager.instance.shakeCamera();

        progressFillImage.fillAmount = 0f;
        totalObstaclesNo = obstaclesParent.childCount+magnetParent.childCount+camoParent.childCount;
        currentObstacleCount = 0;
        currentLevel = GameObject.FindObjectOfType<GameData>().currentLevel;
        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();
/*        if (GameObject.FindObjectOfType<levelManager>() != null)
            levelManager.instance.nextLevelEvent += increaseLevel;
        else print("levelManager not found");*/
        
    }
   
     void increaseLevel()
     {
        currentLevel++;
        updateLevelText();
     }
    public void updateLevelText()
    {
       
        if(GameObject.FindObjectOfType<UIManager>()!=null)
           StartCoroutine(showLevelCompleteText(.25f, 1f));       
    }
    IEnumerator showLevelCompleteText(float waitTime,float fadeRate)
    {
        yield return new WaitForSeconds(waitTime);
        levelCompleteText.DOFade(1f, 1f);
    }
   public void updateLevelProgressBar()
    {
        currentObstacleCount++;
        if (currentObstacleCount < totalObstaclesNo)
        {
            float currentFillAmount = progressFillImage.fillAmount;
            float fillCount = ((float)currentObstacleCount / totalObstaclesNo);
            StartCoroutine(changeFillAmount(currentFillAmount, fillCount,ProgressBarUpdateRate));
        }
        else if(currentObstacleCount>=totalObstaclesNo)
        {
            StartCoroutine(changeFillAmount(progressFillImage.fillAmount, 1, ProgressBarUpdateRate));
            increaseLevel();
            if (GameObject.FindObjectOfType<levelManager>() != null)
                levelManager.instance.nextLevel();
            else print("LevelManager not found!");
        }
    }
   public IEnumerator changeFillAmount(float currentFillAmount, float fillCount, float rateOfChange,float waitTime=0f)
    {
        yield return new WaitForSeconds(waitTime);
        float percent = 0f;
        while(percent<1f)
        {
            percent += Time.deltaTime*rateOfChange;
            progressFillImage.fillAmount = Mathf.Lerp(currentFillAmount, fillCount, percent);
            yield return null;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            levelManager.instance.restartLevel();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            levelManager.instance.nextLevel();
        }
    }
   public void pause()
    {
        if (GameObject.FindObjectOfType<pauseMenu>() == null)
        {
            GameObject.FindObjectOfType<BoardController>().GetComponent<BoardController>().enabled = false;
            GameObject pauseMenuObject = Instantiate(pauseMenu);
        }
    }

}
