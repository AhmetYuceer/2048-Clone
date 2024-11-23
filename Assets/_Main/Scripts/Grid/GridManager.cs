using UnityEngine;
using System.Collections.Generic;
using _Main.Scripts.Controller;

namespace _Main.Scripts.Grid
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance;
        
        [Header("Grid Settings")]
        public Vector2Int GridSize;
        [SerializeField] private Transform _gridParentTransform;
        [SerializeField] private GameObject _tilePrefab;

        [Header("Grid")]
        public List<GridData> Grid = new List<GridData>();
        public List<GridData> EmptyGrid = new List<GridData>();
        
        private void Awake()
        {
            if (Instance==null)
                Instance = this;                
            else
                Destroy(gameObject);
        }

        public void CreateGrid()
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    Vector2 gridDataPos = new Vector2(x , y);
                    if (Instantiate(_tilePrefab, gridDataPos, Quaternion.identity, _gridParentTransform).TryGetComponent(out GridData gridData))
                    {
                        gridData.name = gridDataPos.ToString();
                        gridData.SetGrid(gridDataPos);
                        Grid.Add(gridData);
                    }
                }
                EmptyGrid = new List<GridData>(Grid);
            }
            CameraController.Instance.CenterCameraOnGrid(GridSize.x, GridSize.y);
        }

        public GridData GetRandomEmptyGridData()
        {
            SetEmptyGridData();

            if (EmptyGrid.Count <= 0)
                return null;
            
            var rnd = Random.Range(0, EmptyGrid.Count - 1);

            return EmptyGrid[rnd];
        }

        public void SetEmptyGridData()
        {
            EmptyGrid.Clear();
            foreach (var grid in Grid)
            {
                if (!grid.IsFull)
                    EmptyGrid.Add(grid);
            }
        }
        
        public GridData GetGridDataByPosition(Vector2 position)
        {
           return Grid.Find(x => x.Position == position);
        }
    }
}
