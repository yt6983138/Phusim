using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class JudgeLineManager
{
    public GameObject Parent;
    public List<JudgeLineInternalFormat> JudgeLines;
    public GameObject JudgeLineTemplate;
    public List<GameObject> JudgeLinesObject { get; private set; } = new();

    public void CreateAllLines()
    {
        foreach (JudgeLineInternalFormat line in JudgeLines)
        {
            JudgeLinesObject[line.Id] = GameObject.Instantiate(JudgeLineTemplate, Parent.transform);
        }
    }
    public void AddNotesToLine(int lineId, Note note)
    {
        // GameObject.Instantiate()
        throw new NotImplementedException();
    }
}
