using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class JudgeLine
{
    public NoteManager NoteManager = new();
    public int Id { get; set; }
    public float BPM { get; set; }
    public List<Note> NotesAbove { get; set; }
    public List<Note> NotesBelow { get; set; }
    public List<Event> Events { get; set; }
}
