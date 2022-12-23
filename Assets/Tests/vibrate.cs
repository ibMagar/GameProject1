#region first code
using UnityEngine;
public static class vibrate
{

#if UNITY_ANDROID && !UNITY_EDITOR

    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;

#endif
    public static void Vibrate(long miliseconds)
    {
        bool _isandroid = isAndroid();
        if (_isandroid)
        {
            vibrator.Call("vibrate", miliseconds);
            Debug.Log("android");
        }
        else
        {
            Handheld.Vibrate();
            Debug.Log("non android");
        }
    }
    public static void Cancel()
    {
        if (isAndroid())
        {
            vibrator.Call("cancel");
        }
    }

    public static bool isAndroid()
    {
#if UNITY_ANDROID
        return true;
#else
        return false;
#endif
    }

}

#endregion

#region second code
/*
using UnityEngine;
public class vibrate : MonoBehaviour
{
#if UNITY_ANDROID || UNITY_EDITOR
    private static AndroidJavaObject plugin = null;
#endif
    void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        plugin = new AndroidJavaClass("uav.uav.uav.UnityAndroidVibrator").CallStatic<AndroidJavaObject>("instance");
#endif
    }


    /// <summary>
    /// <para>Vibrates For Given Amount Of Time.</para>
    /// <para>1 sec = 1000 Millseconds :)</para>
    /// </summary>
    /// <param name="DurationInMilliseconds">Duration in milliseconds.</param>
    public void VibrateForGivenDuration(int DurationInMilliseconds)
    {
        plugin.Call("VibrateForGivenDuration", DurationInMilliseconds);

    }

    /// <summary>
    /// Stoping All Vibrate.
    /// </summary>
    public void StopVibrate()
    {
        plugin.Call("StopVibrate");
    }


    /// <summary>
    /// <para>Customs Vibrate or Vibration with Pattern.</para>
    /// <para>Start without a delay</para>
    /// <para>Each element then alternates between vibrate, sleep, vibrate, sleep...</para>
    /// <para>long[] Pattern = {0, 100, 100, 300};</para>
    /// </summary>
    /// <param name="Pattern">Pattern.</param>
    public void CustomVibrate(long[] Pattern)
    {
        plugin.Call("CustomVibrate", Pattern);
    }


}*/
#endregion


#region third code
/*
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public static class vibrate
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate()
    {
        if (isAndroid())
            vibrator.Call("vibrate");
        else
            Handheld.Vibrate();
    }


    public static void Vibrate(long milliseconds)
    {
        if (isAndroid())
        {
            vibrator.Call("vibrate", milliseconds);
            Debug.Log("android device vibrateed");
        }
        else
        {
            
            //Handheld.Vibrate();
            Debug.Log("non android device vibrated");
        }
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        if (isAndroid())
            vibrator.Call("vibrate", pattern, repeat);
        else
            Handheld.Vibrate();
    }

    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static bool isAndroid()
    {
        bool _isandroid;
#if UNITY_ANDROID && !UNITY_EDITOR
        _isandroid = true;
#else
        _isandroid=false;
        return false;
#endif
        return _isandroid;
    }
}*/
#endregion