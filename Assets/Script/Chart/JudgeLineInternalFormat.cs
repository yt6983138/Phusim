using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class JudgeLineInternalFormat
{
    // migrating smth
    [JsonIgnore]
    private long maxTime;
    [JsonIgnore]
    private int[] Counter;
    [JsonIgnore]
    private bool hasComputed = false;
    // migrating end

    public int Id { get; set; }
    public float BPM { get; set; }
    public List<Note> Notes { get; set; }
    public List<Event> Events { get; set; }
    public bool UseOverrideTexture { get; set; }

    /// <summary>
    /// why byte: 0~255 is enough except u have a 10 bit screen(i think)<br/>
    /// 0: fully transparent, 255: solid
    /// </summary>
    [JsonIgnore]
    public byte[] OpacityArray { get; private set; }
    /// <summary>
    /// why short: -32768 ~ 32767 is enough for rotation<br/>
    /// u can understand x, y ,z right? 0~360 euler degrees
    /// </summary>
    [JsonIgnore]
    public (short x, short y, short z)[] RotationArray { get; private set; }
    /// <summary>
    /// its position, 0 ~ 1(0 ~ window right)
    /// </summary>
    [JsonIgnore]
    public (float x, float y, float z)[] PositionArray { get; private set; }
    /// <summary>
    /// todo: add comment
    /// </summary>
    [JsonIgnore]
    public int[] NotePositionArray { get; private set; }

    [JsonIgnore]
    public List<GameObject> NoteObjects { get; private set; } = new();
    [JsonIgnore]
    private float LastSpeedEventEndPos = 0;
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
    public void InitializeNotes()
    {
        foreach (Note note in this.Notes)
        {
            GameObject noteObj = GameObject.Instantiate(NoteTextureManager.NoteTemplate, ChartManager.JudgeLineManager.JudgeLinesObject[this.Id].transform);
            Vector3 pos = Vector2.zero;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                noteObj.GetComponent<RectTransform>(),
                new Vector2(note.PositionX * 0.1f * ChartManager.ChartWindowSize.width, note.PositionY * note.Speed * ChartManager.ChartWindowSize.height * 0.5f),
                ChartManager.Cam,
                out pos
            );
            noteObj.transform.position = pos;
        }
    }
    public void BuildEventArrays(int? chartLength = null)
    {
        int length = chartLength ?? ChartManager.EndTimeMs;
        this.OpacityArray = new byte[length];
        this.PositionArray = new (float x, float y, float z)[length];
        this.RotationArray = new (short x, short y, short z)[length];
        this.NotePositionArray = new int[length];
        foreach (Event chartEvent in this.Events)
        {
            int startTime = (int)StaticUtils.ChartTimeToMS(chartEvent.StartTime, this.BPM);
            int endTime = (int)StaticUtils.ChartTimeToMS(chartEvent.EndTime, this.BPM);

            switch (chartEvent.EventType)
            {
                case JudgeLineEventType.SpeedEvent:
                    break;
                case JudgeLineEventType.MoveEvent:
                    for (int i = startTime; i <= Math.Min(endTime, length); i++)
                    {
                        this.PositionArray[i].x = StaticUtils.GetValueFromLinearFunction(chartEvent.MovementStart.x, chartEvent.MovementEnd.x, endTime - startTime, i);
                        this.PositionArray[i].y = StaticUtils.GetValueFromLinearFunction(chartEvent.MovementStart.y, chartEvent.MovementEnd.y, endTime - startTime, i);
                        this.PositionArray[i].z = StaticUtils.GetValueFromLinearFunction(chartEvent.MovementStart.z, chartEvent.MovementEnd.z, endTime - startTime, i);
                    }
                    break;
                case JudgeLineEventType.RotateEvent:
                    for (int i = startTime; i <= Math.Min(endTime, length); i++)
                    {
                        this.RotationArray[i].x = (short)(StaticUtils.GetValueFromLinearFunction(chartEvent.RotationStart.x, chartEvent.RotationEnd.x, endTime - startTime, i) * 360);
                        this.RotationArray[i].y = (short)(StaticUtils.GetValueFromLinearFunction(chartEvent.RotationStart.y, chartEvent.RotationEnd.y, endTime - startTime, i) * 360);
                        this.RotationArray[i].z = (short)(StaticUtils.GetValueFromLinearFunction(chartEvent.RotationStart.z, chartEvent.RotationEnd.z, endTime - startTime, i) * 360);
                    }
                    break;
                case JudgeLineEventType.OpacityEvent:
                    for (int i = startTime; i <= Math.Min(endTime, length); i++)
                    {
                        this.OpacityArray[i] = (byte)(Math.Min(StaticUtils.GetValueFromLinearFunction(chartEvent.StartOpacity, chartEvent.EndOpacity, endTime - startTime, i) * 255, 255));
                    }
                    break;
                case JudgeLineEventType.UnsetEvent:
                    goto default;
                default:
                    LogHandler.Log(LogHandler.Warning, $"Found invaild event: {chartEvent.ToString()}");
                    break;
            }
        }
    }
}
