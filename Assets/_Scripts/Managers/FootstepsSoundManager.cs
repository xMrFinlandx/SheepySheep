using System.Collections.Generic;
using _Scripts.Gameplay.Tilemaps;
using _Scripts.Player;
using _Scripts.Utilities;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
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

        private bool _isPaused;
        private bool _canPlay;
        private bool _isTileModifierContainsSound; 

        private void Start()
        {
            PlayerController.PlayerInNewTileAction += ChangeFootstepSound;
            GameStateManager.GameStateChangedAction += OnGameStateChanged;

            foreach (var soundData in _soundData)
            {
                foreach (var tile in soundData.Tiles)
                {
                    _dataFromTiles.Add(tile, soundData);
                }
            }
        }

        private void OnGameStateChanged(GameStateType state)
        {
            if (_soundID == 0)
                return;
            
            _isPaused = state == GameStateType.Paused;
            
            if (_isPaused)
            {
                BroAudio.Pause(_soundID);
            }
            else
            {
                BroAudio.Play(_soundID);
            }
        }

        public void Stop()
        {
            _canPlay = false;
            _delayCounter = _playSoundDelay;
        }

        private void ChangeFootstepSound(Vector2Int position)
        {
            if (TilemapManager.Instance.TryGetTileModifier(position, out var value) && value.FootstepsSound.ID != 0)
            {
                _soundID = value.FootstepsSound;
            }
            else
            {
                var tile = TilemapManager.Instance.GetTileAtCellPosition((Vector3Int) position);
                _soundID = _dataFromTiles[tile].SoundID;
            }
            
            _canPlay = true;
        }

        private void OnDestroy()
        {
            PlayerController.PlayerInNewTileAction -= ChangeFootstepSound;
            GameStateManager.GameStateChangedAction -= OnGameStateChanged;
        }

        private void FixedUpdate()
        {
            if (!_canPlay || _isPaused)
                return;

            _delayCounter -= Time.fixedDeltaTime;

            if (_delayCounter > 0)
                return;
            
            BroAudio.Play(_soundID);
            
            _delayCounter = _playSoundDelay;
        }
    }
}