using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scoreTeamA = 0;
    public int scoreTeamB = 0;

    private Vector3 ballStartPosition;
    private Quaternion ballStartRotation;
    private GameObject[] players;
    private GameObject[] enemies;

    void Start()
    {
        // Store the ball's initial position and rotation
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballStartPosition = ball.transform.position;
            ballStartRotation = ball.transform.rotation;
        }

        // Store the initial positions and rotations of players
        players = GameObject.FindGameObjectsWithTag("Player");
        // Assume 'Player' tag is used for all player objects

        // Store the initial positions and rotations of enemies
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // Assume 'Enemy' tag is used for all enemy objects
    }

      public void GoalScored(string scoringTeam)
    {
        if (scoringTeam == "TeamA")
        {
            scoreTeamA++;
            Debug.Log("Score Team A: " + scoreTeamA); // Log only when the score changes
        }
        else if (scoringTeam == "TeamB")
        {
            scoreTeamB++;
            Debug.Log("Score Team B: " + scoreTeamB); // Log only when the score changes
        }

        // Reset positions after a goal is scored
        ResetPositions();
    }

private void ResetPositions()
{
    // Reset the ball
    GameObject ball = GameObject.FindGameObjectWithTag("Ball");
    if (ball != null)
    {
        ball.transform.position = ballStartPosition;
        ball.transform.rotation = ballStartRotation;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        Debug.Log("Ball reset to position: " + ball.transform.position);
    }

    // Reset players
    Debug.Log("Resetting players to their starting positions.");
    foreach (var player in players)
    {
        var playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.ResetPosition();
            Debug.Log($"Player {player.name} reset to startPosition: {playerScript.startPosition}");
        }
        else
        {
            Debug.Log($"Player script not found on {player.name}");
        }
    }

    // Reset enemies
    Debug.Log("Resetting enemies to their starting positions.");
    foreach (var enemy in enemies)
    {
        var enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.ResetPosition();
            Debug.Log($"Enemy {enemy.name} reset to startPosition: {enemyScript.startPosition}");
        }
        else
        {
            Debug.Log($"Enemy script not found on {enemy.name}");
        }
    }
}


    // Update is called once per frame
void Update()
{
    if (Input.GetKeyDown(KeyCode.R)) // Press 'R' to reset the player's position for testing
    {
        ResetPositions();
    }

    // Rest of your update logic...
}
}