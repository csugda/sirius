﻿using System.Collections;
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
            yield return BehaviorTreeManager.StartCoroutine(DecoratedBehavior.Tick());
            Debug.Log("Inverting " + DecoratedBehavior.name);
            switch (DecoratedBehavior.CurrentState)
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