using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI.Decorators
{
    public class Inverter : BehaviorDecorator
    {
        public Inverter(string name, int depth, int id) 
            : base(name, depth, id)
        { }

        public override IEnumerator Tick(WaitForSeconds delaySTart = null)
        {
            if (children == null) yield return null;
            if (children.Count <= 0) yield return null;
            var behavior = children[0] as BehaviorTreeElement;
            yield return BehaviorTreeManager.StartCoroutine(behavior.Tick());
            Debug.Log("Inverting " + children[0].name);
            switch (behavior.CurrentState)
            {
                case BehaviorState.Fail:
                    this.CurrentState = BehaviorState.Success;
                    break;
                case BehaviorState.Success:
                    CurrentState = BehaviorState.Fail;
                    break;
                case BehaviorState.Running:
                    this.CurrentState = BehaviorState.Running;
                    break;
                default:
                    Debug.LogError("Something went wrong in an inverter.");
                    break;
            }
        }
    }
}