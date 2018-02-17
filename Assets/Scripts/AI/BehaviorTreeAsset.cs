using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [System.Serializable]
	public class BehaviorTreeAsset : ScriptableObject
	{
        [SerializeField]
        List<BehaviorTreeElement> _TreeElements =
            new List<BehaviorTreeElement>();

		public List<BehaviorTreeElement> treeElements
		{
			get { return _TreeElements; }
			set { _TreeElements = value; }
		}
	}
}
