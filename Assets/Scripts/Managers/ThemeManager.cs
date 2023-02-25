// Written by Peter Thompson - Playify.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    public class ThemeManager : MonoBehaviour
    {
		public Text ui;

		[Serializable]
		public class Text
		{
			public Texts texts;
			public Fonts fonts;
			public Sizes size;

			[Serializable]
			public class Texts
			{
				[Tooltip("eg. Tap to play.")]
				public string clickToStartText;
			}

			[Serializable]
			public class Fonts
			{
				[Tooltip("Font for any titles.")]
				public Font titleFont;
				[Tooltip("Font for texts that do not need to be easy to read. Eg. In-game tooltips.")]
				public Font readableFont;
				[Tooltip("Font for texts that you would like to be stylised. Eg. Game start countdowns.")]
				public Font stylisedFont;
			}

			[Serializable]
			public class Sizes
			{
				// Arrays used for different platforms. Different platforms may need to use different font sizes.
				// Otherwise autosizing may just be used.
				public int[] titleTextSize;
				public int[] clickToStartTextSize;
				public int[] gameStartCountdownSize;
				public int[] youDiedTextSize;
				public int[] toolTipTextSize;
			}
		}
	}
}
