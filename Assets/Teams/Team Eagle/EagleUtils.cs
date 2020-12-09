using DoNotModify;
using BehaviorDesigner.Runtime;

namespace Eagle
{
    [System.Serializable]
    public class SharedGameData : SharedVariable<GameData>
    {
        public static implicit operator SharedGameData(GameData value) { return new SharedGameData { Value = value }; }
    }
}