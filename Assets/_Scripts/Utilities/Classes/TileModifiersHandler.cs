using System.Collections.Generic;
using _Scripts.Utilities.Interfaces;

namespace _Scripts.Utilities.Classes
{
    public class TileModifiersHandler
    {
        public bool CanRemoveData => _tileModifiers.Count == 0;
        public bool IsSingleAtTile => _tileModifiers.Exists(modifier => modifier.IsSingleAtTile);
        
        private readonly List<ITileModifier> _tileModifiers = new();

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

        public bool TryInteract()
        {
            var isInteractionPerfomed = false;

            foreach (var tileModifier in _tileModifiers)
            {
                if (tileModifier is not IMouseInteraction mouseInteraction)
                    continue;

                mouseInteraction.Interact();
                isInteractionPerfomed = true;
            }

            return isInteractionPerfomed;
        }

        public bool TryRemove()
        {
            var isAnyRemoved = false;
            var toRemove = new List<ITileModifier>();
            
            foreach (var tileModifier in _tileModifiers)
            {
                if (tileModifier is not IMouseInteraction mouseInteraction) 
                    continue;

                isAnyRemoved = true;
                toRemove.Add(mouseInteraction);
                mouseInteraction.Remove();
            }

            foreach (var modifier in toRemove)
            {
                _tileModifiers.Remove(modifier);
            }
            
            return isAnyRemoved;
        }
    }
}