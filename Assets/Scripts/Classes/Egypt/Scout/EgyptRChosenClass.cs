﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptRChosenClass : ClassNode
{
  public EgyptRChosenClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nBideWait";
  }

  public override string ClassName()
  {
      return "Ra's Chosen";
  }

  public override ClassNode GetParent(){
      return new EgyptWhispererClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptPChannelerClass(), new EgyptNSpeakerClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+2 hp battle";
  }


  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetHpBuffInactive(unit.GetHpBuff() + 2);
      return unit;
  }
}
