using UnityEngine;

namespace PluggableAI.Decision
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(StateController controller);
    }
}