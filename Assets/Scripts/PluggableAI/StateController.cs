using System.Collections.Generic;
using Tank;
using UnityEngine;
using UnityEngine.AI;

namespace PluggableAI
{
    public class StateController : MonoBehaviour
    {
        public State currentState;
        public EnemyStats enemyStats;
        public Transform eyes;
        public State remainState;

        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public TankShooting tankShooting;
        [HideInInspector] public TankPopupIcon popupIcon;
        [HideInInspector] public List<Transform> wayPointList;
        [HideInInspector] public int nextWayPoint;
        [HideInInspector] public Transform chaseTarget;
        [HideInInspector] public float stateTimeElapsed;

        private bool m_AIActive;


        private void Awake()
        {
            tankShooting = GetComponent<TankShooting>();
            popupIcon = GetComponent<TankPopupIcon>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!m_AIActive) return;

            currentState.UpdateState(this);
        }

        private void OnDrawGizmos()
        {
            if (currentState != null && eyes != null)
            {
                Gizmos.color = currentState.sceneGizmoColor;
                Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
            }
        }

        public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
        {
            wayPointList = wayPointsFromTankManager;
            m_AIActive = aiActivationFromTankManager;
            navMeshAgent.enabled = m_AIActive;
        }

        public void TransitionToState(State nextState)
        {
            if (nextState == remainState) return;
            currentState = nextState;
            OnExitState();
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return stateTimeElapsed >= duration;
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }
    }
}