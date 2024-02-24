using System.Collections.Generic;
using _Scripts.Player;
using _Scripts.Utilities.Interfaces;

namespace _Scripts.Utilities.Classes
{
    public class TileModifiersHandler
    {
        public bool CanRemoveData => _tileModifiers.Count == 0;
        public bool IsSingleAtTile => _tileModifiers.Exists(modifier => modifier.IsSingleAtTile);
        
        
        private List<ITileModifier> _tileModifiers = new();

        public TileModifiersHandler(ITileModifier modifier) => Add(modifier); 
        
        public void Add(ITileModifier modifier)
        {
            _tileModifiers.Add(modifier);
        }

        public void Remove(ITileModifier modifier)
        {
            _tileModifiers.Remove(modifier);
        }

        public void Activate(IPlayerController playerController)
        {
            foreach (var tileModifier in _tileModifiers)
            {
                tileModifier.Activate(playerController);
            }
        }
    }
}