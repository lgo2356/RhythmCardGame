using DarkChocoSoft.Exception;
using System.Collections.Generic;

namespace DarkChocoSoft.Algorithm.DataStructure
{
    public class Deque<T>
    {
        private LinkedList<T> m_List = new();

        public void EnqueueFront(T item)
        {
            m_List.AddFirst(item);
        }

        public void EnqueueBack(T item)
        {
            m_List.AddLast(item);
        }

        public T DequeueFront()
        {
            if (m_List.Count > 0)
            {
                T item = m_List.First.Value;
                m_List.RemoveFirst();
                return item;
            }
            else
            {
                throw new DequeEmptyException("Deque is empty.");
            }
        }

        public T DequeueBack()
        {
            if (m_List.Count > 0)
            {
                T item = m_List.Last.Value;
                m_List.RemoveLast();
                return item;
            }
            else
            {
                throw new DequeEmptyException("Deque is empty.");
            }
        }

        public int Count
        {
            get { return m_List.Count; }
        }
    }
}
