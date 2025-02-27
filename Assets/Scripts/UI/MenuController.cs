using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        // UI elements
        public GameConstants gameConstants;
        public Text currentRoundText;
        public Text currentScoreText;
        public Button startButton;
        public Button restartButton;

        // Events
        public UnityEvent onGameStart;
        public UnityEvent onGameRestart;

        private Image m_Background;
        private bool m_GameStarted;

        // Start is called before the first frame update
        private void Start()
        {
            m_Background = GetComponent<Image>();
            // Setup main menu UI based on saved game constants
            if (gameConstants.currentRound == 0)
            {
                restartButton.gameObject.SetActive(false);
            }
            else
            {
                startButton.GetComponentInChildren<Text>().text = "Continue!";
            }
        }

        // Update is called once per frame
        private void Update()
        {
            // Poll for changes in game constants and update menu UI accordingly
            currentRoundText.text = $"Current round: {(gameConstants.currentRound + 1).ToString()}";
            currentScoreText.text = $"Current score: {(gameConstants.tankScores[0]).ToString()}";
            currentRoundText.enabled = m_GameStarted || gameConstants.currentRound > 0;
            currentScoreText.enabled = m_GameStarted || gameConstants.currentRound > 0;

            if (Input.GetKeyDown(KeyCode.P))
            {
                HandlePauseGame();
            }
        }

        public void HandleStartGame()
        {
            if (m_GameStarted)
            {
                HandleResumeGame();
            }
            else
            {
                m_GameStarted = true;
                onGameStart.Invoke();
                HideMenu();
            }
        }

        public void HandleRestartGame()
        {
            onGameRestart.Invoke();
            HideMenu();
        }

        private void HandleResumeGame()
        {
            HideMenu();
        }

        private void HandlePauseGame()
        {
            startButton.GetComponentInChildren<Text>().text = "Resume!";
            ShowMenu();
        }

        private void ShowMenu()
        {
            m_Background.enabled = true;
            foreach (Transform eachChild in transform)
            {
                eachChild.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        private void HideMenu()
        {
            m_Background.enabled = false;
            foreach (Transform eachChild in transform)
            {
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}