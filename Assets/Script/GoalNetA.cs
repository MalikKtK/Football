using UnityEngine;

public class GoalNet : MonoBehaviour
{
    public GameManager gameManager;
    public bool isTeamAGoal;

        // Metode kaldt når en collider (fx bolden) går ind i målnettet.
private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            string scoringTeam = isTeamAGoal ? "TeamA" : "TeamB";
            gameManager.GoalScored(scoringTeam);
        }
    }
}
