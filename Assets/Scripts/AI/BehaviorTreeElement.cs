using Assets.Scripts.AI.TreeModel;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [System.Serializable]
    public class BehaviorTreeElement : TreeElement
    {
        public BehaviorType ElementType = BehaviorType.None;
        public BehaviorManager BehaviorTreeManager;

        private BehaviorState _CurrentState;

        public BehaviorTreeElement(string name, int depth, int id, BehaviorType bType = BehaviorType.None) 
            : base(name, depth, id)
        {
            this.ElementType = bType;
        }

        public BehaviorState CurrentState
        {
            get
            {
                return _CurrentState;
            }
            protected set
            {
                _CurrentState = value;
            }
        }

        public virtual IEnumerator Tick()
        {
            yield return null;
        }

        public override string ToString()
        {
            return "ID: " + id + "\n" +
                   "Name: " + this.name + "\n" +
                   "Depth: " + depth + "\n" +
                   "Type: " + ElementType.ToString();
        }
    }
}