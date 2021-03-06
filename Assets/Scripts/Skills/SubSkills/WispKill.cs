﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WispKill : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.DidKill: DidKill(u1, u2); break;
          default: return;
      }
  }

  public override void BeginningGame(UnitProxy unit)
  {

  }

  public override void DidAttack(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override void DidKill(UnitProxy attacker, UnitProxy defender)
  {
      //BoardProxy.instance.GetTileAtPosition(defender.GetPosition()).FloatUp(Skill.Actions.DidKill, "skele summon", Color.green, "Skeleton Summon");
      BoardProxy.instance.SummonAtPosition(defender.GetPosition(),attacker.GetData().GetTeam(), value);
  }

  public override void DidMove(UnitProxy unit, List<TileProxy> path){

  }

  public override void DidWait(UnitProxy unit)
  {

  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Utility };
  }

  public override string PrintDetails(){
      return "Summon wisp on enemy unit kill. " + ReturnBlurbByString(GetSkillGen());
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.skele);
  }

  public override SkillGen GetSkillGen()
  {
      return SkillGen.WispKill;
  }
}
