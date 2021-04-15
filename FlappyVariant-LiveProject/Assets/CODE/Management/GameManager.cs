using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Flappy.Player;

namespace Flappy.Management
{
    #region Component Overview
    /// <summary>
    /// [Keeps track of the game state, ie. isGameOver?]
    /// </summary>
    #endregion

    public class GameManager : MonoBehaviour
    {
        #region Public Variables
        // Boolean representing gameOver state
        [SerializeField] private bool b_isGameOver = false;

        [SerializeField] private RectTransform StartMenuPanel = null;
        [SerializeField] private RectTransform GameOverPanel = null;
        [SerializeField] private Text HighScoreTextValue = null;

        // Singleton reference for GameManager
        public static GameManager GM_Instance = null;
        #endregion



        #region Private Variables
        private PlayerController player = null;
        private BackgroundSoundManager bgSound = null;
        private ScoreSystem scoreSystem = null;

        private ObstacleManager obstacles = null;
        private ProjectileManager projectiles = null;
        private ParticleManager particles = null;
        #endregion



        #region MonoBehaviour Methods


        private void OnEnable()
        {

            // Check if singleton has a reference
            if(GM_Instance != null)
            {
                // check if the reference is assigned to another gameObject
                if (GM_Instance != this)
                {
                    // if true, destroy this gameObject
                    Destroy(this);
                }
                // if reference is assigned to this object
                else if (GM_Instance == this)
                {
                    // return out of function
                    return;
                }
            }
            // if the reference isn't assigned
            else if(GM_Instance == null)
            {
                // set the reference to this gameObject
                GM_Instance = this;
            }


            Time.timeScale = 0f;

            if (!StartMenuPanel.gameObject.activeSelf)
            {
                StartMenuPanel.gameObject.SetActive(true);
            }

            if (GameOverPanel.gameObject.activeSelf)
            {
                GameOverPanel.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            bgSound = BackgroundSoundManager.SM_Instance;
            scoreSystem = ScoreSystem.SS_Instance;
            particles = ParticleManager.PA_Instance;
            projectiles = ProjectileManager.PM_Instance;

            obstacles = FindObjectOfType<ObstacleManager>();

            player = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {

            // if gameOver boolean is true
            if (b_isGameOver)
            {
                

                // * KEYBOARD CONTROLS*
                //check for player input and if TimeScale is set to 0
                if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0)
                {
                    
                }

                // *TOUCH CONTROLS*
                if (Input.touchCount > 0 && Time.timeScale == 0)
                {
                    bgSound.SetVolume(false);
                    scoreSystem.ResetScore();

                    // reset gameOver boolean to false
                    b_isGameOver = false;

                    // reset TimeScale to 1
                    Time.timeScale = 1;
                }
            }
        }

        #endregion



        #region Custom Methods

        public void SetGameOver()
        {
            b_isGameOver = true;

            // check if TimeScale is not 0
            if (Time.timeScale != 0)
            {
                // set TimeScale to 0
                Time.timeScale = 0;

                bgSound.SetVolume(true);

                GameOverPanel.gameObject.SetActive(true);

                if(HighScoreTextValue == null) { return; }

                HighScoreTextValue.text = scoreSystem.GetScore().ToString();
            }

            ResetAllGameElements();
        }

        public void StartGame()
        {
            if(StartMenuPanel == null) { return; }

            StartMenuPanel.gameObject.SetActive(false);

            Time.timeScale = 1f;
        }

        public void RestartGame()
        {
            if(GameOverPanel == null) { return; }

            GameOverPanel.gameObject.SetActive(false);

            bgSound.SetVolume(false);

            scoreSystem.ResetScore();

            // reset gameOver boolean to false
            b_isGameOver = false;

            // reset TimeScale to 1
            Time.timeScale = 1;
        }


        void ResetAllGameElements()
        {
            player.ResetPlayer();

            obstacles.ResetAllObstacles();
            projectiles.ResetAllProjectiles();
            particles.ResetAllParticles();
        }

        #endregion
    }
}