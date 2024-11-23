using UnityEngine;

namespace _Main.Scripts.Grid
{
    public class GridData : MonoBehaviour
    {
        public bool IsFull;
        public Vector2 Position;
        public Tile Tile;

        public void SetGrid(Vector2 position)
        {
            Position = position;
        }

        public void SetTile(Tile tile)
        {
            IsFull = true;
            Tile = tile;
            Tile.SetPosition(Position);
        }

        public void ClearTile()
        {
            IsFull = false;
            Tile = null;
        }
    }
}