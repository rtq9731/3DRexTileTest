using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class MaterialData
{
  [SerializeField]
  int idx;
  public int Idx { get {return idx; } set { this.idx = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { this.name = value;} }
  
  [SerializeField]
  MissileTypes.MissileMaterial type;
  public MissileTypes.MissileMaterial TYPE { get {return type; } set { this.type = value;} }
  
  [SerializeField]
  int makingtime;
  public int Makingtime { get {return makingtime; } set { this.makingtime = value;} }
  
  [SerializeField]
  int morerange;
  public int Morerange { get {return morerange; } set { this.morerange = value;} }
  
  [SerializeField]
  string info;
  public string Info { get {return info; } set { this.info = value;} }
  
}