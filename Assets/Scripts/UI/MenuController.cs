using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        public GameConstants gameConstants;
        public Text currentRound;
        public Button startButton;
        public Button restartButton;
        public UnityEvent onGameStart;

        private Image m_Background;
        private bool m_HasExistingGame;
        private bool m_GameStarted;

        // Start is called before the first frame update
        private void Start()
        {
            if (gameConstants.currentRound == 0) currentRound.enabled = false;
            m_Background = GetComponent<Image>();
            m_HasExistingGame = gameConstants.tankScores.Any(score => score > 0);
            if (m_HasExistingGame)
            {
                startButton.GetComponentInChildren<Text>().text = "Continue!";
            }
        }

        // Update is called once per frame
        private void Update()
        {
            currentRound.text = $"Current round: {(gameConstants.currentRound + 1).ToString()}";
            currentRound.enabled = gameConstants.currentRound != 0;

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
                onGameStart.Invoke();
                m_GameStarted = true;
                m_Background.enabled = false;
                foreach (Transform eachChild in transform)
                {
                    eachChild.gameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                }
            }
        }

        private void HandleResumeGame()
        {
            m_Background.enabled = false;
            foreach (Transform eachChild in transform)
            {
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }

        private void HandlePauseGame()
        {
            startButton.GetComponentInChildren<Text>().text = "Resume!";
            m_Background.enabled = true;
            foreach (Transform eachChild in transform)
            {
                eachChild.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}