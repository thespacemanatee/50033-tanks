using System;

namespace PluggableAI
{
    [Serializable]
    public class Transition
    {
        public Decision.Decision decision;
        public State trueState;
        public State falseState;
    }
}