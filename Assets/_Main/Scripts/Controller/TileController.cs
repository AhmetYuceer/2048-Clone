using System;
using System.Collections.Generic;
using _Main.Scripts.Enums;
using UnityEngine;

namespace _Main.Scripts.Controller
{
    public class TileController
    {
        private readonly Dictionary<TileType, Sprite> _tileSprites = new Dictionary<TileType, Sprite>();

        public TileController()
        {
            foreach (TileType item in Enum.GetValues(typeof(TileType)))
            {
                var itemInt = (int)item;
                var sprite = Resources.Load<Sprite>(itemInt.ToString());

                if (sprite != null)
                {
                    _tileSprites.Add(item, sprite);
                }
                else
                {
                    Debug.LogError($"Item sprite could not be loaded. Enum : {item}");
                    break;
                }                
            }
        }
        
        public Sprite GetTileSprite(TileType tileType)
        {
            if (_tileSprites[tileType] == null)
                Debug.LogError($"Item sprite could not be loaded. Enum : {tileType}");
            return _tileSprites[tileType];
        }
    }
}
