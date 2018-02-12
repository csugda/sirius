using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Components
{
    public class ParallelRunner : BehaviorComponent
    {
        /// <summary>
        /// Number of times the children return fail before the parallel runner returns in a fail state.
        /// 0 means ignore number of failures.
        /// 0 for both succeed and fail means loops infinitely
        /// </summary>
        public int NumberOfFailuresBeforeFail = 0;

        /// <summary>
        /// Number of times the children return success before the parallel runner returns in a success state.
        /// 0 means ignore number of sucesses.
        /// 0 for both succeed and fail means loops infinitely
        /// </summary>
        public int NumberOfSuccessBeforeSucceed = 0;

        public ParallelRunner(string name, int depth, int id)
            : base(name, depth, id) { }

        public override IEnumerator Tick(WaitForSeconds delayStart = null)
        {
            int numFail = 0;
            int numSucceed = 0;

            Debug.Log("Starting Parallel Tick.");

            CurrentState = BehaviorState.Running;

            yield return delayStart;
            foreach (var behavior in SubBehaviors)
            {
                BehaviorTreeManager.StartCoroutine(behavior.Tick());

                if(NumberOfFailuresBeforeFail != 0 && behavior.CurrentState == BehaviorState.Fail)
                {
                    ++numFail;
                    if(numFail >= NumberOfFailuresBeforeFail)
                    {
                        CurrentState = BehaviorState.Fail;
                        yield break;
                    }
                }

                if (NumberOfSuccessBeforeSucceed != 0 && behavior.CurrentState == BehaviorState.Success)
                {
                    ++numSucceed;
                    if (numSucceed >= NumberOfSuccessBeforeSucceed)
                    {
                        CurrentState = BehaviorState.Success;
                        yield break;
                    }
                }
                Debug.Log("Ending Parallel Tick in Run State.");
                CurrentState = BehaviorState.Running;
            }
        }
    }
}
