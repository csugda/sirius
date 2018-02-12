using Assets.Scripts.AI.Components;
using Assets.Scripts.AI.Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class BehaviorManager : MonoBehaviour
    {
        /// <summary>
        /// Primary Runner for this manager. 
        /// Runs all sub-behaviors/trees at the same time using the specified parallelrunner attributes.
        /// </summary>
        public ParallelRunner Runner;

        public IList<TreeElement> BehaviorTreeList;

        public float SecondsBetweenTicks = 0.1f;

        public int TimesToTick = 10;

        //TODO: Add ILogger *(perhaps Observer pattern? This is our "singleton")*
        //Dispatch messages to observed classes and receive that information here...
        //How to store? List? Dictionary? My face? Cat Pictures?
        IEnumerator Start()
        {
            //This will act as the treeModel's root element. It will be hidden in the treeview.
            if (BehaviorTreeList == null) BehaviorTreeList = new List<TreeElement>();
            if (Runner == null) Runner = new ParallelRunner("Main Root", -1, 0);

            WaitForSeconds wfs = new WaitForSeconds(SecondsBetweenTicks);

            Debug.Log("Starting ticks on Runner: \n\t" + Runner.ToString());
            Debug.Log("State: " + Runner.CurrentState);
            yield return Runner.Tick();
            while (Runner.CurrentState == BehaviorState.Running && TimesToTick >= 0)
            {
                yield return StartCoroutine(Runner.Tick(wfs));
                Debug.Log("State: " + Runner.CurrentState);
                --TimesToTick;
            }

            Debug.LogWarning("All Coroutines Should be DONE now! Ending all to make sure....");
            //StopAllCoroutines();
        }

        /// <summary>
        /// Ticks on the aggregate ParallelRunner then continues ticking for as long as the runner is in running satte
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartBehaviorTree()
        {
            WaitForSeconds wfs = new WaitForSeconds(SecondsBetweenTicks);
            Runner.Tick();
            Debug.Log("State: " + Runner.CurrentState);
            while (Runner.CurrentState == BehaviorState.Running)
            {
                yield return StartCoroutine(Runner.Tick(wfs));
                Debug.Log("State: " + Runner.CurrentState);
            }
        }

        public TreeModel<TreeElement> GetTreeModel()
        {
            return new TreeModel<TreeElement>(BehaviorTreeList);
        }

        bool LoadTree()
        {
            return false;
        }

        void SaveTree(string filepath)
        {
        }
    }
}
