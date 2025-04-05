using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public interface IEvent { }

    public interface IEVentReceiverBase { }
    
    public interface IEventReceiver<T> : IEVentReceiverBase where T : struct, IEvent
    {
        void OnEvent(T e);
    }

    public static class EventBus<T> where T : struct, IEvent
    {
        private static IEventReceiver<T>[] m_buffer;
        private static int m_count;
        private static int m_blocksize = 256;

        private static HashSet<IEventReceiver<T>> m_hash;

        static EventBus()
        {
            m_hash = new HashSet<IEventReceiver<T>>();
            m_buffer = new IEventReceiver<T>[0];
        }

        public static void Register(IEVentReceiverBase handler)
        {
            m_count++;
            m_hash.Add(handler as IEventReceiver<T>);
            if (m_buffer.Length < m_count)
            {
                m_buffer = new IEventReceiver<T>[m_count + m_blocksize];
            }

            m_hash.CopyTo(m_buffer);
        }

        public static void Unregister(IEVentReceiverBase handler)
        {
            m_hash.Remove(handler as IEventReceiver<T>);
            m_hash.CopyTo(m_buffer);
            m_count--;
        }

        public static void Raise(T e)
        {
            for (int i = 0; i < m_count; i++)
            {
                m_buffer[i].OnEvent(e);
            }
        }

        public static void RaiseAsInterface(IEvent e)
        {
            Raise((T)e);
        }

        public static void Clear()
        {
            m_hash.Clear();
        }
    }
}
