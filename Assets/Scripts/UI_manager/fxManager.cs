using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fxManager : MonoBehaviour
{

    [SerializeField] ParticleSystem winFx;

    private void Start()
    {
        if (GameObject.FindObjectOfType<levelManager>() != null)
            levelManager.instance.nextLevelEvent += playWinFx;
        else
            print("levelMangar not found!");
    }
    public void playWinFx()
    {
        if(winFx!=null)
        winFx.Play();
    }

}
