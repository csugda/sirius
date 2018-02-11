using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluentBehaviourTree;

public abstract class BehaviorTreeScript : MonoBehaviour
{
    protected IBehaviourTreeNode tree;
	
	virtual protected void Update () {
        this.tree.Tick(new TimeData(100000));
	}
}
