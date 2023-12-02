using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


#pragma warning disable IDE1006 // Naming Styles


public class NoteOfficalChart : IOfficalNotes
{
    public short type { get; set; } // do not fix name violation on those
    public int time { get; set; }
    public float holdTime { get; set; }
    public float speed { get; set; }
    public float positionX { get; set; }
    public float floorPosition { get; set; }
    public Note ToInternalFormat(bool isAbove)
    {
        Note _note = new()
        {
            RequireTap = (type == 1) || (type == 3),
            RequireFlick = (type == 4),
            IsAbove = isAbove,
            UseOverrideTexture = false,
            Time = this.time,
            State = NoteState.Pending,
            HoldTime = this.holdTime,
            Speed = this.speed,
            PositionX = this.positionX / Resource.OfficalChartPosXMagicNumber,
            PositionY = this.floorPosition
        };
        return _note;
    }
}
public class LineSpeedEventsOfficalChart : IOfficalChartEvents
{
    public float startTime { get; set; }
    public float endTime { get; set; }
    public float value { get; set; }
    public IInternalEventFormat ToInternalFormat()
    {
        return new InternalSpeedEvent()
        {
            StartTime = this.startTime,
            EndTime = this.endTime,
            Speed = this.value //tbh phi naming is kinda shitty
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
    public IInternalEventFormat ToInternalFormat()
    {
        return new InternalMovementEvent()
        {
            StartTime = this.startTime,
            EndTime = this.endTime,
            MovementStart = new Vector3() { x = this.start, y = this.start2, z = 0 },
            MovementEnd = new Vector3() { x = this.end, y = this.end2, z = 0 }
        };
    }
}
public class LineRotateEventsOfficalChart : IOfficalChartEvents
{
    public float startTime { get; set; }
    public float endTime { get; set; }
    public float start { get; set; }
    public float end { get; set; }
    public IInternalEventFormat ToInternalFormat()
    {
        return new InternalRotationEvent()
        {
            StartTime = this.startTime,
            EndTime = this.endTime,
            RotationStart = new Vector3() { x = 0, y = 0, z = this.start },
            RotationEnd = new Vector3() { x = 0, y = 0, z = this.end }
        };
    }
}
public class LineDisappearEventsOfficalChart : IOfficalChartEvents // bro why disappear instead opacity
{
    public float startTime { get; set; }
    public float endTime { get; set; }
    public float start { get; set; }
    public float end { get; set; }
    public IInternalEventFormat ToInternalFormat()
    {
        return new InternalOpacityEvent()
        {
            StartTime = this.startTime,
            EndTime = this.endTime,
            StartOpacity = this.start,
            EndOpacity = this.end
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
    public JudgeLineInternalFormat ToInternalFormat()
    {
        List<Note> _notes = new();
        List<IInternalEventFormat> _events = new();
        List<IOfficalChartEvents> _officalChartEvents = new();
        _officalChartEvents.AddRange(this.speedEvents);
        _officalChartEvents.AddRange(this.judgeLineMoveEvents);
        _officalChartEvents.AddRange(this.judgeLineRotateEvents);
        _officalChartEvents.AddRange(this.judgeLineDisappearEvents);
        foreach (NoteOfficalChart note in notesAbove)
        {
            _notes.Add(note.ToInternalFormat(isAbove: true));
        }
        foreach (NoteOfficalChart note in notesBelow)
        {
            _notes.Add(note.ToInternalFormat(isAbove: false));
        }
        foreach (IOfficalChartEvents chartEvent in _officalChartEvents)
        {
            _events.Add(chartEvent.ToInternalFormat());
        }
        _events.Sort();
        return new JudgeLineInternalFormat()
        {
            BPM = this.bpm,
            Notes = _notes,
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
        List<JudgeLineInternalFormat> _judgeLines = new();
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