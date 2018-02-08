using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluentBehaviourTree;

public class SampleBehaviorTree : MonoBehaviour {

    IBehaviourTreeNode tree;

	void Start () {
        var builder = new BehaviourTreeBuilder();
        this.tree = builder
            .Sequence("Sample-Sequence")
            .Do("SampleAction1", t =>
            {
                Debug.Log("I am doing action 1!");
                return BehaviourTreeStatus.Success;
            })
            .Do("SampleAction2", tree =>
            {
                Debug.Log("I am doing the second action!");
                return BehaviourTreeStatus.Success;
            })
            .End()
            .Build();
	}
	
	void Update () {
        this.tree.Tick(new TimeData(100000));
	}
}
