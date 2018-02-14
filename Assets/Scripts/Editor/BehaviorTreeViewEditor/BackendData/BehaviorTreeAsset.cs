using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeViewEditor.BackendData
{
    [System.Serializable]
	public class BehaviorTreeAsset : ScriptableObject
	{
        [SerializeField]
        List<BehaviorTreeElement> _TreeElements =
            new List<BehaviorTreeElement>();

		internal List<BehaviorTreeElement> treeElements
		{
			get { return _TreeElements; }
			set { _TreeElements = value; }
		}
	}
}
