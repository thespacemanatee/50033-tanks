using UnityEngine;

namespace PluggableAI.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        private static void Chase(StateController controller)
        {
            controller.navMeshAgent.destination = controller.chaseTarget.position;
            controller.navMeshAgent.isStopped = false;
            controller.popupIcon.LoadChaseIcon();
        }
    }
}