﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanPKnightClass : ClassNode
{
  public HumanPKnightClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "RageMove";
  }

  public override string ClassName()
  {
      return "Prism Knight";
  }

  public override ClassNode GetParent(){
      return new HumanCSergeantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}