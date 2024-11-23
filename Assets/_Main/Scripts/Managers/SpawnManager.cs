using _Main.Scripts.Enums;
using _Main.Scripts.Grid;
using UnityEngine;

namespace _Main.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;
        [SerializeField] private GameObject _tilePrefab;

        private void Awake()
        {
            if (Instance==null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void InstantiateTile(int tileCount)
        {
            for (int i = 0; i < tileCount; i++)
            {
                GridData gridData = GridManager.Instance.GetRandomEmptyGridData();
                if (gridData == null)
                    break;
                
                if (Instantiate(_tilePrefab, gridData.Position, Quaternion.identity).TryGetComponent(out Tile tile))
                {
                    gridData.gameObject.name = gridData.Position.ToString();
                    gridData.SetTile(tile);
                    gridData.Tile.SetTile(GetRandomTileType(), gridData.Position);
                }
            }
        }
        
        private TileType GetRandomTileType()
        {
            var rnd = Random.value;
            if (rnd >= 0f && rnd <= 0.1f)
                return TileType.LevelTwo;
            else
                return TileType.LevelOne;
        }
    }   
}