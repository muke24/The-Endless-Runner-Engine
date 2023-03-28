using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game Level Data")]
    public class LevelData : MonoBehaviour
    {
        internal enum GameMode { Endless, Race }
        [SerializeField, Tooltip("What game mode is this level using?")]
        internal GameMode gameMode = GameMode.Endless;
    }
}
