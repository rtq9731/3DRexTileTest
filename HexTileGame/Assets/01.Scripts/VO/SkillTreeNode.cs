using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillTreeNode
{
    [SerializeField]
    ResearchType type;

    [SerializeField]
    int resarchThingIdx = 0;

    [SerializeField]
    int idx = -1;

    [SerializeField]
    int turnForResearch = 0;

    [SerializeField]
    int researchResource = 0;

    [SerializeField]
    int[] requireResearches;

    [SerializeField]
    string researchInfo = "";

    public SkillTreeNode()
    {

    }
}

enum ResearchType
{
    Warhead,
    Engine,
    Material
}
