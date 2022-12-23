using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public enum ObstacleType
{
    normal,jumping,movingObj,scalingObj
};
public class obstaclesScript : MonoBehaviour
{

    public  ObstacleType type=ObstacleType.normal;
    [Header("Jump Behaviour")]
    [Range(1f, 3f)] public float minJumpTime;
    [Range(3f, 7f)] public float maxJumpTime;
    private float jumpInterval;
    [Range(.1f,1f)]
    public float jumpTime;

    [Header("moving behaviour")]
    public Transform TargetPosition;
    public float minMoveSpeed;
    public float maxMoveSpeed;
    [SerializeField] float moveSpeed;
    public Coroutine moveCoroutine;

    [Header("scaling behaviour")]
    public float minScaling;
    public float maxScaling;
    public float minScalingTime;
    public float maxScalingTime;
    public float scalingTime;
    private void Start()
    {
        switch(type)
        {
            case ObstacleType.normal:  break;
            case ObstacleType.jumping: jumpInterval = UnityEngine.Random.Range(minJumpTime, maxJumpTime);
                                       StartCoroutine(jump());   
                                       break;
            case ObstacleType.movingObj: moveSpeed = UnityEngine.Random.Range(minMoveSpeed, maxMoveSpeed);
                                         moveCoroutine= StartCoroutine(move());
                                         break;
            case ObstacleType.scalingObj:  scalingTime = UnityEngine.Random.Range(minScaling, maxScaling);
                                                StartCoroutine(scale());
                                            break;
        }
        
    }
    IEnumerator jump()
    {
        while (true)                // always loops 
        {
            yield return new WaitForSeconds(jumpInterval);
            transform.DOJump(transform.position, 1f, 1, .5f).OnStart(
                () =>
                {
                    float x = UnityEngine.Random.value;
                    if (x < 3.3f)
                    {
                        transform.DORotate(transform.localEulerAngles + new Vector3(180f, 0f, 0f), jumpTime);
                    }
                    else if (x < 6.6f)
                    {
                        transform.DORotate(transform.localEulerAngles + new Vector3(0f, 180f, 0f), jumpTime);
                    }
                    else
                    {
                        transform.DORotate(transform.localEulerAngles + new Vector3(0f, 0f, 180f), jumpTime);
                    }
                });
        }
    }
     IEnumerator move()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = TargetPosition.position;
        while(true)
        {
            /*transform.DOMove(targetPosition, moveSpeed).OnComplete(() =>
            {
                Vector3 temp = currentPosition;
                currentPosition = targetPosition;
                targetPosition = temp;
            });*/
            float timer = 0f;
            SoundManager.instance.PlayslideSound();
            while(transform.position!=targetPosition)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, timer/moveSpeed);
                timer += Time.deltaTime;
                yield return null;
            }
            //yield return new WaitForSeconds(moveSpeed);
            Vector3 temp = currentPosition;
            currentPosition = targetPosition;
            targetPosition = temp;
        }
    }
    public void stopMoving()
    {
        StopCoroutine(moveCoroutine);
    }
    IEnumerator scale()
    {
        Vector3 scalingFactor = transform.localScale;
        while(true)
        {
            transform.DOShakeRotation(.5f, 90, 10, 90);
            yield return new WaitForSeconds(.2f);
            transform.DOScale(scalingFactor * maxScaling,scalingTime);
            yield return new WaitForSeconds(scalingTime);
            transform.DOShakeRotation(.3f, 90, 10, 90);
            yield return new WaitForSeconds(.15f);
            transform.DOScale(scalingFactor * 1f, scalingTime);
            yield return new WaitForSeconds(scalingTime);
        }
    }

    private void OnDrawGizmos()
    {
        if(type==ObstacleType.movingObj)
        {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, TargetPosition.position);
        }
        else if(type==ObstacleType.scalingObj)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale * maxScaling);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, transform.localScale * minScaling);
        }
    }
}
