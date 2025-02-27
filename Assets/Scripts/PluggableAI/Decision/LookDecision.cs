﻿using UnityEngine;

namespace PluggableAI.Decision
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
    public class LookDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            var targetVisible = Look(controller);
            return targetVisible;
        }

        private static bool Look(StateController controller)
        {
            var position = controller.eyes.position;
            var radius = controller.enemyStats.lookSphereCastRadius;
            var direction = controller.eyes.forward;
            var lookRange = controller.enemyStats.lookRange;

            Debug.DrawRay(position, direction.normalized * lookRange, Color.green);

            if (Physics.SphereCast(position, radius, direction, out var hit, lookRange) && hit.collider.CompareTag("Player"))
            {
                controller.chaseTarget = hit.transform;
                return true;
            }

            return false;
        }
    }
}