using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underGroundTrigger : MonoBehaviour
{
    [SerializeField] int totalObstacleCount;
    magnet magnetScript;

    public Vector3 pos;
    public BoardController board;
    private void Start()
    {
        magnetScript = GameObject.FindObjectOfType<magnet>();
        board = GameObject.FindObjectOfType<BoardController>(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!GameData.isGameOver)
        {
            string tag = other.tag;
            if (tag.Equals("object"))
            {
                GameData.isGameOver = true;
               /* if (EventHandler.ObjectDroppedEvent != null)
                    EventHandler.ObjectDroppedEvent();
*/
                //
                if (magnetScript.affectedObjects.Contains(other.transform))
                {
                    magnetScript.affectedObjects.Remove(other.transform);
                }

                SoundManager.instance.PlayObjectDropSound();
                board.ObjectDropEffect();
                levelManager.instance.restartLevel();
            }
            else if (tag.Equals("chargeObject"))
            {
                GameData.isGameOver = true;
                SoundManager.instance.PlayObjectDropSound();
                board.ChargeObjectDropEffect();
                levelManager.instance.restartLevel();

            }
            else if (tag.Equals("obstacle"))
            {
                if (magnetScript.affectedObjects.Contains(other.transform))
                {
                    magnetScript.affectedObjects.Remove(other.transform);
                }

                    UIManager.instance.updateLevelProgressBar();
                    board.ObstacleDropEffect();
                    SoundManager.instance.PlayObstacleDropSound();
                cameraManager.instance.smallShake();
            }
            else if (tag.Equals("magnet"))
            {
                UIManager.instance.updateLevelProgressBar();
                board.enableMagnet(6f);

                SoundManager.instance.PlayMagnetDropShortSound();
                cameraManager.instance.mediumShake();
            }

            if (other.transform.parent.CompareTag("camo"))
            {
                //Debug.Log("camo found");
                Destroy(other.transform.parent.gameObject);
            }
            else
            {

                Destroy(other.gameObject);
            }
        
        }
    }
}
