using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class MissileWarheadData
{
  [SerializeField]
  short idx;
  public short Idx { get {return idx; } set { this.idx = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { this.name = value;} }
  
  [SerializeField]
  MissileTypes.MissileWarheadType type;
  public MissileTypes.MissileWarheadType TYPE { get {return type; } set { this.type = value;} }
  
  [SerializeField]
  int makingtime;
  public int Makingtime { get {return makingtime; } set { this.makingtime = value;} }
  
  [SerializeField]
  int atk;
  public int Atk { get {return atk; } set { this.atk = value;} }
  
  [SerializeField]
  int price;
  public int Price { get {return price; } set { this.price = value;} }
  
  [SerializeField]
  int weight;
  public int Weight { get {return weight; } set { this.weight = value;} }
  
  [SerializeField]
  string info;
  public string Info { get {return info; } set { this.info = value;} }

  public Sprite mySprite;
}