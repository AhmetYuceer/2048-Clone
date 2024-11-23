using UnityEngine;

namespace _Main.Scripts.SaveSystem
{
    public static class SaveAndLoad
    {
        public static void SetBestScore(int score)
        {
            PlayerPrefs.SetInt("BestScore", score);            
        } 
        
        public static int GetBestScore()
        {
            return PlayerPrefs.GetInt("BestScore");            
        } 
    }
}