﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuBaseMage : ClassNode
{
  public CthulhuBaseMage(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+1 mv trn\n+1 atk rng";
  }

  public override string ClassName()
  {
      return "Mage";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuLesserDemonClass(), new CthulhuNecromancerClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 exp inactive";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetInactiveExpBuff(unit.GetInactiveExpBuff() + 1);
      return unit;
  }
}
