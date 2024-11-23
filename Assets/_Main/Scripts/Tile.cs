using System.Collections;
using UnityEngine;
using _Main.Scripts.Enums;
using _Main.Scripts.Managers;
using DG.Tweening;

namespace _Main.Scripts
{
    public class Tile : MonoBehaviour
    {
        public Vector2 Position;
        public TileType TileType;
        private SpriteRenderer _spriteRenderer;

        private Vector3 defaultScale;
        private Vector3 animatioScale;
        
        public void SetTile(TileType tileType , Vector2 position)
        {
            defaultScale = transform.localScale;
            animatioScale = transform.localScale * 1.2f;
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Position = position;
            TileType = tileType;
            _spriteRenderer.sprite = GameManager.TileController.GetTileSprite(TileType);
            this.name = $"{Position} : {TileType}";
        }

        public void SetPosition(Vector2 position)
        {
            this.Position = position;
            transform.DOMove(Position, 0.1f);
            this.name = $"{Position} : {TileType}";
        }

        public void Remove()
        {
            StartCoroutine(RemoveDelay());
        }

        IEnumerator RemoveDelay()
        {
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            Destroy(this.gameObject);
        }
        
        public void LevelUp()
        {
            switch (TileType)
            {
                case TileType.LevelOne: TileType = TileType.LevelTwo; break;
                case TileType.LevelTwo: TileType = TileType.LevelThree; break;
                case TileType.LevelThree: TileType = TileType.LevelFour; break;
                case TileType.LevelFour: TileType = TileType.LevelFive; break;
                case TileType.LevelFive: TileType = TileType.LevelSix; break;
                case TileType.LevelSix: TileType = TileType.LevelSeven; break;
                case TileType.LevelSeven: TileType = TileType.LevelEight; break;
                case TileType.LevelEight: TileType = TileType.LevelNine; break;
                case TileType.LevelNine: TileType = TileType.LevelTen; break;
                case TileType.LevelTen: TileType = TileType.LevelEleven; break;
                case TileType.LevelEleven:
                    break;
            }
            GameManager.Instance.ScoreController.IncrementScore((int)TileType * 2);
            LevelUpAnimation();
            this.name = $"{Position} : {TileType}";

            if (TileType == TileType.LevelEleven)
                GameManager.Instance.WonGame();
        }

        private void LevelUpAnimation()
        {
            transform.DOScale(animatioScale, 0.1f)
                .OnComplete(() =>
                {
                    _spriteRenderer.sprite = GameManager.TileController.GetTileSprite(TileType);
                    transform.DOScale(defaultScale, 0.1f);
                });            
        }
    }
}