using DarkChocoSoft.RhythmCardGame;
using System.Collections.Generic;
using UnityEngine;

public class RhythmNoteHitTimingManager : MonoBehaviour
{
    [SerializeField] Transform m_NoteHitZoneTransform;
    [SerializeField] RectTransform[] m_NoteHitBoxRects;

    public List<RhythmNote> m_RhythmNoteInstances = new();
    Vector2[] m_NoteHitBoxs;

    public void CheckHitTiming()
    {
        if (m_RhythmNoteInstances.Count == 0)
            return;

        RhythmNote note = m_RhythmNoteInstances[0];
        float notePosX = note.transform.localPosition.x;

        for (int i = 0; i < m_NoteHitBoxs.Length; i++)
        {
            if (m_NoteHitBoxs[i].x <= notePosX && notePosX <= m_NoteHitBoxs[i].y)
            {
                m_RhythmNoteInstances.Remove(note);
                note.Destroy();

                return;
            }
        }
    }

    public void CheckReleaseTiming()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RhythmNote note = collision.GetComponent<RhythmNote>();
        m_RhythmNoteInstances.Add(note);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RhythmNote note = collision.GetComponent<RhythmNote>();
        m_RhythmNoteInstances.Remove(note);

        Debug.Log("Break!");
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
