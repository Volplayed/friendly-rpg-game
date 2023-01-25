using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //canvases
    public GameObject optionsCanvas;
    public GameObject menuCanvas;

    //player amount panel
    public GameObject playerAmountPanel;

    //game length panel
    public GameObject gameLengthPanel;

    //menu panel
    public GameObject menuPanel;

    //after game stats canvas
    public GameObject afterGameStatsCanvas;

    //scene loader
    public GameObject sceneLoader;

    //button click sound
    public AudioSource buttonClickSound;

    void Start() {
        //on game launch set first time playing
        setFirstTimePlaying();

        //if game started
        if (StaticValuesController.gameStarted) {
            //set after game stats canvas active
            afterGameStatsCanvas.SetActive(true);

            //set menu canvas inactive
            gameObject.SetActive(false);
        }
    }

    //open player amount
    public void openPlayerAmount() {
        //play button click sound
        playButtonClickSound();
        //if first time playing open tutorial
        if (StaticValuesController.firstTimePlaying) {
            openTutorial();
        } else {
            //set player amount panel active
            playerAmountPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
    }

    //close player amount
    public void closePlayerAmount() {
        playerAmountPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    //open game length panel
    public void openGameLength() {
        //set game length panel active
        gameLengthPanel.SetActive(true);

        //set player amount panel inactive
        playerAmountPanel.SetActive(false);
    }

    //close game length panel
    public void closeGameLength() {
        //set game length panel inactive
        gameLengthPanel.SetActive(false);

        //set player amount panel active
        playerAmountPanel.SetActive(true);
    }

    //set player amount
    public void setPlayerAmount(int amount) {
        //play button click sound
        playButtonClickSound();

        //set player amount
        StaticValuesController.playerAmount = amount;

        //set game started to true
        StaticValuesController.gameStarted = true;
            
        //start game
        openGameLength();
    }

    //set game length
    public void setGameLength(int length) {
        //play button click sound
        playButtonClickSound();

        //set game length
        StaticValuesController.finalBossTurn = length;

        //set game started to true
        StaticValuesController.gameStarted = true;

        //start game
        openGame();
    }

    //open main game scene
    private void openGame() {
        //get load scene script
        LoadingGame loadSceneScript = sceneLoader.GetComponent<LoadingGame>();

        //load game statistics
        PlayerGameStatistics.loadGameStatistics();

        //load game scene
        loadSceneScript.LoadScene(1);
    }

    //open tutorial scene
    public void openTutorial() {
        //get load scene script
        LoadingGame loadSceneScript = sceneLoader.GetComponent<LoadingGame>();

        //change first time playing to false
        changeFirstTimePlaying();

        //load tutorial scene
        loadSceneScript.LoadScene(2);
    }

    //open tutorial on click
    public void openTutorialOnClick() {
        //play button click sound
        playButtonClickSound();

        //open tutorial
        openTutorial();
    }

    //close every panel and open menu panel
    public void backToMenu() {
        //play button click sound
        playButtonClickSound();

        //set player amount panel inactive
        playerAmountPanel.SetActive(false);

        //set game length panel inactive
        gameLengthPanel.SetActive(false);

        //set menu panel active
        menuPanel.SetActive(true);
    }

    //play button click sound
    public void playButtonClickSound() {
        buttonClickSound.Play();
    }

    //set first time playing value
    public void setFirstTimePlaying() {
        //get value from player prefs
        bool value = PlayerPrefs.GetInt("firstTimePlaying", 0) != 1;

        StaticValuesController.firstTimePlaying = value;
    }

    //change first time playing value
    public void changeFirstTimePlaying() {
        //set value to player prefs
        PlayerPrefs.SetInt("firstTimePlaying", 1);

        StaticValuesController.firstTimePlaying = false;
    }

    //open options
    public void openOptions()
    {
        optionsCanvas.SetActive(true);
        menuCanvas.SetActive(false);

        //play button click sound
        playButtonClickSound();
    }
    //close options
    public void closeOptions()
    {
        optionsCanvas.SetActive(false);
        menuCanvas.SetActive(true);

        //play button click sound
        playButtonClickSound();
    }

    
}
