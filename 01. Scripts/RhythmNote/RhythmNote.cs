using DarkChocoSoft.RhythmCardGame.Interface;
using System;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public abstract class RhythmNote : MonoBehaviour, IRhythmNote
    {
        protected RhythmNoteData m_Data;
        protected Action<RhythmNote> m_OnDestroyAction;
        protected Coroutine m_MoveCoroutine;

        public void InitRhythmNote(RhythmNoteData data)
        {
            m_Data = data;
        }

        public void SetConfig(ScriptableObject config)
        { 
            //Config = config as RhythmNoteConfig;
        }

        public void StartMove()
        {
            if (m_MoveCoroutine != null)
            {
                StopCoroutine(m_MoveCoroutine);
            }

            m_MoveCoroutine = StartCoroutine(MoveCoroutine());
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

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                transform.localPosition += Vector3.right * m_Data.Speed * Time.deltaTime;

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
