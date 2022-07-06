using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public int currentRound;
    // Hardcoded to 7 tank scores
    public int[] tankScores = { 0, 0, 0, 0, 0, 0, 0 };
    
    public void ResetGameState()
    {
        currentRound = 0;
        for (var i = 0; i < tankScores.Length; i++)
        {
            tankScores[i] = 0;
        }   
    }
}