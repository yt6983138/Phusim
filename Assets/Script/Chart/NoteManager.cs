using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NoteManager
{
    public JudgeLineInternalFormat AssignedLine { get; set; }
    public List<GameObject> NoteObjects { get; set; }
    public void InitializeNotes()
    {
        foreach (Note note in AssignedLine.Notes)
        {
            NoteObjects[note.Id] = GameObject.Instantiate(NoteTextureManager.NoteTemplate, ChartManager.JudgeLineManager.JudgeLinesObject[AssignedLine.Id].transform);
        }
    }
}
