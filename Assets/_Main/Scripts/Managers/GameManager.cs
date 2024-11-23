using System.Collections;
using _Main.Scripts.Controller;
using _Main.Scripts.Grid;
using _Main.Scripts.UI;
using UnityEngine;
using Direction = _Main.Scripts.Enums.Direction;

namespace _Main.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static TileController TileController;
        public ScoreController ScoreController;

        private bool _isPlay;
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            TileController = new TileController();
            GridManager.Instance.CreateGrid();
            ScoreController = new ScoreController();
            ScoreController.SetBestScore();
            SpawnManager.Instance.InstantiateTile(2);
            _isPlay = true;
        }

        public void WonGame()
        {
            _isPlay = false;
            UIManager.Instance.OpenWonPanel();
        }
        
        private void GameOver()
        {
            bool endGame = CanMakeMove();
            if (!endGame)
            {
                _isPlay = false;
                ScoreController.UpdateBestScore();
                UIManager.Instance.OpenTryAgainPanel();
            }
        }
        
        public void CheckTiles(Direction direction)
        {
            if (!_isPlay) return;

            var hasMoved = false;
            
            switch (direction)
            {
                case Direction.Up:
                    hasMoved = CheckUpTiles();
                    break;
                case Direction.Down:
                    hasMoved = CheckDownTiles();
                    break;
                case Direction.Right:
                    hasMoved = CheckRightTiles();
                    break;
                case Direction.Left:
                    hasMoved = CheckLeftTiles();
                    break;
            }

            if (hasMoved)
                StartCoroutine(SpawnDelay());
        }
        
        IEnumerator SpawnDelay()
        {
            yield return new WaitForSeconds(0.2f);
            SpawnManager.Instance.InstantiateTile(1);
            GameOver();
        }

        private bool CanMakeMove()
        {
            var gridSize = GridManager.Instance.GridSize;

            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    GridData currentCell = GridManager.Instance.GetGridDataByPosition(new Vector2(x, y));
                    if (currentCell.Tile == null) continue;
              
                    if (CanMoveOrMerge(currentCell, x, y - 1) || // Up
                        CanMoveOrMerge(currentCell, x, y + 1) || // Down
                        CanMoveOrMerge(currentCell, x - 1, y) || // Left
                        CanMoveOrMerge(currentCell, x + 1, y))   // Right
                    {
                        return true; 
                    }
                }
            }
            return false;
        }
        private bool CanMoveOrMerge(GridData currentCell, int targetX, int targetY)
        {
            GridData targetCell = GridManager.Instance.GetGridDataByPosition(new Vector2(targetX, targetY));
            if (targetCell == null) return false;
            if (!targetCell.IsFull) return true;
            if (targetCell.Tile.TileType == currentCell.Tile.TileType) return true;
            return false;
        }
        private bool CheckUpTiles()
        {
            var gridSize = GridManager.Instance.GridSize;
            bool hasMoved = false; 

            for (int x = 0; x < gridSize.x; x++)
            {
                bool[] lockedCells = new bool[gridSize.y];
                
                for (int y = gridSize.y -1 ; y >= 0; y--)
                {
                    GridData currentCell = GridManager.Instance.GetGridDataByPosition(new Vector2(x, y));
                    if (currentCell.Tile == null) continue;
                    
                    for (int targetY = y + 1; targetY < gridSize.y; targetY++)
                    {
                        GridData targetCell = GridManager.Instance.GetGridDataByPosition(new Vector2(x, targetY));

                        if (targetCell == null) break;

                        if (targetCell.IsFull)
                        {
                            if (targetCell.Tile.TileType == currentCell.Tile.TileType && !lockedCells[targetY])
                            {
                                targetCell.Tile.LevelUp();
                                lockedCells[targetY] = true;
                                currentCell.Tile.Remove();
                                currentCell.ClearTile();
                                hasMoved = true; 
                            }
                            else
                            {
                                GridData previousCell = GridManager.Instance.GetGridDataByPosition(new Vector2(x, targetY - 1));
                                if (previousCell != null && previousCell != currentCell)
                                {
                                    previousCell.SetTile(currentCell.Tile);
                                    currentCell.ClearTile();
                                    hasMoved = true; 
                                }
                            }
                            break;
                        }
                        else
                        {
                            targetCell.SetTile(currentCell.Tile);
                            currentCell.ClearTile();
                            currentCell = targetCell;
                            hasMoved = true; 
                        }
                    }
                }
            }
            return hasMoved;
        }
        private bool CheckDownTiles()
        {
            var gridSize = GridManager.Instance.GridSize;
            bool hasMoved = false; 
            
            for (int x = 0; x < gridSize.x; x++)
            {
                bool[] lockedCells = new bool[gridSize.y];
                
                for (int y = 1; y < gridSize.y; y++)
                {
                    GridData currentCell = GridManager.Instance.GetGridDataByPosition(new Vector2(x, y));
                    if (currentCell.Tile == null) continue;
                    
                    for (int targetY = y - 1; targetY >= 0; targetY--)
                    {
                        GridData targetCell = GridManager.Instance.GetGridDataByPosition(new Vector2(x, targetY));

                        if (targetCell == null) break;

                        if (targetCell.IsFull)
                        {
                            if (targetCell.Tile.TileType == currentCell.Tile.TileType && !lockedCells[targetY])
                            {
                                targetCell.Tile.LevelUp();
                                lockedCells[targetY] = true;
                                currentCell.Tile.Remove();
                                currentCell.ClearTile();
                                hasMoved = true;
                            }
                            else
                            {
                                GridData previousCell = GridManager.Instance.GetGridDataByPosition(new Vector2(x, targetY + 1));
                                if (previousCell != null && previousCell != currentCell)
                                {
                                    previousCell.SetTile(currentCell.Tile);
                                    currentCell.ClearTile();
                                    hasMoved = true;
                                }
                            }                            
                            break;
                        }
                        else
                        {
                            targetCell.SetTile(currentCell.Tile);
                            currentCell.ClearTile();
                            currentCell = targetCell;
                            hasMoved = true;
                        }
                    }
                }
            }

            return hasMoved;
        }
        private bool CheckRightTiles()
        {
            var gridSize = GridManager.Instance.GridSize;
            bool hasMoved = false; 
            for (int y = 0; y < gridSize.y; y++)
            {
                bool[] lockedCells = new bool[gridSize.x];
                for (int x = gridSize.x - 2; x >= 0; x--)
                {
                    GridData currentCell = GridManager.Instance.GetGridDataByPosition(new Vector2(x, y));
                    if (currentCell.Tile == null) continue;  

                    for (int targetX = x + 1; targetX < gridSize.x; targetX++)
                    {
                        GridData targetCell = GridManager.Instance.GetGridDataByPosition(new Vector2(targetX, y));

                        if (targetCell == null) break; 

                        if (targetCell.IsFull)
                        {
                            if (targetCell.Tile.TileType == currentCell.Tile.TileType && !lockedCells[targetX])
                            {
                                targetCell.Tile.LevelUp();
                                lockedCells[targetX] = true; 
                                currentCell.Tile.Remove();
                                currentCell.ClearTile();
                                hasMoved = true;
                            }
                            else
                            {
                                GridData previousCell = GridManager.Instance.GetGridDataByPosition(new Vector2(targetX - 1, y));
                                if (previousCell != null && previousCell != currentCell)
                                {
                                    previousCell.SetTile(currentCell.Tile);
                                    currentCell.ClearTile();
                                    hasMoved = true;
                                }
                            }
                            break;
                        }
                        else
                        {
                            targetCell.SetTile(currentCell.Tile);
                            currentCell.ClearTile();
                            currentCell = targetCell;
                            hasMoved = true;
                        }
                    }
                }
            }
            return hasMoved;
        }
        private bool CheckLeftTiles()
        {
            var gridSize = GridManager.Instance.GridSize;
            bool hasMoved = false; 
            for (int y = 0; y < gridSize.y; y++)
            {
                bool[] lockedCells = new bool[gridSize.x];

                for (int x = 1; x < gridSize.x; x++)
                {
                    GridData currentCell = GridManager.Instance.GetGridDataByPosition(new Vector2(x, y));
                    if (currentCell.Tile == null) continue; 

                    for (int targetX = x - 1; targetX >= 0; targetX--)
                    {
                        GridData targetCell = GridManager.Instance.GetGridDataByPosition(new Vector2(targetX, y));

                        if (targetCell == null) break;  

                        if (targetCell.IsFull)
                        {
                            if (targetCell.Tile.TileType == currentCell.Tile.TileType && !lockedCells[targetX])
                            {
                                targetCell.Tile.LevelUp();
                                lockedCells[targetX] = true; 
                                currentCell.Tile.Remove();
                                currentCell.ClearTile();
                                hasMoved = true;
                            }
                            else
                            {
                                GridData previousCell = GridManager.Instance.GetGridDataByPosition(new Vector2(targetX + 1, y));
                                if (previousCell != null && previousCell != currentCell)
                                {
                                    previousCell.SetTile(currentCell.Tile);
                                    currentCell.ClearTile();
                                    hasMoved = true;
                                }
                            }
                            break; 
                        }
                        else
                        {
                            targetCell.SetTile(currentCell.Tile);
                            currentCell.ClearTile();
                            currentCell = targetCell;  
                            hasMoved = true;
                        }
                    }
                }
            }
            return hasMoved;
        }
    }
}