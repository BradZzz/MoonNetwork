﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptBKoboldClass : ClassNode
{
  public EgyptBKoboldClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nThornDef";
  }

  public override string ClassName()
  {
      return "Blood Kobold";
  }

  public override ClassNode GetParent(){
      return new EgyptKoboldClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("ThornDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "ThornDef";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "ThornDef" });
      return unit;
  }
}
