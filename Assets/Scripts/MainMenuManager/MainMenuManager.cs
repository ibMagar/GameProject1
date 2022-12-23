using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class MainMenuManager : MonoBehaviour
{
    #region variables
    [Space]
    [Header("Animations")]
    [SerializeField][Range(.1f,1)] float ButtonAnimationDuration;
    [SerializeField][Range(0,1)] float strength;
    [SerializeField][Range(0,10)] int vibration;
    [SerializeField] RawImage touchEffectSprite;

    [Space]
    [Header("Canvas")]
    [SerializeField] GameObject setting;
    [SerializeField] GameObject levels;
    [SerializeField] GameObject stats;
    [SerializeField] GameObject shop;

    [Header("volumes")]
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider sfxVolume;
    [SerializeField] [Range(0f, 1f)] float defaultMusicVolume;
    [SerializeField] [Range(0f, 1f)] float defaultSFxvolume;
    #endregion
    private void Start()
    {
        musicVolume.value = defaultMusicVolume;
        sfxVolume.value = defaultSFxvolume;
        setMusicVolume();
        setSFXVolume();
    }
    public void play()
    {
        StartCoroutine(loadGameScene());  
    }
    public void switchCanvas(GameObject canvasToSwitch)
    {
        StartCoroutine(SwitchCanvas(canvasToSwitch));
    }
    IEnumerator SwitchCanvas(GameObject c)
    {
        yield return new WaitForSeconds(.2f);
        c.SetActive(true);
    }
    IEnumerator loadGameScene()
    {
        
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(1);
    }

    public void buttonAnimation(GameObject button)
    {
       /* Vector3 initialScale = button.transform.localScale;
        {
            touchEffectSprite.rectTransform.position = button.GetComponent<RectTransform>().position;
            touchEffectSprite.transform.localScale = Vector3.one*.4f;
            touchEffectSprite.GetComponent<RawImage>().DOFade(.3f, .05f);
            touchEffectSprite.transform.DOScale(Vector3.one * 1.5f, .1f).OnComplete(()=>
            {
                touchEffectSprite.GetComponent<RawImage>().DOFade(0f, .05f);
                
            }); 
        }
        button.transform.DOShakeScale(ButtonAnimationDuration,strength, vibration, 90f).OnComplete(
            ()=>
            {
                button.transform.localScale = initialScale;
            });*/
    }
    public static void back(GameObject currrentCanvas)
    {
        currrentCanvas.SetActive(false);
    }
    public void setMusicVolume()
    {
        SoundManager.instance.setMusicvolume(musicVolume.value);
    }
    public void setSFXVolume()
    {
        SoundManager.instance.SetSFXVolume(sfxVolume.value);
    }
}
