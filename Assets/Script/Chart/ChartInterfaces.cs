﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IOfficalChartEvents
{
    public Event ToInternalFormat(); // tbh idk why im doing this lmao
}
interface IOfficalNotes
{
    public Note ToInternalFormat();
}
interface IOfficalJudgeLines
{
    public JudgeLine ToInternalFormat(); 
}
interface IOfficalCharts
{
    public Chart ToInternalFormat();
}
