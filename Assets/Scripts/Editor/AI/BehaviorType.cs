using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// The class types that a behavior can be. 
    /// Used in instantiating and saving/loading from the behavior tree assets.
    /// </summary>
    public enum BehaviorType
    {
        None,
        LeafNode,
        Inverter,
        Selector,
        Sequencer
    }
}
