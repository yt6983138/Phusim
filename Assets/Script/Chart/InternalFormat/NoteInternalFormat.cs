using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class NoteInternalFormat : IComparable<NoteInternalFormat>
{
    [JsonIgnore]
    public GameObject NoteObj { get; set; }

    [JsonIgnore]
    public NoteState State { get; private set; } = NoteState.Pending;
    /// <summary>
    /// basically time out, in ms
    /// </summary>
    [JsonIgnore]
    public float HitTime { get; set; }
    [JsonIgnore]
    public bool IsVisible { get; private set; } = false;
    [JsonIgnore]
    public int? HitJudgeMS { get; private set; } = null;

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
    /// speed, this * judgeline speed = real speed, does not affect floorPos <br/>
    /// todo: add defintion
    /// </summary>
    public float Speed { get; set; }
    /// <summary>
    /// ok lets change the define-- <br/>
    /// -1 ~ 1
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
    public int VisibleSinceTimeMS { get; set; }

    // private int PosOnScreen;
    private Vector3 _notePos;
    private int TimeMS;
    private Color _noteColor;
    private Vector2 _posOnScreen;
    public int CompareTo(NoteInternalFormat note)
    {
        return this.Id.CompareTo(note.Time);
    }
    public void Update(int timeMS, in int[] notePosArray, in Camera cam)
    {
        this._posOnScreen.y = notePosArray[Math.Max(this.TimeMS - timeMS + 200, 0)] * this.Speed;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)ChartManager.Canvas.transform,
            this._posOnScreen,
            cam,
            out this._notePos
        );
        this.NoteObj.transform.position = this._notePos;
        if (!this.IsVisible && TimeMS > this.VisibleSinceTimeMS)
        { //   ^ to prevent use getcomponent every update
            this._noteColor.a = 1;
            this.NoteObj.GetComponent<Image>().color = this._noteColor;
            this.IsVisible = true;
        }
    }
    public void Initalize(float bpm, int chartWidth, in JudgeLineInternalFormat parent)
    {
        this.TimeMS = (int)StaticUtils.ChartTimeToMS(bpm, this.Time);
        _posOnScreen = new Vector2(chartWidth / 2 * this.PositionX, 0);
        this.NoteObj = GameObject.Instantiate(NoteTextureManager.NoteTemplate, parent.LineObj.transform);
        Image component = this.NoteObj.AddComponent<Image>();
        component.sprite = NoteTextureManager.GetSpriteForNote(this);
        this.State = NoteState.DoneLoading;
    }
}
