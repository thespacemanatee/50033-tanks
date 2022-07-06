using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public int m_NumRoundsToWin = 5;
        public float m_StartDelay = 3f;
        public float m_EndDelay = 3f;
        public CameraControl m_CameraControl;
        public Text m_MessageText;
        public GameObject[] m_TankPrefabs;
        public TankManager[] m_Tanks;
        public List<Transform> wayPointsForAI;
        private WaitForSeconds m_EndWait;
        private TankManager m_GameWinner;

        private int m_RoundNumber;
        private TankManager m_RoundWinner;
        private WaitForSeconds m_StartWait;


        private void Start()
        {
            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);

            SpawnAllTanks();
            SetCameraTargets();

            StartCoroutine(GameLoop());
        }


        private void SpawnAllTanks()
        {
            m_Tanks[0].m_Instance =
                Instantiate(m_TankPrefabs[0], m_Tanks[0].m_SpawnPoint.position, m_Tanks[0].m_SpawnPoint.rotation);
            m_Tanks[0].m_PlayerNumber = 1;
            m_Tanks[0].SetupPlayerTank();

            for (var i = 0; i < m_Tanks.Length - 1; i++)
            {
                var currAIIndex = i + 1;
                m_Tanks[currAIIndex].m_Instance =
                    // Alternate between spawning Chaser and Scanner
                    Instantiate(m_TankPrefabs[i % (m_TankPrefabs.Length - 1) + 1],
                        m_Tanks[currAIIndex].m_SpawnPoint.position,
                        m_Tanks[currAIIndex].m_SpawnPoint.rotation);
                m_Tanks[currAIIndex].m_PlayerNumber = currAIIndex + 1;
                m_Tanks[currAIIndex].SetupAI(wayPointsForAI);
            }
        }


        private void SetCameraTargets()
        {
            var targets = new Transform[m_Tanks.Length];

            for (var i = 0; i < targets.Length; i++)
                targets[i] = m_Tanks[i].m_Instance.transform;

            m_CameraControl.m_Targets = targets;
        }


        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());

            if (m_GameWinner != null) SceneManager.LoadScene(0);
            else StartCoroutine(GameLoop());
        }


        private IEnumerator RoundStarting()
        {
            ResetAllTanks();
            DisableTankControl();

            m_CameraControl.SetStartPositionAndSize();

            m_RoundNumber++;
            m_MessageText.text = $"ROUND {m_RoundNumber}";

            yield return m_StartWait;
        }


        private IEnumerator RoundPlaying()
        {
            EnableTankControl();

            m_MessageText.text = string.Empty;

            while (!IsPlayerDeadOrLastManStanding()) yield return null;
        }


        private IEnumerator RoundEnding()
        {
            DisableTankControl();

            m_RoundWinner = null;

            m_RoundWinner = GetRoundWinner();
            if (m_RoundWinner != null) m_RoundWinner.m_Wins++;

            m_GameWinner = GetGameWinner();

            var message = EndMessage();
            m_MessageText.text = message;

            yield return m_EndWait;
        }


        /// <summary>
        /// Check if player is dead or the last man standing.
        /// </summary>
        /// <returns>True if player is dead or last man standing, false otherwise.</returns>
        private bool IsPlayerDeadOrLastManStanding()
        {
            return !m_Tanks[0].m_Instance.activeSelf || m_Tanks.Count(tank => tank.m_Instance.activeSelf) == 1;
        }

        private TankManager GetRoundWinner()
        {
            return m_Tanks.FirstOrDefault(t => t.m_Instance.activeSelf);
        }

        private TankManager GetGameWinner()
        {
            return m_Tanks.FirstOrDefault(t => t.m_Wins == m_NumRoundsToWin);
        }


        private string EndMessage()
        {
            var sb = new StringBuilder();

            if (m_RoundWinner != null)
            {
                sb.AppendLine(m_RoundWinner.m_PlayerNumber == 1 ? "You won the round!" : "You lost this round!");
            }
            else
            {
                sb.Append("DRAW!");
            }

            sb.Append("\n\n\n\n");

            foreach (var t in m_Tanks)
                sb.AppendLine($"{t.m_ColoredPlayerText}: {t.m_Wins} WINS");

            if (m_GameWinner != null)
                sb.Append($"{m_GameWinner.m_ColoredPlayerText} WINS THE GAME!");

            return sb.ToString();
        }


        private void ResetAllTanks()
        {
            var playerWins = m_Tanks[0].m_Wins;
            // Progressively enable more tanks as player attains more kills
            for (var i = 0; i < m_Tanks.Length; i++) m_Tanks[i].Reset(i - playerWins < m_TankPrefabs.Length);
        }


        private void EnableTankControl()
        {
            foreach (var t in m_Tanks) t.EnableControl();
        }


        private void DisableTankControl()
        {
            foreach (var t in m_Tanks) t.DisableControl();
        }
    }
}