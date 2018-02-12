using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI.Components
{
    [System.Serializable]
    public class BehaviorComponent : BehaviorTreeElement
    {
        public LinkedList<BehaviorTreeElement> SubBehaviors = new LinkedList<BehaviorTreeElement>();

        public BehaviorComponent(string name, int depth, int id) 
            : base(name, depth, id)
        {
            SubBehaviors = new LinkedList<BehaviorTreeElement>();
        }

        public virtual void AddChild(BehaviorTreeElement element)
        {
            element.parent = this;
            element.BehaviorTreeManager = BehaviorTreeManager;
            SubBehaviors.AddLast(element);
        }

        public override string ToString()
        {
            string retString = base.ToString() + "\n";
            foreach (var child in SubBehaviors)
            {
                retString += ("-> " + child.ToString()).PadLeft(5);
            }

            return retString;
        }
    }
}
