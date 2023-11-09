using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Note : IComparable<Note>
{
    public NoteState State { get; set; }
    public bool RequireFlick { get; set; }
    public bool RequireTap { get; set; }
    public bool IsFake { get; set; }
    public bool DoublePress { get; set; }
    public int Id { get; set; }
    /// <summary>
    /// so basically 4 beat = 32, 1 beat = 8, and like 1/3 = 11(32/3), 0:1/4 in rpe = 8
    /// </summary>
    public int Time { get; set; } 
    public float HoldTime { get; set; }
    public float Speed { get; set; } // speed, this * judgeline speed = real speed
    public float PositionX { get; set; } // -9 ~ 9 :thonking:
    /// <summary>
    /// <example>
    /// ypos for note, *note, the note can be disappeared even it havent touch the judgement line yet, also it is also disappearing when it touches judge line(tho it havent time out) <br/>
    /// also varies with *judgeline* speed and time, does not vary with *note* speed<br/>
    /// example: <br/>
    /// 4th beat, judgeline speed 20, gives time 32 and pos 20<br/>
    /// 8th beat, judgeline speed 20, gives time 64 and pos 40<br/>
    /// 8th beat, judgeline speed 20, note speed 2 gives time 64 and pos 40<br/>
    /// </example>
    /// </summary>
    public float FloorPosition { get; set; } 
    public float HitTime { get; set; }
    /// <summary>
    /// basically time out
    /// </summary>
    public void Process()
    {
        // this.DoublePress = NoteManager.CheckMultiPress(this.Time);

        this.State = NoteState.DoneLoading;
    }
    public int CompareTo(Note note)
    {
        return this.Id.CompareTo(note.Id);
    }
}
