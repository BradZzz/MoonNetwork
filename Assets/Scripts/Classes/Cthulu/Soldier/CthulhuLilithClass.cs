﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuLilithClass : ClassNode
{
  public CthulhuLilithClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nRageKill";
  }

  public override string ClassName()
  {
      return "Lilith";
  }

  public override ClassNode GetParent(){
      return new CthulhuSuccubusClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}