// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    [CreateAssetMenu(fileName = "NewTheme", menuName = "Create Endless Runner Theme")]
    public class Theme : ScriptableObject
    {
        public enum ThemeType { ThreeDimentional, TwoDimentional }
        public ThemeType themeType = ThemeType.ThreeDimentional;


    }
}
