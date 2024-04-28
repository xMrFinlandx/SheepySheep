using System.Collections.Generic;
using _Scripts.Gameplay.Tilemaps;
using _Scripts.Player;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Managers
{
    public class SoundsManager : Singleton<SoundsManager>
    {
        [SerializeField] private TileSoundData[] _soundData;
        [SerializeField] private AudioSource _audioSource;

        private Dictionary<TileBase, TileSoundData> _dataFromTiles = new();
        
        private void Start()
        {
            PlayerController.PlayerInNewTileAction += ChangeFootstepSound;

            foreach (var soundData in _soundData)
            {
                foreach (var tile in soundData.Tiles)
                {
                    _dataFromTiles.Add(tile, soundData);
                }
            }
        }

        private void ChangeFootstepSound(Vector2Int position)
        {
            var tile = TilemapManager.Instance.GetTileAtCellPosition((Vector3Int) position);

            _audioSource.clip = _dataFromTiles[tile].GetRandomClip();
            _audioSource.Play();
        }

        private void OnDestroy()
        {
            PlayerController.PlayerInNewTileAction -= ChangeFootstepSound;
        }
    }
}