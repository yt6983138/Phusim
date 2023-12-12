using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class JudgeLineInternalFormat : IComparable<JudgeLineInternalFormat>
{
    // migrating smth
    [JsonIgnore]
    private long _maxTime;
    [JsonIgnore]
    private int[] _multipressCounter;
    [JsonIgnore]
    private bool _hasComputed = false;
    // migrating end
    [JsonIgnore]
    private const float _zPlaneLength = 1000f;

    public int Id { get; set; }
    public float BPM { get; set; }
    public List<NoteInternalFormat> Notes { get; set; }
    public List<IInternalEventFormat> Events { get; set; }
    public bool UseOverrideTexture { get; set; }

    /// <summary>
    /// why byte: 0~255 is enough except u have a 10 bit screen(i think)<br/>
    /// 0: fully transparent, 255: solid
    /// </summary>
    [JsonIgnore]
    public byte[] OpacityArray { get; private set; }
    /// <summary>
    /// u can understand x, y ,z right? 0~360 euler degrees
    /// x: vertical
    /// y: horzonal 
    /// z: plane
    /// </summary>
    [JsonIgnore]
    public Vector3[] RotationArray { get; private set; }
    /// <summary>
    /// its position, 0 ~ window right
    /// </summary>
    [JsonIgnore]
    public Vector3[] PositionArray { get; private set; }
    /// <summary>
    /// todo: add comment
    /// </summary>
    [JsonIgnore]
    public int[] NotePositionArray { get; private set; }
    /// <summary>
    /// should be cleaned after use
    /// </summary>
    [JsonIgnore]
    public GameObject LineObj { get; set; }
    [JsonIgnore]
    private Image _component;
    [JsonIgnore]
    private Color _lineColor;
    public void Initalize(in GameObject canva)
    {
        this.LineObj = GameObject.Instantiate(JudgeLineTextureManager.LineTemplate, canva.transform);
        this._component = this.LineObj.AddComponent<Image>();
        this._component.sprite = JudgeLineTextureManager.GetSpriteForLine(this);
        this._lineColor = new Color(1, 1, 1, 1);
        Notes.Sort();
        Events.Sort();
        AssignIds();
        ComputeMultiPress();
        foreach (NoteInternalFormat note in this.Notes)
        {
            note.MultiPress = _multipressCounter[note.Time] > 1; // check multi press
            note.Initalize(this.BPM, this.NotePositionArray, this);
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
        if (_hasComputed) { return; }
        _maxTime = Notes.Last().Time;
        _multipressCounter = new int[_maxTime];
        foreach (NoteInternalFormat note in Notes)
        {
            _multipressCounter[note.Time]++;
        }
        _hasComputed = true;
    }
    public void BuildEventArrays(int? chartLengthMS = null)
    {
        int length = chartLengthMS ?? ChartManager.EndTimeMs;
        this.OpacityArray = new byte[length];
        this.RotationArray = new Vector3[length];
        this.PositionArray = new Vector3[length];
        this.NotePositionArray = new int[length + 200]; // for animation
        float[] SpeedArray = new float[length];
        // this.LastSpeedEvent = new()
        // {
        //     StartTime = 0,
        //     EndTime = length,
        //     Speed = 1
        // };
        (float width, float height) = ChartManager.ChartRenderSize;
        List<InternalSpeedEvent> speedEvents = new();
        foreach (IInternalEventFormat chartEvent in this.Events)
        {
            int startTime = (int)Utils.ChartTimeToMS(chartEvent.StartTime, this.BPM);
            int endTime = (int)Utils.ChartTimeToMS(chartEvent.EndTime, this.BPM);

            switch (chartEvent)
            {
                case InternalSpeedEvent @event:
                    speedEvents.Add(@event);
                    for (int i = startTime; i <= Math.Min(endTime, length); i++)
                    {
                        SpeedArray[i] = @event.Speed;
                    }
                    //this.LastSpeedEvent = chartEvent;
                    break;
                case InternalMovementEvent @event:
                    for (int i = startTime; i <= Math.Min(endTime, length); i++)
                    {
                        this.PositionArray[i].x = Utils.GetValueFromLinearFunction(@event.MovementStart.x, @event.MovementEnd.x, endTime - startTime, i) * width;
                        this.PositionArray[i].y = Utils.GetValueFromLinearFunction(@event.MovementStart.y, @event.MovementEnd.y, endTime - startTime, i) * height;
                        this.PositionArray[i].z = Utils.GetValueFromLinearFunction(@event.MovementStart.z, @event.MovementEnd.z, endTime - startTime, i) * _zPlaneLength;
                    }
                    break;
                case InternalRotationEvent @event:
                    for (int i = startTime; i <= Math.Min(endTime, length); i++)
                    {
                        this.RotationArray[i].x = Utils.GetValueFromLinearFunction(@event.RotationStart.x, @event.RotationEnd.x, endTime - startTime, i) * 360;
                        this.RotationArray[i].y = Utils.GetValueFromLinearFunction(@event.RotationStart.y, @event.RotationEnd.y, endTime - startTime, i) * 360;
                        this.RotationArray[i].z = Utils.GetValueFromLinearFunction(@event.RotationStart.z, @event.RotationEnd.z, endTime - startTime, i) * 360;
                    }
                    break;
                case InternalOpacityEvent @event:
                    for (int i = startTime; i <= Math.Min(endTime, length); i++)
                    {
                        this.OpacityArray[i] = (byte)(Math.Min(Utils.GetValueFromLinearFunction(@event.StartOpacity, @event.EndOpacity, endTime - startTime, i) * 255, 255));
                    }
                    break;
                default:
                    LogHandler.Log(LogHandler.Warning, $"Found invaild/undefined event: {chartEvent}");
                    break;
            }
        }
        float sum = 0;
        for (int i = 0; i < this.NotePositionArray.Length - 200; i++)
        {
            this.NotePositionArray[i + 200] = (int)sum;
            sum += SpeedArray[i] * height * 1000;
        }
        sum = 0;
        for (int i = 199; i >= 0; i--)
        {
            sum -= SpeedArray[0] * height * 1000; // late judge animation purpose
            this.NotePositionArray[i] = (int)sum;
        }
    }
    public async void Update(int currentMS)
    {
        this.LineObj.transform.position = this.PositionArray[currentMS];
        this.LineObj.transform.eulerAngles = this.RotationArray[currentMS];
        this._lineColor.a = 0.003921569f * this.OpacityArray[currentMS];
        this._component.color = this._lineColor;
        await Task.Run(() =>
        {
            foreach (NoteInternalFormat note in this.Notes)
            {
                note.Update(currentMS, this.NotePositionArray, this.BPM, ChartManager.Cam); 
            }
        });
    }
    public int CompareTo(JudgeLineInternalFormat line)
    {
        return this.Notes.Count.CompareTo(line.Notes.Count);
    }
}
