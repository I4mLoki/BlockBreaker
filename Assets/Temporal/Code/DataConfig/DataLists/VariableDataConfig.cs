using Sirenix.OdinInspector;
using UnityEngine;
namespace Code.DataConfig.DataLists
{
    [CreateAssetMenu(fileName = "VariableData", menuName = "BlockBreaker/Data/Variables", order = 0)]
    public class VariableDataConfig : ScriptableObject
    {
        [BoxGroup("Behaviour Parameters"), ColorBox]
        public string[] targets;
        [BoxGroup("Behaviour Parameters"), ColorBox]
        public string[] fields;
    }
}