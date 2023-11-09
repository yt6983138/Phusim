﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#pragma warning disable IDE1006 // Naming Styles
public class NoteOfficalChart : IOfficalNotes
{
    public short type { get; set; } // do not fix name violation on those
    public int time { get; set; }
    public float holdTime { get; set; }
    public float speed { get; set; }
    public float positionX { get; set; }
    public float floorPosition { get; set; }
    public Note ToInternalFormat()
    {
        Note _note = new Note()
        {
            RequireTap = (type == 1) ? true : (type == 3),
            RequireFlick = (type == 4),
            Time = this.time,
            State = NoteState.Pending,
            HoldTime = this.holdTime,
            Speed = this.speed,
            PositionX = this.positionX,
            FloorPosition = this.floorPosition
        };
        return _note;
    }
}
public class LineSpeedEventsOfficalChart : IOfficalChartEvents
{
    public float startTime { get; set; }
    public float endTime { get; set; }
    public float value { get; set; }
    public Event ToInternalFormat()
    {
        return new Event()
        {
            StartTime = this.startTime,
            EndTime = this.endTime,
            Speed = this.value, //tbh phi naming is kinda shitty
            EventType = JudgeLineEventType.SpeedEvent
        };
    }
}
public class LineMoveEventsOfficalChart : IOfficalChartEvents
{
    public float startTime { get; set; }
    public float endTime { get; set; }
    public float start { get; set; }
    public float end { get; set; }
    public float start2 { get; set; }
    public float end2 { get; set; }
    public Event ToInternalFormat()
    {
        return new Event()
        {
            StartTime = this.startTime,
            EndTime = this.endTime,
            StartX = this.start,
            StartY = this.start2,
            EndX = this.end,
            EndY = this.end2,
            EventType = JudgeLineEventType.MoveEvent
        };
    }
}
public class LineRotateEventsOfficalChart : IOfficalChartEvents
{
    public float startTime { get; set; }
    public float endTime { get; set; }
    public float start { get; set; }
    public float end { get; set; }
    public Event ToInternalFormat()
    {
        return new Event()
        {
            StartTime = this.startTime,
            EndTime = this.endTime,
            StartRotate = this.start,
            EndRotate = this.end,
            EventType = JudgeLineEventType.RotateEvent
        };
    }
}
public class LineDisappearEventsOfficalChart : IOfficalChartEvents // bro why disappear instead opacity
{
    public float startTime { get; set; }
    public float endTime { get; set; }
    public float start { get; set; }
    public float end { get; set; }
    public Event ToInternalFormat()
    {
        return new Event()
        {
            StartTime = this.startTime,
            EndTime = this.endTime,
            StartOpacity = this.start,
            EndOpacity = this.end,
            EventType = JudgeLineEventType.OpacityEvent
        };
    }
}
public class judgeLineListOfficalChart : IOfficalJudgeLines
{
    public float bpm { get; set; }
    public List<NoteOfficalChart> notesAbove { get; set; }
    public List<NoteOfficalChart> notesBelow { get; set; }
    public List<LineSpeedEventsOfficalChart> speedEvents { get; set; }
    public List<LineMoveEventsOfficalChart> judgeLineMoveEvents { get; set; }
    public List<LineRotateEventsOfficalChart> judgeLineRotateEvents { get; set; }
    public List<LineDisappearEventsOfficalChart> judgeLineDisappearEvents { get; set; } // to prevent u being confused on this one - disappear = opacity
    public JudgeLine ToInternalFormat()
    {
        List<Note> _notesAbove = new();
        List<Note> _notesBelow = new();
        List<Event> _events = new();
        List<IOfficalChartEvents> _officalChartEvents = new();
        _officalChartEvents.AddRange(this.speedEvents);
        _officalChartEvents.AddRange(this.judgeLineMoveEvents);
        _officalChartEvents.AddRange(this.judgeLineRotateEvents);
        _officalChartEvents.AddRange(this.judgeLineDisappearEvents);
        foreach (NoteOfficalChart note in notesAbove)
        {
            _notesAbove.Add(note.ToInternalFormat());
        }
        foreach (NoteOfficalChart note in notesBelow)
        {
            _notesBelow.Add(note.ToInternalFormat());
        }
        foreach (IOfficalChartEvents chartEvent in _officalChartEvents)
        {
            _events.Add(chartEvent.ToInternalFormat());
        }
        _events.Sort();
        return new JudgeLine()
        {
            BPM = this.bpm,
            NotesAbove = _notesAbove,
            NotesBelow = _notesBelow,
            Events = _events
        };
    }
}
public class OfficalChart : IOfficalCharts
{
    public int formatVersion { get; set; }
    public float offset { get; set; }
    public List<judgeLineListOfficalChart> judgeLineList { get; set; }
    public Chart ToInternalFormat()
    {
        List<JudgeLine> _judgeLines = new();
        foreach (judgeLineListOfficalChart line in this.judgeLineList)
        {
            _judgeLines.Add(line.ToInternalFormat());
        }
        return new Chart()
        {
            Format = "Offical:" + this.formatVersion.ToString(),
            Offset = this.offset,
            JudgeLineList = _judgeLines,
            Features = new()
            {
                Use3D = false,
                AllowOverride = false,
                AllowUseExternalTexture = false
            }
        };
    }
}
#pragma warning restore IDE1006 // Naming Styles