using DarkChocoSoft.RhythmCardGame;
using System.Collections.Generic;
using UnityEngine;

public class RhythmNoteHitTimingManager : MonoBehaviour
{
    [SerializeField] Transform m_NoteHitZoneTransform;
    [SerializeField] RectTransform[] m_NoteHitBoxRects;

    List<RhythmNote> m_RhythmNoteInstances = new();
    Vector2[] m_NoteHitBoxs;

    private int m_HitCount = 0;
    private int m_TotalScore = 0;

    public float HitRatio
    {
        get;
        private set;
    } = 0f;

    public void CheckHitTiming()
    {
        if (m_RhythmNoteInstances.Count == 0)
            return;

        RhythmNote note = m_RhythmNoteInstances[0];
        float notePosX = note.transform.localPosition.x;

        //TODO : Note 타입에 따라 다르게 처리

        for (int i = 0; i < m_NoteHitBoxs.Length; i++)
        {
            if (m_NoteHitBoxs[i].x <= notePosX && notePosX <= m_NoteHitBoxs[i].y)
            {
                m_RhythmNoteInstances.Remove(note);
                note.Destroy();

                switch (i)
                {
                    case 0:
                        m_TotalScore += 100;
                        break;

                    case 1:
                        m_TotalScore += 80;
                        break;

                    case 2:
                        m_TotalScore += 60;
                        break;

                    case 3:
                        m_TotalScore += 40;
                        break;

                    case 4:
                        m_TotalScore += 20;
                        break;
                }

                RecordHitRating();

                return;
            }
        }
    }

    public void CheckReleaseTiming()
    {
        
    }

    private void RecordHitRating()
    {
        m_HitCount++;
        HitRatio = m_TotalScore / m_HitCount;

        Debug.Log(HitRatio);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RhythmNote note))
        {
            m_RhythmNoteInstances.Add(note);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RhythmNote note))
        {
            m_RhythmNoteInstances.Remove(note);

            if (collision.gameObject.activeSelf)
            {
                Debug.Log("Break!");

                RecordHitRating();
            }
        }
    }

    void Awake()
    {
        m_NoteHitBoxs = new Vector2[m_NoteHitBoxRects.Length];

        for (int i = 0; i < m_NoteHitBoxs.Length; i++)
        {
            float newX = m_NoteHitZoneTransform.localPosition.x - m_NoteHitBoxRects[i].rect.width / 2;
            float newY = m_NoteHitZoneTransform.localPosition.x + m_NoteHitBoxRects[i].rect.width / 2;

            m_NoteHitBoxs[i].Set(newX, newY);
        }
    }

    void Start()
    {
        
    }
}
