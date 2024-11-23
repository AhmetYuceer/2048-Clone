using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace _Main.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _bestScoreText;
        [SerializeField] private Button _newGameButton;
        
        [Header("Score Animation")]
        Stack<Animation> _animationStack = new Stack<Animation>();
        [SerializeField] private Animation _scoreAnimation; 
        [SerializeField] private GameObject _scoreAnimationPanel;
        [SerializeField] private TextMeshProUGUI _scoreAnimationAmountText;
        
        [Header("Try Again Panel")]
        [SerializeField] private GameObject _tryAgainPanel;
        [SerializeField] private Button _tryAgainButton;

        [Header("Won panel")]
        [SerializeField] private GameObject _wonPanel;
        [SerializeField] private Button _wonNewGameButton;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            _tryAgainPanel.SetActive(false);
            _wonPanel.SetActive(false);
            _newGameButton.onClick.AddListener(OnNewGameButtonPressed);
            _tryAgainButton.onClick.AddListener(OnNewGameButtonPressed);
            _wonNewGameButton.onClick.AddListener(OnNewGameButtonPressed);
        }

        public void IncreaseScoreAnimation(int amount)
        {
            _scoreAnimationAmountText.text = $"+ {amount}";
            if (_scoreAnimation.isPlaying)
                _scoreAnimation.Stop();
            _scoreAnimation.Play();
        }
        
        private void OnNewGameButtonPressed()
        {
            SceneManager.LoadScene(0);
        }
        
        public void UpdateScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void SetBestScoreText(int bestScore)
        {
            _bestScoreText.text = bestScore.ToString();            
        }

        public void OpenTryAgainPanel()
        {
            _tryAgainPanel.SetActive(true);
        }
        
        public void OpenWonPanel()
        {
            _wonPanel.SetActive(true);
        }
    }
}