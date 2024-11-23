using _Main.Scripts.Enums;
using UnityEngine;

namespace _Main.Scripts.Managers
{
    public class InputManager :MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) ||Input.GetKeyDown(KeyCode.UpArrow))
                GameManager.Instance.CheckTiles(Direction.Up);
            
            if (Input.GetKeyDown(KeyCode.S) ||Input.GetKeyDown(KeyCode.DownArrow))
                GameManager.Instance.CheckTiles(Direction.Down);

            if (Input.GetKeyDown(KeyCode.A) ||Input.GetKeyDown(KeyCode.LeftArrow))
                GameManager.Instance.CheckTiles(Direction.Left);

            if (Input.GetKeyDown(KeyCode.D) ||Input.GetKeyDown(KeyCode.RightArrow))
                GameManager.Instance.CheckTiles(Direction.Right);
        }
    }
}