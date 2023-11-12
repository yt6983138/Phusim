using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class JudgeLine
{
    // migrating smth
    private long maxTime;
    private int[] Counter;
    private bool hasComputed = false;
    // migrating end
    
    public int Id { get; set; }
    public float BPM { get; set; }
    public List<Note> Notes { get; set; }
    public List<Event> Events { get; set; }
    public void Process()
    {
        Notes.Sort();
        Events.Sort();
        AssignIds();
        ComputeMultiPress();
        foreach (Note note in Notes)
        {
            note.MultiPress = Counter[note.Time] > 1; // check multi press
            note.State = NoteState.DoneLoading;
        }
    }
    private void AssignIds()
    {
        for (int i = 0; i < Notes.Count; i++)
        {
            Notes[i].Id = i;
        }
    }
    private void ComputeMultiPress()
    {
        if (hasComputed) { return; }
        maxTime = Notes.Last().Time;
        Counter = new int[maxTime];
        foreach (Note note in Notes)
        {
            Counter[note.Time]++;
        }
        hasComputed = true;
    }
}
