using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class vibrate_operator : MonoBehaviour
{
    public Slider secondsValue;
    public TMP_Text seconds;
    //public vibrate _vibrate;
    public void VibrateRequest()
    {
        vibrate.Vibrate((long)secondsValue.value*1000);
        //Handheld.Vibrate();
        
    }
    private void Update()
    {
        seconds.text = secondsValue.value.ToString("F2");
    }
   
}
