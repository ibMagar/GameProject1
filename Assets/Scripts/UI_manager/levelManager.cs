using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class levelManager : MonoBehaviour
{
    public static levelManager instance;
    public event System.Action RestartLevelEvent;
    public event System.Action nextLevelEvent;          //fx Manager, UI Manager

    [Header("variables")]
    [SerializeField] float timeToRestart;
    [SerializeField] float timeToNextLevel;
    [Header("levels")]
    int totalLevels;
    [Space]
    [SerializeField] RectTransform slider1,slider2,timer,timerHand;
    [SerializeField] float slider1XOffsetMin,slider1XOffsetMax;
    [SerializeField] float slider2XOffsetMin,slider2XOffsetMax;
    
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        totalLevels = SceneManager.sceneCountInBuildSettings;
    }
    private void Start()
    {
        //DontDestroyOnLoad(this);
    }
    public void restartLevel()
    {
        StartCoroutine(waitToRestart(timeToRestart));
        if(RestartLevelEvent!=null)
        RestartLevelEvent();                    // 1. subscribed by the camera manager
    }
    IEnumerator waitToRestart(float timeToWait)
    {
        transition(timeToWait);
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void nextLevel()
    {
        if (nextLevelEvent != null)
            nextLevelEvent();           
        StartCoroutine(waitToNextLevel(timeToNextLevel));
    }
    public void loadFirstScene()
    {
        if (nextLevelEvent != null)
            nextLevel();
        StartCoroutine(waitToNextLevel(1f));

    }
    IEnumerator waitToNextLevel(float timeToWait)
    {
             yield return new WaitForSeconds(1f);
            transition(timeToWait);
            yield return new WaitForSeconds(timeToWait);
      
        //load next level
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%totalLevels);
    }
    
     public void transition(float transitionTime)
     {
        /*float offsetTime = transitionTime / 3f;
        timer.DOScale(Vector3.one * .7f, offsetTime);
        slider1.DOAnchorPosX(slider1XOffsetMax, offsetTime);
        slider2.DOAnchorPosX(slider2XOffsetMax, offsetTime).OnComplete(() => { StartCoroutine(cleanTrantition(offsetTime)); });*/
     }
     IEnumerator cleanTrantition(float timeToWait)
     {
        yield return new WaitForSeconds(timeToWait+.15f);
        timer.DOScale(Vector3.zero, timeToWait);
        slider1.DOAnchorPosX(slider1XOffsetMin, timeToWait);
        slider2.DOAnchorPosX(slider2XOffsetMin, timeToWait);
     }

}
