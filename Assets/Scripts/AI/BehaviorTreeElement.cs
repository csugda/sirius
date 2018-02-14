using Assets.Scripts.AI.Tree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [Serializable]
    public class BehaviorTreeElement : TreeElement
    {
        [SerializeField] public string ElementType;

        public BehaviorManager BehaviorTreeManager;

        public BehaviorTreeElement(string name, int depth, int id) 
            : base(name, depth, id)
        {
            ElementType = this.GetType().ToString();
            _CurrentState = BehaviorState.Null;
        }

        [SerializeField]
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

        public virtual IEnumerator Tick(WaitForSeconds delayStart = null)
        {
            if (delayStart != null)
            {
                yield return delayStart;
            }

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