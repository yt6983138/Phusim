using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public class InternalSpeedEvent : IComparable<IInternalEventFormat>, IInternalEventFormat
{
    /// <inheritdoc />
    public float StartTime { get; set; }
    /// <inheritdoc />
    public float EndTime { get; set; }
    /// <summary>
    /// idk how does this work
    /// -- nvm- redefine this to 1 speed = 1 screen height/s
    /// </summary>
    public float Speed { get; set; }
    public bool HasSameTime(IInternalEventFormat compareTo)
    {
        if (this.StartTime == compareTo.StartTime && this.EndTime == compareTo.EndTime) return true;
        else return false;
    }
    public int CompareTo(IInternalEventFormat chartEvent)
    {
        return this.StartTime.CompareTo(chartEvent.StartTime);
    }
    public override string ToString()
    {
        return $"StartTime: {this.StartTime}, \n" +
            $"EndTime: {this.EndTime}, \n" +
            $"Speed: {this.Speed} \n";
    }
}
public class InternalMovementEvent : IComparable<IInternalEventFormat>, IInternalEventFormat
{
    /// <inheritdoc />
    public float StartTime { get; set; }
    /// <inheritdoc />
    public float EndTime { get; set; }
    /// <summary>
    /// x: 0 ~ 1, left to right
    /// y: 0 ~ 1, bottem to top
    /// z: 0 ~ 1, plane 0 ~ 1000
    /// </summary>
    public Vector3 MovementStart { get; set; }
    /// <summary>
    /// x: 0 ~ 1, left to right
    /// y: 0 ~ 1, bottem to top
    /// z: 0 ~ 1, plane 0 ~ 1000
    /// </summary>
    public Vector3 MovementEnd { get; set; }
    public bool HasSameTime(IInternalEventFormat compareTo)
    {
        if (this.StartTime == compareTo.StartTime && this.EndTime == compareTo.EndTime) return true;
        else return false;
    }
    public int CompareTo(IInternalEventFormat chartEvent)
    {
        return this.StartTime.CompareTo(chartEvent.StartTime);
    }
    public override string ToString()
    {
        return $"StartTime: {this.StartTime}, \n" +
            $"EndTime: {this.EndTime}, \n" +
            $"MovementStart: {this.MovementStart}, \n" +
            $"MovementEnd: {this.MovementEnd} \n";
    }
}
public class InternalRotationEvent : IComparable<IInternalEventFormat>, IInternalEventFormat
{
    /// <inheritdoc />
    public float StartTime { get; set; }
    /// <inheritdoc />
    public float EndTime { get; set; }
    /// <summary>
    /// just like normal euler degrees
    /// </summary>
    public Vector3 RotationStart { get; set; }
    /// <summary>
    /// just like normal euler degrees
    /// </summary>
    public Vector3 RotationEnd { get; set; }
    public bool HasSameTime(IInternalEventFormat compareTo)
    {
        if (this.StartTime == compareTo.StartTime && this.EndTime == compareTo.EndTime) return true;
        else return false;
    }
    public int CompareTo(IInternalEventFormat chartEvent)
    {
        return this.StartTime.CompareTo(chartEvent.StartTime);
    }
    public override string ToString()
    {
        return $"StartTime: {this.StartTime}, \n" +
            $"EndTime: {this.EndTime}, \n" +
            $"RotationStart: {this.RotationStart}, \n" +
            $"RotationEnd: {this.RotationEnd} \n";
    }
}
public class InternalOpacityEvent : IComparable<IInternalEventFormat>, IInternalEventFormat
{
    /// <inheritdoc />
    public float StartTime { get; set; }
    /// <inheritdoc />
    public float EndTime { get; set; }
    /// <summary>
    /// opacity, 0 ~ 1
    /// </summary>
    public float StartOpacity { get; set; } // 0 ~ 1
    /// <summary>
    /// opacity, 0 ~ 1
    /// </summary>
    public float EndOpacity { get; set; } // 0 ~ 1
    public bool HasSameTime(IInternalEventFormat compareTo)
    {
        if (this.StartTime == compareTo.StartTime && this.EndTime == compareTo.EndTime) return true;
        else return false;
    }
    public int CompareTo(IInternalEventFormat chartEvent)
    {
        return this.StartTime.CompareTo(chartEvent.StartTime);
    }
    public override string ToString()
    {
        return $"StartTime: {this.StartTime}, \n" +
            $"EndTime: {this.EndTime}, \n" +
            $"StartOpacity: {this.StartOpacity}, \n" +
            $"EndOpacity: {this.EndOpacity}, \n";
    }
}