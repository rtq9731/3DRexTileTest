using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class MissileEngineData
{
  [SerializeField]
  short idx;
  public short Idx { get {return idx; } set { this.idx = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { this.name = value;} }
  
  [SerializeField]
  MissileTypes.MissileEngineType type;
  public MissileTypes.MissileEngineType TYPE { get {return type; } set { this.type = value;} }
  
  [SerializeField]
  int makingtime;
  public int Makingtime { get {return makingtime; } set { this.makingtime = value;} }
  
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