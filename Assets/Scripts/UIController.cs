using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    

    [SerializeField]
    private Image[] lifeImages;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private Button restartBtn;

    [SerializeField]
    private float tickRate = 0.2F;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    private void Start()
    {
        ToggleRestartButton(false);


        scoreLabel.text = "0";
        Player._instance.OnPlayerHit += UpdateUI;
        Player._instance.OnPlayerScoreChanged += actualizarScore;
        Player._instance.OnPlayerDied += muerto;
        
      //  Player._instance.OnPlayerScoreChanged += UpdateUI;
        

        if (Player._instance != null && lifeImages.Length == Player.playerLives)
        {
            InvokeRepeating("UpdateUI", 0F, tickRate);
        }
    }

    private void ToggleRestartButton(bool val)
    {
        if (restartBtn != null)
        {
            restartBtn.gameObject.SetActive(val);
        }
    }

    
    private void UpdateUI(int Lives)
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (lifeImages[i] != null && lifeImages[i].enabled)
            {
                lifeImages[i].gameObject.SetActive(Player._instance.Lives >= i + 1);
                print("Usando el que es");
            }
        }

        /*if (Player._instance.Lives <= 0)
        {
            CancelInvoke();

            if (scoreLabel != null)
            {
                scoreLabel.text = "Game Over";
            }

            ToggleRestartButton(true);
        }*/
    }

    private void muerto()
    {
        if (Player._instance.Lives <= 0)
        {
            CancelInvoke();

            if (scoreLabel != null)
            {
                scoreLabel.text = "Game Over";
            }

            ToggleRestartButton(true);
        }
    }
    private void actualizarScore(int Score)
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = Player._instance.Score.ToString();
        }
    }
}