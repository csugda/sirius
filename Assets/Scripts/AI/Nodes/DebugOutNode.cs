﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Nodes
{
    public class DebugOutNode : BehaviorNode
    {
        public DebugOutNode(string name, int depth, int id) 
            : base(name, depth, id)
        {
        }

        public override IEnumerator Tick(WaitForSeconds delayStart = null)
        {
            Debug.Log(name + "node starting... waiting...");
            yield return delayStart;
            Debug.Log("BEHAVIOR NODE DOIN THE THANG!");
            CurrentState = BehaviorState.Success;
            yield return null;
        }
    }
}
