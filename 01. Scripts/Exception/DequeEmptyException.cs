using System;
using UnityEngine;

namespace DarkChocoSoft.Exception
{
    public class DequeEmptyException : SystemException
    {
        public DequeEmptyException() { }
        public DequeEmptyException(string message)
        {
            Debug.LogError(message);
        }
    }
}
