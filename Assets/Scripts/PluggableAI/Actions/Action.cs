using UnityEngine;

namespace PluggableAI.Actions
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}