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
        // Reset any other necessary components, like Rigidbody velocity
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        Debug.Log("Ball reset to position: " + ball.transform.position);
    }

    // Reset players
    foreach (var player in players)
    {
        Debug.Log("Resetting player: " + player.name + " from position: " + player.transform.position + " to startPosition: " + player.GetComponent<Player>().startPosition);
        player.transform.position = player.GetComponent<Player>().startPosition;
        player.transform.rotation = player.GetComponent<Player>().startRotation;
        // Reset any other necessary components, like Rigidbody velocity
        // Add Rigidbody reset if players have Rigidbody and use physics
    }

    // Reset enemies
    foreach (var enemy in enemies)
    {
        Debug.Log("Resetting enemy: " + enemy.name + " from position: " + enemy.transform.position + " to startPosition: " + enemy.GetComponent<Enemy>().startPosition);
        enemy.transform.position = enemy.GetComponent<Enemy>().startPosition;
        enemy.transform.rotation = enemy.GetComponent<Enemy>().startRotation;
        // Optionally reset any other components, like Rigidbody
        Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
        if (enemyRb != null)
        {
            enemyRb.velocity = Vector3.zero;
            enemyRb.angularVelocity = Vector3.zero;
        }
    }
}

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Score Team A: " + scoreTeamA);
        Debug.Log("Score Team B: " + scoreTeamB);
    }
}
