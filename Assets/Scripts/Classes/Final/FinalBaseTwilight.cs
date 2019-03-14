﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseTwilight : ClassNode
{
  public FinalBaseTwilight(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+2hp\nRageAtk";
  }

  public override string ClassName()
  {
      return "Twilight";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new FinalBaseEvening() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
