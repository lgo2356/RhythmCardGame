using DarkChocoSoft.RhythmCardGame.Data;
using System;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public abstract class RhythmNote : MonoBehaviour
    {
        protected RhythmNoteDto m_Data;
        protected Action<RhythmNote> m_OnDestroyAction;
        protected Coroutine m_MoveCoroutine;

        public void SetData(RhythmNoteDto data)
        {
            m_Data = data;
        }

        public void StartMove(int speed)
        {
            if (m_MoveCoroutine != null)
            {
                StopCoroutine(m_MoveCoroutine);
            }

            m_MoveCoroutine = StartCoroutine(MoveCoroutine(speed));
        }

        public void SetOnDestroyListener(Action<RhythmNote> callback)
        {
            m_OnDestroyAction = callback;
        }

        public void Destroy()
        {
            if (m_MoveCoroutine != null)
            {
                StopCoroutine(m_MoveCoroutine);
                m_MoveCoroutine = null;
            }

            m_OnDestroyAction?.Invoke(this);
        }

        private IEnumerator MoveCoroutine(int speed)
        {
            while (true)
            {
                transform.localPosition += Vector3.right * speed * Time.deltaTime;

                yield return null;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("RhythmNoteDestroyCollider"))
            {
                Destroy();
            }
        }

        private void OnDisable()
        {
            m_OnDestroyAction = null;
        }
    }
}
