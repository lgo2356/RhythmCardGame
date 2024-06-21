using DarkChocoSoft.RhythmCardGame.Interface;
using System;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public class RhythmNote : MonoBehaviour, IRhythmNote
    {
        private RhythmNoteData m_Data;
        private Action<RhythmNote> m_OnDestroyAction;
        private Coroutine m_MoveCoroutine;
        private bool m_IsStarted = false;

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
            m_IsStarted = true;
            //if (m_MoveCoroutine != null)
            //{
            //    StopCoroutine(m_MoveCoroutine);
            //}

            //m_MoveCoroutine = StartCoroutine(MoveCoroutine());
        }

        public void SetOnDestroyListener(Action<RhythmNote> callback)
        {
            m_OnDestroyAction = callback;
        }

        public void Destroy()
        {
            //if (m_MoveCoroutine != null)
            //{
            //    StopCoroutine(m_MoveCoroutine);
            //    m_MoveCoroutine = null;
            //}            

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("RhythmNoteDestroyCollider"))
            {
                Destroy();
            }
        }

        private void Update()
        {
            if (!m_IsStarted)
                return;

            transform.localPosition += Vector3.right * m_Data.Speed * Time.deltaTime;
        }

        private void OnDisable()
        {
            m_IsStarted = false;
            m_OnDestroyAction = null;
        }
    }
}
