using _Main.Scripts.SaveSystem;
using _Main.Scripts.UI;

namespace _Main.Scripts.Controller
{
    public class ScoreController
    {
        private int score;
        public void IncrementScore(int amount)
        {
            score += amount;
            UIManager.Instance.IncreaseScoreAnimation(amount);
            UIManager.Instance.UpdateScoreText(score);
        }
        
        public void SetBestScore()
        {
            int bestScore = SaveAndLoad.GetBestScore();
            UIManager.Instance.SetBestScoreText(bestScore);
        }

        public void UpdateBestScore()
        {
            int bestScore = SaveAndLoad.GetBestScore();
            
            if (score > bestScore)
            {
                SaveAndLoad.SetBestScore(score);
                SetBestScore();
            }
        }
    }
}