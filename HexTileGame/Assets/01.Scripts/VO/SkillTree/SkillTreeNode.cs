using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillTreeNode
{
    [SerializeField]
    ResearchType type;
    public ResearchType Type
    {
        get { return type; }
    }

    [SerializeField]
    int researchThingIdx = 0;
    public int ResearchThingIdx
    {
        get { return researchThingIdx; }
    }

    [SerializeField]
    int idx = -1;
    public int Idx
    {
        get { return idx; }
    }

    [SerializeField]
    int turnForResearch = 0;
    public int TrunForResearch
    {
        get { return turnForResearch; }
    }

    [SerializeField]
    int researchResource = 0;
    public int ResearchResource
    {
        get { return researchResource; }
    }

    [SerializeField]
    int[] requireResearches;
    public int[] RequireResearches
    {
        get { return requireResearches; }
    }

    [SerializeField]
    string researchInfo = "";
    public string ResearchInfo
    {
        get { return researchInfo; }
    }
}

public enum ResearchType
{
    Warhead,
    Engine,
    Material
}
