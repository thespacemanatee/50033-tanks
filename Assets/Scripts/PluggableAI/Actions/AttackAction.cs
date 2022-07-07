using UnityEngine;

namespace PluggableAI.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Act(StateController controller)
        {
            Attack(controller);
        }

        private static void Attack(StateController controller)
        {
            RaycastHit hit;
            var position = controller.eyes.position;
            var radius = controller.enemyStats.lookSphereCastRadius;
            var direction = controller.eyes.forward;
            var attackRange = controller.enemyStats.attackRange;

            Debug.DrawRay(position, direction.normalized * attackRange, Color.red);

            if (Physics.SphereCast(position, radius, direction, out hit, attackRange) && hit.collider.CompareTag("Player"))
                if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate))
                    controller.tankShooting.Fire(controller.enemyStats.attackForce, controller.enemyStats.attackRate);
        }
    }
}