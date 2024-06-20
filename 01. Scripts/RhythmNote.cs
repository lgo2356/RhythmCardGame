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
            StartCoroutine(MoveCoroutine());
        }

        public void SetOnDestroyListener(Action<RhythmNote> callback)
        {
            m_OnDestroyAction = callback;
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
                m_OnDestroyAction?.Invoke(this);
            }
        }
    }
}
