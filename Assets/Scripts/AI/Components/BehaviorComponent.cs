using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI.Components
{
    public abstract class BehaviorComponent : BehaviorTreeElement
    {
        protected LinkedList<BehaviorTreeElement> SubBehaviors = new LinkedList<BehaviorTreeElement>();

        public BehaviorComponent(string name, int depth, int id) 
            : base(name, depth, id)
        { }

        public virtual void AddChild(BehaviorTreeElement element)
        {
            SubBehaviors.AddLast(element);
        }

        public override string ToString()
        {
            string retString = base.ToString() + "\n";
            foreach (var child in SubBehaviors)
            {
                retString += "-> " + child.ToString().PadRight(4);
            }

            return retString;
        }
    }
}
