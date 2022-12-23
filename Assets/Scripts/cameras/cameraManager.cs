using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using Microsoft.Win32.SafeHandles;

public class cameraManager : MonoBehaviour
{

    #region singleton
    public static cameraManager instance;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }
    #endregion
    private void Start()
    {
        levelManager.instance.RestartLevelEvent += shakeCamera;
        //levelManager.instance.nextLevelEvent += shakeCamera;
        mainCamera = Camera.main;

        EventHandler.ObstacleDroppedEvent += smallShake;
        EventHandler.MagnetDroppedEvent += mediumShake;
    }
    Camera mainCamera;
    bool _shaking;

    [Header("shortShake")]
    [SerializeField] float _Sduration=.3f;
    [SerializeField] float _Sstrength=1f;
    [SerializeField] int _Svibration = 10;
    [SerializeField] float _Srandomness = 90f;

    [Header("MediumShake")]
    [SerializeField] float _Mduration=.3f;
    [SerializeField] float _Mstrength=1f;
    [SerializeField] int _Mvibration=10;
    [SerializeField] float _Mrandomness = 90f;

    [Header("GameOverShake")]
    [SerializeField] float _duration = .3f;
    [SerializeField] float _strength = 1f;
    [SerializeField] int _vibration = 10;
    [SerializeField] float _randomness = 90f;

    public void shakeCamera()
    {
        if(!_shaking)
        {
        //mainCamera.transform.DOShakePosition(_duration, _strength, _vibration, _randomness).OnStart(()=> { _shaking = true; }).OnComplete(() => { _shaking = false; });
        mainCamera.transform.DOShakeRotation(_duration, _strength, _vibration, _randomness).OnStart(()=> { _shaking = true; }).OnComplete(() => { _shaking = false; });

        }
    }
    public void smallShake()
    {
        if(!_shaking)
        mainCamera.transform.DOShakePosition(_Sduration, _Sstrength, _Svibration, _Srandomness).OnStart(()=>{_shaking=true;}).OnComplete(() => { _shaking = false; });
    }
    public void mediumShake()
    {
        if(!_shaking)
        mainCamera.transform.DOShakePosition(_Mduration, _Mstrength, _Mvibration, _Mrandomness).OnStart(()=>{_shaking=true;}).OnComplete(() => { _shaking = false; });
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            smallShake();
        }if(Input.GetKeyDown(KeyCode.D))
        {
            mediumShake();
        }if(Input.GetKeyDown(KeyCode.F))
        {
            shakeCamera();
        }
    }

}
