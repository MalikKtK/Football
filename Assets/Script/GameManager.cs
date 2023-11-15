using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scoreTeamA = 0;
    public int scoreTeamB = 0;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Score Team A: " + scoreTeamA);
        Debug.Log("Score Team B: " + scoreTeamB);
    }

    public void GoalScored(string scoringTeam)
    {
        if (scoringTeam == "TeamA")
        {
            scoreTeamA++;
        }
        else if (scoringTeam == "TeamB")
        {
            scoreTeamB++;
        }

        ResetBall();
    }

    private void ResetBall()
    {
        // Reset the ball position to the middle or a predefined respawn point
        // Example: assuming the ball has a Rigidbody component, reset its position
        GameObject ball = GameObject.FindWithTag("Ball");
        if (ball != null)
        {
            ball.transform.position = Vector3.zero; // Adjust as needed
        }
    }
}