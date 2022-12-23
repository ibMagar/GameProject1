using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static bool isGameOver;
    public static bool isMoving;
    public LevelData[] LevelDatas;
    public int currentLevel;
    [SerializeField] int levelOffset=0;
    //[SerializeField] LevelData currentLevelData;
    [System.Serializable]
    public class LevelData
    {

        public Material poolMaterial;
        public Material objectMaterial;
        public Material obstacleMaterial;
        public SpriteRenderer borderSprite;
        public SpriteRenderer baseSprite;
        public SpriteRenderer shadowSprite;
        public SpriteRenderer fadeSprite;
        public SpriteRenderer holeBorderSprite;
        public Image progressBarImage;


        [Space]
        [Header("pool")]
        public Color poolColor;
        public Color borderSpriteColor;
        public Color baseSpriteColor;
        public Color shadowSpriteColor;
        public Color HoleBorderColor;
        [Header("background")]
        public Color backgroundColor;
        public Color fadeSpriteColor;
        [Header("progressColor")]
        public Color progressBarImageColor;
        [Header("objects")]
        public Color objectColor;
        public Color obstacleColor;
        [Header("magnet")]
        public float magnetDuration = 10f;
    }
    private void Awake()
    {
        //changeLevelDesign();
        levelOffset = 1;
    }
    void changeLevelDesign()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex - levelOffset;
        //Renderer poolRenderer = GameObject.FindObjectOfType<BoardController>().GetComponent<Renderer>();
        //poolRenderer.material.SetColor("_Color", LevelDatas[currentLevel].poolColor);

        //plattform.GetComponent<Material>().color = LevelDatas[currentLevel].poolColor;
        LevelDatas[currentLevel].poolColor = LevelDatas[currentLevel].poolMaterial.color;
        LevelDatas[currentLevel].objectColor = LevelDatas[currentLevel].objectMaterial.color;
        LevelDatas[currentLevel].obstacleColor = LevelDatas[currentLevel].obstacleMaterial.color;
        LevelDatas[currentLevel].backgroundColor = Camera.main.backgroundColor;
        LevelDatas[currentLevel].borderSpriteColor = LevelDatas[currentLevel].borderSprite.color;
        LevelDatas[currentLevel].baseSpriteColor = LevelDatas[currentLevel].baseSprite.color;
        LevelDatas[currentLevel].shadowSpriteColor = LevelDatas[currentLevel].shadowSprite.color;
        LevelDatas[currentLevel].fadeSpriteColor = LevelDatas[currentLevel].fadeSprite.color;
        LevelDatas[currentLevel].HoleBorderColor = LevelDatas[currentLevel].holeBorderSprite.color;
        LevelDatas[currentLevel].progressBarImageColor = LevelDatas[currentLevel].progressBarImage.color;
    }

    private void OnValidate()
    {
        LevelDatas[currentLevel].poolMaterial.color = LevelDatas[currentLevel].poolColor;
        LevelDatas[currentLevel].objectMaterial.color = LevelDatas[currentLevel].objectColor;
        LevelDatas[currentLevel].obstacleMaterial.color = LevelDatas[currentLevel].obstacleColor;
        Camera.main.backgroundColor = LevelDatas[currentLevel].backgroundColor;
        LevelDatas[currentLevel].borderSprite.color = LevelDatas[currentLevel].borderSpriteColor;
        LevelDatas[currentLevel].baseSprite.color = LevelDatas[currentLevel].baseSpriteColor;
        LevelDatas[currentLevel].shadowSprite.color = LevelDatas[currentLevel].shadowSpriteColor;
        LevelDatas[currentLevel].fadeSprite.color = LevelDatas[currentLevel].fadeSpriteColor;
        LevelDatas[currentLevel].holeBorderSprite.color = LevelDatas[currentLevel].HoleBorderColor;
        LevelDatas[currentLevel].progressBarImage.color = LevelDatas[currentLevel].progressBarImageColor;
    }
    private void Start()
    {
        //currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        //DontDestroyOnLoad(this);
        changeLevelDesign();
        isGameOver = false;
        isMoving = false;
    }
    void changeLevel()
    {
        
    }
}
