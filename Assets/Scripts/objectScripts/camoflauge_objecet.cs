using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class camoflauge_objecet : MonoBehaviour
{                                                               // Object && Obstacle && Manget Range
    public enum state { Object=0,obstacle,magnet};
    public enum camoState { objectAndObstacle,objectAndmagnet,obstacleAndMagnet };
    
    public GameObject objectGameobject;
    public GameObject obstacleGameobject;
    public GameObject magnetGameobject;
    [Range(1.2f, 4f)]
    public float camoflague_time;
    public state defaultState;
    public camoState camoBehaviour=camoState.obstacleAndMagnet;
    public void Start()
    {
        StartCoroutine(camoflagueBehaviour());
    }
    IEnumerator camoflagueBehaviour()
    {
        int prevValue = 0;
        while(true)
        {
            int x = (int)defaultState;
            while(prevValue==x)
            {
             
                switch(camoBehaviour)
                {
                    case camoState.objectAndObstacle: x = UnityEngine.Random.Range(0, 2); break;
                    case camoState.obstacleAndMagnet: x = UnityEngine.Random.Range(1, 3); break;
                    case camoState.objectAndmagnet: x = UnityEngine.Random.value > .5 ? 0 : 2; break;
                }
               // x = UnityEngine.Random.Range(0, 3);
                yield return null;
                
            }
            prevValue = x;
           
            prevValue = x;
            for (int i = 0; i < 3; i++)
            {
                
                transform.GetChild(i).gameObject.SetActive(i == x);
            }
            yield return new WaitForSeconds(camoflague_time);
            //transform.DOShakePosition(.2f, 1, 10, 90);
            Vector3 currentPosition = transform.position;
            transform.DOJump(currentPosition, .7f, 1, .4f).OnStart(
                ()=>{
                    SoundManager.instance.PlayCamoSound();
                    transform.DORotate(transform.eulerAngles + new Vector3(UnityEngine.Random.Range(180,270f), 0, 0f), .1f);
                    }
                ); 
        }
    }
    
}
