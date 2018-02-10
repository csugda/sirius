using Assets.Scripts.AI.TreeModel;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [Serializable]
    public class BehaviorTreeElement : TreeElement
    {
        [SerializeField]
        public string ElementType;

        [NonSerialized] public BehaviorManager BehaviorTreeManager;

        public BehaviorTreeElement(string name, int depth, int id) 
            : base(name, depth, id)
        {
            ElementType = this.GetType().Name.ToString();
        }

        private BehaviorState _CurrentState;
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