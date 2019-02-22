﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealAlliesAtk : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.DidAttack: DidAttack(u1, u2); break;
          default: return;
      }
  }

  public override void BeginningGame(UnitProxy unit)
  {

  }

  public override void DidAttack(UnitProxy attacker, UnitProxy defender)
  {
      foreach(TileProxy tl in BoardProxy.instance.GetAllVisitableNodes(attacker, value + 1, true)){
          tl.FloatUp(Skill.Actions.DidAttack, "heal", Color.green, "Allies healed from another unit's attack");
          Debug.Log("HealAlliesAtk");
          if (tl.HasUnit()) {
            Debug.Log(tl.HasUnit().ToString());
            Debug.Log((tl.GetUnit().GetData().GetTeam() == attacker.GetData().GetTeam()).ToString());
            Debug.Log(tl != BoardProxy.instance.GetTileAtPosition(attacker.GetPosition()));
            if (tl.GetUnit().GetData().GetTeam() == attacker.GetData().GetTeam() 
              && tl != BoardProxy.instance.GetTileAtPosition(attacker.GetPosition())) {
                Debug.Log("Attempting to heal unit");
                tl.GetUnit().HealUnit(1, Skill.Actions.DidAttack);
            }
          }
      }
  }

  public override void DidKill(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override void DidMove(UnitProxy unit, List<TileProxy> path){

  }

  public override void DidWait(UnitProxy unit)
  {

  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override string PrintDetails(){
      return "HealAlliesAtk";
  }
}
