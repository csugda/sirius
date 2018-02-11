using Assets.Scripts.AI.Components;
using Assets.Scripts.AI.TreeModel;
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

        public List<BehaviorTreeElement> BehaviorTreeList;

        //TODO: Add ILogger *(perhaps Observer pattern? This is our "singleton")
        //Dispatch messages to observed classes and receive that information here...
        //How to store? List? Dictionary? My face? Cat Pictures?
        void Start()
        {
            //This will act as the treeModel's root element. It will be hidden in the treeview.
            if (BehaviorTreeList == null) BehaviorTreeList = new List<BehaviorTreeElement>();
            if (Runner == null) Runner = new ParallelRunner("Main Root", -1, 0);

            Debug.Log("Starting ticks on runner. Runner: \n\t" + Runner.ToString());
            StartBehaviorTree();
            Debug.LogWarning("All Coroutines Should be DONE now! Ending all to make sure....");
            StopAllCoroutines();
        }

        /// <summary>
        /// Ticks on the aggregate ParallelRunner then continues ticking for as long as the runner is in running satte
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartBehaviorTree()
        {
            Runner.Tick();
            while (Runner.CurrentState == BehaviorState.Running)
            {
                yield return StartCoroutine(Runner.Tick());
            }
        }

        public TreeModel GetTreeModel<T>()
        {
            return null;
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
