using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class chargeObject : MonoBehaviour
{
    #region variables region
    //
    [SerializeField] Transform holeCenterPosition;
    [Header("jump")]
    [Range(1,3)] public int jumpNo;
    [Header("time")]
    public float minOffsetTime;
    public float maxOffsetTime;
    public float offsetTime;
    [Header("speed")]
    public float minjumpSpeed;
    public float maxJumpSpeed;
    public float jumpSpeed;
    [Header("power")]
    public float power;
    public float targetOffsetPosition;
    [Header("chargetime")]
    public float chargeTime;
    #endregion
    private void Start()
    {
        holeCenterPosition = GameObject.FindGameObjectWithTag("Player").transform;

        offsetTime = UnityEngine.Random.Range(minOffsetTime, maxOffsetTime);
        jumpSpeed = UnityEngine.Random.Range(minjumpSpeed, maxJumpSpeed);
        StartCoroutine(ChargeAndAttack());
    }
    IEnumerator ChargeAndAttack()
    {
        yield return new WaitForSeconds(offsetTime-chargeTime);
        Vector3 positionToJump = holeCenterPosition.position + new Vector3(Random.Range(0, targetOffsetPosition),.2f , Random.Range(0, targetOffsetPosition));
        transform.DOShakeScale(chargeTime, .5f, 3, 90);
        //transform.DOShakePosition(chargeTime, .2f, 1.5, 0);
        yield return new WaitForSeconds(chargeTime);
        transform.DOJump(positionToJump, power, jumpNo, jumpSpeed).OnStart(()=> {
            SoundManager.instance.PlayJumpSound();
        }).OnComplete(()=>{
            StartCoroutine(ChargeAndAttack());
            //Debug.Log(positionToJump);
        });
    }

}
