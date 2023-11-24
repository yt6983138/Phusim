using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Event : IComparable<Event>
{
    /// <summary>
    /// 1st beat = 8, 2nd beat = 16 etc
    /// </summary>
    public float StartTime {  get; set; } 
    /// <summary>
    ///  1st beat = 8, 2nd beat = 16 etc
    /// </summary>
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
    /// <summary>
    /// just like normal euler degrees
    /// </summary>
    public Vector3 RotationStart { get; set; }
    /// <summary>
    /// just like normal euler degrees
    /// </summary>
    public Vector3 RotationEnd { get; set; }
    /// <summary>
    /// idk how does this work
    /// </summary>
    public float Speed { get; set; } 
    /// <summary>
    /// opacity, 0 ~ 1
    /// </summary>
    public float StartOpacity { get; set; } // 0 ~ 1
    /// <summary>
    /// opacity, 0 ~ 1
    /// </summary>
    public float EndOpacity { get; set; } // 0 ~ 1
    public JudgeLineEventType EventType { get; set; } = JudgeLineEventType.UnsetEvent;
    public bool HasSameTime(Event compareTo)
    {
        if (this.StartTime == compareTo.StartTime && this.EndTime == compareTo.EndTime) return true;
        else return false;
    }
    public int CompareTo(Event chartEvent)
    {
        return this.StartTime.CompareTo(chartEvent.StartTime);
    }
    public override string ToString()
    {
        return $"StartTime: {this.StartTime}, \n" +
            $"EndTime: {this.EndTime}, \n" +
            $"MovementStart: {this.MovementStart}, \n" +
            $"MovementEnd: {this.MovementEnd}, \n" +
            $"RotationStart: {this.RotationStart}, \n" +
            $"RotationEnd: {this.RotationEnd}, \n" +
            $"Speed: {this.Speed}, \n" +
            $"StartOpacity: {this.StartOpacity}, \n" +
            $"EndOpacity: {this.EndOpacity}, \n" +
            $"EventType: {this.EventType}\n";
    }
}
