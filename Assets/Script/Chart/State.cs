using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum NoteState
{
    Pending = 0,
    DoneLoading = 1
}

public enum JudgeLineEventType
{
    UnsetEvent = 0,
    SpeedEvent = 1,
    MoveEvent = 2,
    RotateEvent = 3,
    OpacityEvent = 4
}