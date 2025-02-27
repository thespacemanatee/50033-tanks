﻿using PluggableAI.Actions;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public class State : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;
        public Color sceneGizmoColor = Color.grey;

        public void UpdateState(StateController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(StateController controller)
        {
            for (var i = 0; i < actions.Length; i++) actions[i].Act(controller);
        }

        private void CheckTransitions(StateController controller)
        {
            for (var i = 0; i < transitions.Length; ++i)
            {
                var decisionSucceded = transitions[i].decision.Decide(controller);

                if (decisionSucceded)
                    controller.TransitionToState(transitions[i].trueState);
                else
                    controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}