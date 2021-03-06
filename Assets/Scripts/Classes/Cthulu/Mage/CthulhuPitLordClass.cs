﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuPitLordClass : ClassNode
{
  public CthulhuPitLordClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 atk rng\nFireKill";
  }

  public override string ClassName()
  {
      return "Pit Lord";
  }

  public override ClassNode GetParent(){
      return new CthulhuLesserDemonClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuJudasClass(), new CthulhuVirgilClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 atk range battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetAttackRngBuff(unit.GetAttackRngBuff() + 1);
      return unit;
  }
}
