using FluentBehaviourTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BehaviorTree
{
    class SampleBehaviorTree : BehaviorTreeScript
    {
        void Start()
        {
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
    }
}
