using UnityEngine;

namespace Assets.Scrpits.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DCSource", menuName = "ScriptableObjects/DCSource", order = 0)]
    public class DCSource : ScriptableObject 
    {
        [Min(0f)] public float resistance;
        [Min(0f)] public float power;
    }
}