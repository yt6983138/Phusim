﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    /// 0 ~ 1, left to right
    /// </summary>
    public float StartX { get; set; } 
    /// <summary>
    /// 0 ~ 1, down to up
    /// </summary>
    public float StartY { get; set; }
    /// <summary>
    /// 0 ~ 1, left to right
    /// </summary>
    public float EndX { get; set; }
    /// <summary>
    /// 0 ~ 1, down to up
    /// </summary>
    public float EndY { get; set; }
    /// <summary>
    /// just like normal euler degrees
    /// </summary>
    public float StartRotate { get; set; }
    /// <summary>
    /// just like normal euler degrees
    /// </summary>
    public float EndRotate { get; set; }
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
}