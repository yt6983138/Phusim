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
    public bool MultiPress { get; set; }
    public bool IsAbove { get; set; }
    public bool UseOverrideTexture { get; set; }
    public int Id { get; set; }
    /// <summary>
    /// so basically 4 beat = 32, 1 beat = 8, and like 1/3 = 11(32/3), 0:1/4 in rpe = 8
    /// </summary>
    public int Time { get; set; } 
    public float HoldTime { get; set; }
    /// <summary>
    /// speed, this * judgeline speed = real speed, does not affect floorPos
    /// </summary>
    public float Speed { get; set; } 
    /// <summary>
    /// -9 ~ 9 :thonking:
    /// </summary>
    public float PositionX { get; set; }
    /// <summary>
    /// ypos for note, *note, the note can be disappeared even it havent touch the judgement line yet, also it is also disappearing when it touches judge line(tho it havent time out) <br/>
    /// also varies with *judgeline* speed, time, and bpm, does not vary with *note* speed<br/>
    /// also floor pos 1 = 0.5 screen height
    /// <example>
    /// example: <br/>
    /// 4th beat, judgeline speed 20, bpm 60, gives time 32 and pos 20<br/>
    /// 4th beat, judgeline speed 40, bpm 60, gives time 32 and pos 40<br/>
    /// 4th beat, judgeline speed 20, bpm 120, gives time 32 and pos 10<br/>
    /// 8th beat, judgeline speed 20, bpm 60, gives time 64 and pos 40<br/>
    /// 8th beat, judgeline speed 20, bpm 60, note speed 2 gives time 64 and pos 40<br/>
    /// however, the actual pos is note speed * this <br/>
    /// </example>
    /// (just floorPosition in offical chart lmao)
    /// </summary>
    public float PositionY { get; set; } 
    /// <summary>
    /// basically time out, in ms
    /// </summary>
    public float HitTime { get; set; }
    public int CompareTo(Note note)
    {
        return this.Id.CompareTo(note.Time);
    }
}
