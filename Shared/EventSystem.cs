using System;
using System.Collections.Generic;
using UnityEngine;

namespace Luaxe.Shared.Events
{
    /// <summary>
    /// An abstract class to hold various information about any event that occurs in the game.
    /// </summary>
    public abstract class GameEvent
    {
        /// <summary>
        /// Name of the event used in Lua.
        /// </summary>
        public abstract string luaEventName { get; }
    }

    /// <summary>
    /// Shared EventSystem to capture and listen for GameEvents. 
    /// </summary>
    public class EventSystem
    {
        static readonly Dictionary<Type, List<Delegate>> s_Events = new Dictionary<Type, List<Delegate>>();

        /// <summary>
        /// Add a listener to a GameEvent.
        /// </summary>
        /// <typeparam name="T">Type of GameEvent to listen for</typeparam>
        /// <param name="evt"></param>
        public static void AddListener<T>(Func<T, bool> evt) where T : GameEvent
        {
            Type t = typeof(T);
            if (!s_Events.ContainsKey(t))
            {
                s_Events.Add(t, new List<Delegate>());
            }

            if (s_Events.TryGetValue(t, out var funcsList))
            {
                funcsList.Add(evt);
            }
        }

        public static void RemoveListener(Func<GameEvent, bool> evt)
        {
            Type t = evt.GetType();
            if (!s_Events.ContainsKey(t))
            {
                return;
            }

            if (s_Events.TryGetValue(t, out var funcsList))
            {
                funcsList.Remove(evt);
            }
        }

        public static bool Broadcast(GameEvent evt)
        {
            bool returnValue = true;
            if (s_Events.TryGetValue(evt.GetType(), out var funcsList))
            {
                foreach (Delegate dg in funcsList)
                {
                    if(!(bool)dg.DynamicInvoke(evt))
                    {
                        returnValue = false;
                    }
                }
            }

            return returnValue;
        }

        public static void Clear()
        {

        }
    }
}