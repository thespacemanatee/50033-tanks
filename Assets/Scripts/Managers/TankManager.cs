﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class TankManager
    {
        public Color m_PlayerColor;
        public Transform m_SpawnPoint;
        [HideInInspector] public int m_PlayerNumber;
        [HideInInspector] public string m_ColoredPlayerText;
        [HideInInspector] public GameObject m_Instance;
        [HideInInspector] public int m_Wins;
        private GameObject m_CanvasGameObject;


        private TankMovement m_Movement;
        private TankShooting m_Shooting;
        private StateController m_StateController;

        public void SetupAI(List<Transform> wayPointList)
        {
            m_StateController = m_Instance.GetComponent<StateController>();
            m_StateController.SetupAI(true, wayPointList);

            m_Shooting = m_Instance.GetComponent<TankShooting>();
            m_Shooting.m_PlayerNumber = m_PlayerNumber;

            m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;
            m_ColoredPlayerText =
                $"<color=#{ColorUtility.ToHtmlStringRGB(m_PlayerColor)}>AI {m_PlayerNumber - 1}</color>";

            var renderers =  m_Instance.transform.Find("TankRenderers").GetComponentsInChildren<MeshRenderer>();

            for (var i = 0; i < renderers.Length; i++) renderers[i].material.color = m_PlayerColor;
        }


        public void SetupPlayerTank()
        {
            m_Movement = m_Instance.GetComponent<TankMovement>();
            m_Shooting = m_Instance.GetComponent<TankShooting>();
            m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

            m_Movement.m_PlayerNumber = m_PlayerNumber;
            m_Shooting.m_PlayerNumber = m_PlayerNumber;

            m_ColoredPlayerText =
                $"<color=#{ColorUtility.ToHtmlStringRGB(m_PlayerColor)}>PLAYER {m_PlayerNumber}</color>";

            // Get tank renderers specifically as we don't want to color the meshes in Canvas etc.
            var renderers =  m_Instance.transform.Find("TankRenderers").GetComponentsInChildren<MeshRenderer>();

            for (var i = 0; i < renderers.Length; i++) renderers[i].material.color = m_PlayerColor;
        }

        public void DisableControl()
        {
            if (m_Movement != null) m_Movement.enabled = false;

            if (m_StateController != null) m_StateController.enabled = false;

            m_Shooting.enabled = false;

            m_CanvasGameObject.SetActive(false);
        }

        public void EnableControl()
        {
            if (m_Movement != null) m_Movement.enabled = true;

            if (m_StateController != null) m_StateController.enabled = true;

            m_Shooting.enabled = true;
            m_CanvasGameObject.SetActive(true);
        }

        public void Reset(bool enabled)
        {
            m_Instance.transform.position = m_SpawnPoint.position;
            m_Instance.transform.rotation = m_SpawnPoint.rotation;

            m_Instance.SetActive(false);
            m_Instance.SetActive(enabled);
        }
    }
}