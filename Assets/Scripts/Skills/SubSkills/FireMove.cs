﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireMove : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.DidMove: DidMove(u1, path); break;
          default: return;
      }
  }

  public override void BeginningGame(UnitProxy unit)
  {

  }

  public override void DidAttack(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override void DidDefend(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override void DidKill(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override void DidMove(UnitProxy unit, List<TileProxy> path){
      path.RemoveAt(0);
      foreach(TileProxy tile in path) {
          tile.SetTurnsOnFire(2);
      }
  }

  public override void DidWait(UnitProxy unit)
  {

  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override void WasAttacked(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override string PrintDetails(){
      return "FireMove";
  }
}
