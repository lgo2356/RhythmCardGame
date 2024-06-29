using System;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public class EndRhythmObject : MonoBehaviour
    {
        private Action m_OnDestroyAction;
        private Coroutine m_MoveCoroutine;

        public float Speed
        {
            get;
            set;
        }

        public void StartMove()
        {
            if (m_MoveCoroutine != null)
            {
                StopCoroutine(m_MoveCoroutine);
            }

            m_MoveCoroutine = StartCoroutine(MoveCoroutine());
        }

        public void SetOnDestroyListener(Action callback)
        {
            m_OnDestroyAction = callback;
        }

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                transform.localPosition += Vector3.right * Speed * Time.deltaTime;

                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("RhythmNoteDestroyCollider"))
            {
                m_OnDestroyAction?.Invoke();

                Destroy(gameObject);
            }
        }
    }
}
