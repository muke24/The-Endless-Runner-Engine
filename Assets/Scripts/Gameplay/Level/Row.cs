// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    public class Row : MonoBehaviour
    {
		#region Old
		//      //internal float rowVerticalSize = 5f;

		//internal CustomSpawner[] customSpawners;

		//private void Start()
		//{
		//	FindCustomSpawners();
		//	RegenerateCustomSpawners();
		//}

		//void FindCustomSpawners()
		//{
		//	customSpawners = GetComponentsInChildren<CustomSpawner>();
		//}

		//internal void RegenerateCustomSpawners()
		//{
		//	for (int i = 0; i < customSpawners.Length; i++)
		//	{
		//		customSpawners[i].Generate();
		//	}
		//} 
		#endregion
		[SerializeField]
		internal Transform leftSidewallParent;
		[SerializeField]
		internal Transform rightSidewallParent;
		[SerializeField]
		internal Transform backgroundParent;
	}
}
