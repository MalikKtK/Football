using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scoreTeamA = 0;
    public int scoreTeamB = 0;

    private Vector3 ballStartPosition;
    private Quaternion ballStartRotation;
    private GameObject[] players;
    private GameObject[] enemies;
    private GameObject[] aiPlayers;

       public AudioClip goalScoredSound;
    private AudioSource audioSource;


    void Start()
    {

          audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
        // Hent eller tilføj en AudioSource til GameManager, hvis den ikke allerede er tilføjet.
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballStartPosition = ball.transform.position;
            ballStartRotation = ball.transform.rotation;
        }

        players = GameObject.FindGameObjectsWithTag("Player");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        aiPlayers = GameObject.FindGameObjectsWithTag("AIPlayer");
    }

     // Metode kaldt når et mål scores. Den modtager en parameter, der angiver hvilket hold der scorede.
 public void GoalScored(string scoringTeam)
    {
        if (scoringTeam == "TeamA")
        {
            scoreTeamA++;
            Debug.Log("Score Team A: " + scoreTeamA);
        }
        else if (scoringTeam == "TeamB")
        {
            scoreTeamB++;
            Debug.Log("Score Team B: " + scoreTeamB);
        }

        // Play the sound effect
        audioSource.PlayOneShot(goalScoredSound);

        ResetPositions();
    }

    // Metode til at nulstille positionerne for bolden og spillerne/fjenderne.
private void ResetPositions()
{
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
    foreach (var aiPlayer in aiPlayers)
    {
        var aiPlayerScript = aiPlayer.GetComponent<AIPlayer>();
        if (aiPlayerScript != null)
        {
            aiPlayerScript.ResetPosition();
        }
        else
        {
            Debug.LogWarning($"AIPlayer script not found on {aiPlayer.name}");
        }
    }
}

void Update()
{
    if (Input.GetKeyDown(KeyCode.R))  // Hvis 'R'-tasten trykkes, nulstil spillerpositionerne til testformål.

    {
        ResetPositions();
    }
}
}