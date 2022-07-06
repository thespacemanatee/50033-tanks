using UnityEngine;

namespace PluggableAI.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
    public class PatrolAction : Action
    {
        public override void Act(StateController controller)
        {
            Patrol(controller);
        }

        private static void Patrol(StateController controller)
        {
            controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
            controller.navMeshAgent.isStopped = false;
            controller.popupIcon.LoadScanIcon();

            if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance &&
                !controller.navMeshAgent.pathPending)
                controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }
}