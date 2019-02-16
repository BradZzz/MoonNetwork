﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptGenieClass : ClassNode
{
  public EgyptGenieClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nFireMove";
  }

  public override string ClassName()
  {
      return "Genie";
  }

  public override ClassNode GetParent(){
      return new EgyptDjinnClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}