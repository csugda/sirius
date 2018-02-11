using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.AI.Components
{
    /// <summary>
    /// Ticks all sub behaviors until a behavior returns success.
    /// Returns and is in success state if a child was successful, otherwise returns in fail state
    /// </summary>
    public class Selector : BehaviorComponent
    {
        public Selector(string name, int depth, int id) 
            : base(name, depth, id)
        { }

        public override IEnumerator Tick(UnityEngine.WaitForSeconds delayStart = null)
        {
            yield return delayStart;
            foreach (var behavior in SubBehaviors)
            {
                yield return BehaviorTreeManager.StartCoroutine(behavior.Tick());

                if (behavior.CurrentState != BehaviorState.Fail)
                {
                    this.CurrentState = behavior.CurrentState;

                    if (this.CurrentState == BehaviorState.Success)
                    {
                        //This selector has completed, break out of the operation
                        yield break;
                    }
                }
            }
            //if it gets here, it went through all subbehaviors and had no successes
            CurrentState = BehaviorState.Fail;
            yield break;
        }
    } 
}