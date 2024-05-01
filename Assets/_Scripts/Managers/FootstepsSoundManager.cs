using System.Collections.Generic;
using _Scripts.Gameplay.Tilemaps;
using _Scripts.Player;
using _Scripts.Utilities;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

namespace _Scripts.Managers
{
    public class FootstepsSoundManager : Singleton<FootstepsSoundManager>
    {
        [SerializeField] private TileSoundData[] _soundData;
        [SerializeField] private float _playSoundDelay = .37f;

        private readonly Dictionary<TileBase, TileSoundData> _dataFromTiles = new();

        private AudioResource _audioResource;
        private SoundID _soundID;
        
        private float _delayCounter;
        private bool _canPlay;

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

        public void Stop()
        {
            _canPlay = false;
            _delayCounter = _playSoundDelay;
        }

        private void ChangeFootstepSound(Vector2Int position)
        {
            var tile = TilemapManager.Instance.GetTileAtCellPosition((Vector3Int) position);
            
            _soundID = _dataFromTiles[tile].SoundID;
            _canPlay = true;
        }

        private void OnDestroy()
        {
            PlayerController.PlayerInNewTileAction -= ChangeFootstepSound;
        }

        private void FixedUpdate()
        {
            if (!_canPlay)
                return;

            _delayCounter -= Time.fixedDeltaTime;

            if (_delayCounter > 0)
                return;
            
            BroAudio.Play(_soundID);
            
            _delayCounter = _playSoundDelay;
        }
    }
}