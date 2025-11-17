using _Scripts.Utilities.Interfaces;
using Ami.BroAudio;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Passage : MonoBehaviour, ITileModifier
    {
        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;
        
        public void Activate(IPlayerController playerController)
        {
            
        }
        
        public float YOffset => 0;
        public bool IsSingleAtTile => true;
        public SoundID FootstepsSound => default;
        public Transform GetTransform() => transform;

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + new Vector3(0, YOffset), .1f);
        }
    }
}