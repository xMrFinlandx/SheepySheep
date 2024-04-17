using _Scripts.Utilities;
using _Scripts.Utilities.Classes;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Water Offset Config", menuName = "Visuals/Water Offset Config", order = 0)]
    public class WaterOffsetConfig : ScriptableObject
    {
        [SerializeField] private ShaderProperty<Vector2> _noiseOffset = new("_CurrentTexturePosition", Vector2.zero, new Vector2(-40, 0));

        public string PropertyName => _noiseOffset;
        public Vector2 RandomOffset => _noiseOffset.Values.GetRandomItem();
    }
}