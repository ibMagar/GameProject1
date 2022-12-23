using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class pauseMenu : MonoBehaviour
{
    [Header("buttons")]
    [SerializeField] Button playButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button homeButton;
    [SerializeField] Button soundButton;
    [Header("fill")]
    [SerializeField] Image fillImage;
    [Space]
    [Header("prefabs")]
    [SerializeField] GameObject settingMenu;
    private void Start()
    {
        float fillValue = GameData.FindObjectOfType<UIManager>().transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().fillAmount;
        GameObject.FindObjectOfType<SoundManager>().FadeOutMusic();
        fillImage.DOFillAmount(fillValue, 1f).From(0f).OnComplete(()=> {
            Time.timeScale = 0f;
        });
        //fillImage.fillAmount = fillValue;

    }
    public void Resume(GameObject pausemenu)
    {
        Time.timeScale = 1f;
        GameObject.FindObjectOfType<BoardController>().GetComponent<BoardController>().enabled = true;
        GameObject.FindObjectOfType<SoundManager>().PlayMusic();
        Destroy(pausemenu);
    }
    public void setting()
    {
        // Setting thing
    }
    public void home()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void sound()
    {
        // sound manager
    }
}
