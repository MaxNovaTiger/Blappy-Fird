using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text playerScoreText;
    public GameObject gameOverScreen;
    public GameObject leaderboardScreen;
    public GameObject promptScreen;
    public Text finalScoreText;
    public int finalScore;
    public AudioSource pointSFX;
    public Text highScoreText;
    public Text top10Text;
    public Text playerInitials;

    // Increase player score
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        playerScoreText.text = playerScore.ToString();
        pointSFX.Play();
    }

    // Increase player score by 1 via context menu
    [ContextMenu("Increase Score")]
    public void addOneToScore()
    {
        addScore(1);
    }

    // Restart game by reloading scene
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit Application
    public void quitGame()
    {
        Application.Quit();
    }

    // Open Game Over Screen and display final score
    public void gameOver()
    {
        playerScoreText.gameObject.SetActive(false);

        if (isHighScore(playerScore))
        {
            top10Text.text = "Your score of " + playerScore.ToString() + " is in the top ten!";
            promptScreen.SetActive(true);
        }
        else
        {
            gameOverScreen.SetActive(true);
        }
        
        finalScoreText.text = playerScore.ToString();
        finalScore = playerScore;
        Debug.Log("Game Over");
    }

    //Get list of scores
    public PlayerScore[] getScores()
    {
        string top10 = PlayerPrefs.GetString(PlayerScore.PLAYERPREFS_LIST_NAME, null);
        Debug.Log("getScores " + top10);
        PlayerScore[] scoreList = PlayerScore.deserializeList(top10);
        return scoreList;
    }

    // Determine if final score is a high score
    public bool isHighScore(int score)
    {
        Debug.Log("isHighScore called");
        PlayerScore[] scores = getScores();
        if (scores.Length < PlayerScore.MAX_STORED_SCORES)
        {
            return true;
        }
        for (int i = 0; i < scores.Length; i++)
        {
            if (score > scores[i].score)
            {
                return true;
            }
        }
        return false;
    }

    // Update Leaderboard values
    public void updateLeaderboard()
    {
        // Get and deserialize top 10 scores array
        PlayerScore[] scoreList = getScores();

        // Print scorers and scores in top 10 scores list
        for(int i = 0; i < scoreList.Length; i++)
        {
            Debug.Log("Scorer,Score: " + scoreList[i]);
        }

        PlayerScore current = new PlayerScore(playerInitials.text.ToString(), finalScore);
        Debug.Log("current: " + current.ToString());
        string raw = PlayerPrefs.GetString("highScore", "X,-1");
        Debug.Log("highScore: " + raw);
        PlayerScore old = PlayerScore.deserialize(raw);
        
        if (current.score > old.score)
        {
            Debug.Log("New high score saved: " + current.ToString());
            PlayerPrefs.SetString("highScore", current.ToString());
            old = current;
        }

        Debug.Log("highScore: " + PlayerPrefs.GetString("highScore", "X,-1"));

        if (old.score >= 0)
        {
            highScoreText.text = old.initials + "         " + old.score;
        }

        Debug.Log("Leaderboard Updated");
    }

    public void clearLeaderboard()
    {
        PlayerPrefs.DeleteKey("highScore");
        highScoreText.text = "No entries";
    }
}

/** Player experience
 * 1. Game ending event (done)
 * 2. Game over screen OR if in top 10 scorers, prompt player for initials
 * 3. Prompt screen says "Congratulations, you made it into the top 10 scorers!" or something to that effect, with a text box underneath for their initals. (done)
 * 4. Leaderboard screen
 */

/** Current Objectives
 * 2. Store multiple high scores
 * 4. Handle edge condition: more than 10 high scores
 * 5. Sort multiple high scores
 * 6. Display multiple high scores
 */