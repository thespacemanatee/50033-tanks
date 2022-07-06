using System;
using PluggableAI.Decision;

[Serializable]
public class Transition
{
    public Decision decision;
    public State trueState;
    public State falseState;
}