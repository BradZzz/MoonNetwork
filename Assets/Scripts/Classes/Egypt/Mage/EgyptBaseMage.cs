﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptBaseMage : ClassNode
{
  public EgyptBaseMage(){
    whenToUpgrade = 1;
  }

  public override string ClassDesc()
  {
    return "+1 atk\nAoeAtk";
  }

  public override string ClassName()
  {
      return "Mage";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptConjurerClass(), new EgyptDjinnClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
