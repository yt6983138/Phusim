using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IOfficalChartEvents
{

    /// <summary>
    /// 1st beat = 8, 2nd beat = 16 etc
    /// </summary>
    public float startTime { get; set; } // cant fix name violation bruh
    /// <summary>
    ///  1st beat = 8, 2nd beat = 16 etc
    /// </summary>
    public float endTime { get; set; }
    public IInternalEventFormat ToInternalFormat(); // tbh idk why im doing this lmao
}
public interface IOfficalNotes
{
    public Note ToInternalFormat(bool isAbove);
}
public interface IOfficalJudgeLines
{
    public JudgeLineInternalFormat ToInternalFormat(); 
}
public interface IOfficalCharts
{
    public Chart ToInternalFormat();
}

public interface IInternalEventFormat
{
    /// <summary>
    /// 1st beat = 8, 2nd beat = 16 etc
    /// </summary>
    public float StartTime { get; set; }
    /// <summary>
    ///  1st beat = 8, 2nd beat = 16 etc
    /// </summary>
    public float EndTime { get; set; }
}