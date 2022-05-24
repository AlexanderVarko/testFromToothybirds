using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton { get; private set; }
    #region VariablesInGame
    public Player player;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverWindow;
    public Sprite defeatSprite;
    public float carrots;
    public float carrotsNeedToWin;
    public Slider slider;
    public GameObject restartButton;
    public GameObject nextLevelButton;
    public GameObject pauseMenu;
    public Image[] stars;
    public Sprite yelowStar;
    #endregion
    #region VariablesInMainMenu
    public int levelProgress;
    public Image[] levels;
    public Sprite openLevel;
    public bool inGame;
    public GameObject logo;
    public GameObject levelSelect;
    public GameObject loadingScreen;
    public Slider loadingBar;
    public TextMeshProUGUI loadingProgress;
    #endregion
    void Start()
    {
        singleton = this;
        if (inGame)
        {
            levelProgress = Save.LoadLevelProgress();
            GameObject[] carrotsOnLevel = GameObject.FindGameObjectsWithTag("Carrot");
            carrotsNeedToWin = carrotsOnLevel.Length;
            slider.maxValue = carrotsNeedToWin;
        }
        if (!inGame)
        {
            levelProgress = Save.LoadLevelProgress();
            for (int i = 0; i < levelProgress; i++)
            {
                levels[i].sprite = openLevel;
                levels[i].GetComponent<Button>().enabled = true;
            }
        }
        
    }

    void Update()
    {
        if (inGame)
        {
            //плавно добавляем морковку в слайдер
            if (slider.value < carrots)
            {
                slider.value += Time.deltaTime;
            }
            //проверяем собрал ли игрок все морковки
        }

    }
    #region Methods
    public void PickUpCarrot()
    {
        carrots += 1;
    }
    /// <summary>
    /// если win то true, если проиграли то false
    /// </summary>
    /// <param name="value"></param>
    public void WinOrLoss(bool value)
    {
        if (value)
        {
            gameOverText.text = "Congratulations, you complete level!!!";
            gameOverWindow.SetActive(true);
            nextLevelButton.SetActive(true);
            Save.OnSaveInt("levelProgress", levelProgress+1);
            var levelProgressCoeficient = carrots / carrotsNeedToWin;
            if (levelProgressCoeficient < 0.5)
            {
                stars[0].sprite = yelowStar;
            }
            else if (levelProgressCoeficient >= 0.5 && levelProgressCoeficient < 1)
            {
                stars[0].sprite = yelowStar;
                stars[1].sprite = yelowStar;
            }
            else if (levelProgressCoeficient == 1)
            {
                stars[0].sprite = yelowStar;
                stars[1].sprite = yelowStar;
                stars[2].sprite = yelowStar;
            }

        }
        else
        {
            gameOverWindow.GetComponent<Image>().sprite = defeatSprite;
            gameOverText.text = "You loss ";
            gameOverWindow.SetActive(true);
            restartButton.SetActive(true);
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        player.movementSM.ChangeState(player.idle);
        pauseMenu.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    #endregion
    #region MainMenu Methods
    public void LoadLevel(int value)
    {
        SceneManager.LoadScene(value);

        //не успеваю доделать LoadingScreen



        //StartCoroutine(LoadingScreenAsync(value));
    }
    public void ShowLevelSelectMenu()
    {
        logo.SetActive(false);
        levelSelect.SetActive(true);
    }
    //IEnumerator LoadingScreenAsync(int value)
    //{
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(value);
    //    asyncLoad.allowSceneActivation = false;
    //    while (!asyncLoad.isDone)
    //    {
    //        loadingBar.value = asyncLoad.progress;
    //        loadingProgress.text = asyncLoad.progress + "%";
    //        if (asyncLoad.progress >= .9f && !asyncLoad.allowSceneActivation)
    //        {
    //            if (Input.anyKeyDown)
    //            {
    //                asyncLoad.allowSceneActivation = true;
    //            }
    //        }
    //        yield return null;
    //    }
    }
    #endregion

