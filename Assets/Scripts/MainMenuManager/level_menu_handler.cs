using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class level_menu_handler : MonoBehaviour
{

    //[SerializeField] GameObject levelParent;
    [SerializeField] GameObject[] levelItems;

    [SerializeField] Color PrimaryButtonColor;
    [SerializeField] Color PrimaryShadowColor;

    [SerializeField] Color SecondaryButtonColor;
    [SerializeField] Color SecondaryShadowColor;

    [SerializeField] int _completed_level;
    [SerializeField] float levelLoadDelay;
    private void Awake()
    {
        PlayerPrefs.SetInt("completedLevel", 3);
         _completed_level = PlayerPrefs.GetInt("completedLevel");

        PrimaryButtonColor.a = 1f;
        PrimaryShadowColor.a = 1f;
        SecondaryButtonColor.a = 1f;
        SecondaryShadowColor.a = 1f;
        for(int i=0;i<levelItems.Length;i++)
        {
            //levelItems[i] = levelParent.transform.GetChild(i).gameObject;
            if(i<_completed_level)
            {
            levelItems[i].GetComponent<Image>().color = SecondaryButtonColor;
            levelItems[i].transform.GetChild(0).GetComponent<Image>().color = SecondaryShadowColor;
            }
            else
            {
                levelItems[i].GetComponent<Image>().color = PrimaryButtonColor;
                levelItems[i].transform.GetChild(0).GetComponent<Image>().color = PrimaryShadowColor;
            }
        }
    }
    public void loadLevel(int x)
    {
        //play click sound
        if (x < PlayerPrefs.GetInt("completedLevel"))
            StartCoroutine(loadLevel_cor(x));
        else
            Debug.Log("level locked");
    }
    IEnumerator loadLevel_cor(int x)
    {
        yield return new WaitForSeconds(levelLoadDelay);
        SceneManager.LoadScene(x);
    }

}
