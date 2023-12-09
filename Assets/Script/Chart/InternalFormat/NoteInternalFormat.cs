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
    // note size should be screen width / 7.2
    [JsonIgnore]
    public GameObject NoteObj { get; set; }

    [JsonIgnore]
    public NoteState State { get; private set; } = NoteState.Pending;
    /// <summary>
    /// basically time out, in ms
    /// </summary>
    [JsonIgnore]
    public bool IsVisible { get; private set; } = false;
    [JsonIgnore]
    public int? HitJudgeMS { get; private set; } = null;
    [JsonIgnore]
    public bool Suspended { get; private set; } = false;

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
    private SlicedFilledImage _imageComponent;
    public int CompareTo(NoteInternalFormat note)
    {
        return this.Id.CompareTo(note.Time);
    }
    public void Update(int timeMS, in int[] notePosArray, in float bpm, in Camera cam)
    {
        if (this.Suspended)
        {
            if (timeMS > Utils.ChartTimeToMS(bpm, this.HoldTime + this.Time))
            {
                return;
            }
            // below part: the time has changed(user change or smth) so its no longer suspended
            this.Suspended = false;
            this.NoteObj.SetActive(true);
        }

        this._posOnScreen.y = notePosArray[Math.Max(this.TimeMS - timeMS + 200, 0)] * this.Speed * (this.IsAbove ? 1f : -1f);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)ChartManager.Canvas.transform,
            this._posOnScreen,
            cam,
            out this._notePos
        );
        this.NoteObj.transform.position = this._notePos;
        if ((this.IsVisible && TimeMS > this.VisibleSinceTimeMS) || this._notePos.y > 0)
        {
            this._noteColor.a = 1;
            ImageUpdate();
        }
        else if (Utils.ChartTimeToMS(bpm, this.HoldTime + this.Time) - timeMS < 0)
        {
            this._noteColor.a = 0;
            this.Suspended = true;
            this.NoteObj.SetActive(false);
        }
    }
    public void ImageUpdate()
    {
        this._imageComponent.color = this._noteColor; // get/set is actually function so we cant just use ...ent.color.a = x
    }
    public void HoldImageUpdate()
    {

    }
    public void Initalize(float bpm, in int[] notePosArray, in JudgeLineInternalFormat parent)
    {
        this.TimeMS = (int)Utils.ChartTimeToMS(bpm, this.Time);
        this._posOnScreen = new Vector2(ChartManager.ChartRenderSize.width / 2 * this.PositionX, 0);
        this.NoteObj = GameObject.Instantiate(NoteTextureManager.NoteTemplate, parent.LineObj.transform);
        this.NoteObj.transform.eulerAngles = new Vector3(0, 0, this.IsAbove ? 0 : 180);
        this.NoteObj.transform.localScale = new Vector3(
            ChartManager.ChartRenderSize.width / 7.2f,
            ChartManager.ChartRenderSize.height / 7.2f,
            1
        ).Multiply(Config.Configuration.NoteScale);
        RectTransform trasform = this.NoteObj.GetComponent<RectTransform>();
        this._imageComponent = this.NoteObj.AddComponent<SlicedFilledImage>();
        this._imageComponent.sprite = NoteTextureManager.GetSpriteForNote(this);
        this._imageComponent.fillDirection = SlicedFilledImage.FillDirection.Down;
        if (this.HoldTime > 0)
        {
            trasform.sizeDelta = new Vector2(
                trasform.sizeDelta.x,
                (
                    notePosArray[(int)Utils.ChartTimeToMS(bpm, this.HoldTime + this.Time)]
                    - notePosArray[(int)Utils.ChartTimeToMS(bpm, this.Time)]
                ) / this.NoteObj.transform.localScale.y
            );
        }
        else
        {
            ;
        }
        this.State = NoteState.DoneLoading;
    }
}
