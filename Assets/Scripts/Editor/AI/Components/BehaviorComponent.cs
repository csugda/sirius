using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI.Components
{
    [System.Serializable]
    public abstract class BehaviorComponent : BehaviorTreeElement
    {
        public BehaviorComponent(string name, int depth, int id) 
            : base(name, depth, id)
        {
            children = new List<Tree.TreeElement>();
        }

        public virtual void AddChild(BehaviorTreeElement element)
        {
            element.parent = this;
            element.BehaviorTreeManager = BehaviorTreeManager;
            children.Add(element);
        }

        public override string ToString()
        {
            string retString = base.ToString() + "\n";
            foreach (var child in children)
            {
                retString += ("-> " + child.ToString()).PadLeft(5);
            }

            return retString;
        }
    }
}
