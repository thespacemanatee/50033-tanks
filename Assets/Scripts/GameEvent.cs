using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent", order = 3)]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> m_EventListeners = new();

    public void Raise()
    {
        for(var i = m_EventListeners.Count -1; i >= 0; i--)
            m_EventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!m_EventListeners.Contains(listener))
            m_EventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (m_EventListeners.Contains(listener))
            m_EventListeners.Remove(listener);
    }
}