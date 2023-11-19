using UnityEngine;

public class GoalNet : MonoBehaviour
{
    public GameManager gameManager;
    public bool isTeamAGoal; // Set this in the Inspector to true for Team A's goal, and false for Team B's goal

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Determine which team's goal it is based on the isTeamAGoal boolean
            string scoringTeam = isTeamAGoal ? "TeamA" : "TeamB";
            gameManager.GoalScored(scoringTeam);
        }
    }
}
