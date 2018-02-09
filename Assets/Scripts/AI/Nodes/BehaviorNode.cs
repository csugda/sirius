using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.Nodes
{
    public abstract class BehaviorNode : BehaviorTreeElement
    {
        public BehaviorNode(string name, int depth, int id) 
            : base(name, depth, id, BehaviorType.LeafNode)
        {
        }

        public override IEnumerator Tick()
        {
            return base.Tick();
        }
    }
}