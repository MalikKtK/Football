using UnityEngine;

public class GoalNet : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            string scoringTeam = (gameObject.CompareTag("GoalTeamA")) ? "TeamA" : "TeamB";
            gameManager.GoalScored(scoringTeam);
        }
    }
}