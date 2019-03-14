﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Skill
{
    public int value;

    public abstract void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path);
    //public abstract void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path);
  
    //Implemented
    public abstract void BeginningGame(UnitProxy unit);
    public abstract void EndTurn(UnitProxy unit);
    public abstract void DidMove(UnitProxy unit, List<TileProxy> path);
    public abstract void DidWait(UnitProxy unit);
    public abstract void DidAttack(UnitProxy attacker, UnitProxy defender);
    public abstract void DidKill(UnitProxy attacker, UnitProxy defender);

    public abstract string PrintDetails();
    public abstract string PrintStackDetails();
    public abstract SkillTypes[] GetSkillTypes();

    public static Skill ReturnSkillByString(SkillClasses sClass){
        switch(sClass){
            case SkillClasses.AegisAlliesAtk: return new AegisAlliesAtk();
            case SkillClasses.AegisAtk: return new AegisAtk();
            case SkillClasses.AegisBegin: return new AegisBegin();
            case SkillClasses.AegisKill: return new AegisKill();
            case SkillClasses.AegisTurn: return new AegisTurn();
            case SkillClasses.AegisWait: return new AegisWait();
            case SkillClasses.AoeAtk: return new AoeAtk();
            case SkillClasses.BideAlliesWait: return new BideAlliesWait();
            case SkillClasses.BideKill: return new BideKill();
            case SkillClasses.BideWait: return new BideWait();
            case SkillClasses.DivineMove: return new DivineMove();
            case SkillClasses.EnfeebleAtk: return new EnfeebleAtk();
            case SkillClasses.EnfeebleEnemiesWait: return new EnfeebleEnemiesWait();
            case SkillClasses.FireAtk: return new FireAtk();
            case SkillClasses.FireDef: return new FireDef();
            case SkillClasses.FireKill: return new FireKill();
            case SkillClasses.FireMove: return new FireMove();
            case SkillClasses.ForceAtk: return new ForceAtk();  
            case SkillClasses.HealAlliesAtk: return new HealAlliesAtk(); 
            case SkillClasses.HealAlliesWait: return new HealAlliesWait(); 
            case SkillClasses.HealAtk: return new HealAtk(); 
            case SkillClasses.HealKill: return new HealKill();
            case SkillClasses.HealTurn: return new HealTurn();
            case SkillClasses.HealWait: return new HealWait();
            case SkillClasses.HobbleAtk: return new HobbleAtk();
            case SkillClasses.NullifyAtk: return new NullifyAtk();
            case SkillClasses.NullifyEnemiesWait: return new NullifyEnemiesWait();
            case SkillClasses.QuickenAlliesAtk: return new QuickenAlliesAtk();
            case SkillClasses.QuickenAlliesWait: return new QuickenAlliesWait();
            case SkillClasses.RageAlliesWait: return new RageAlliesWait();
            case SkillClasses.RageAtk: return new RageAtk();
            case SkillClasses.RageDef: return new RageDef();
            case SkillClasses.RageKill: return new RageKill();
            case SkillClasses.RageMove: return new RageMove();
            case SkillClasses.RageWait: return new RageWait();
            case SkillClasses.RootAtk: return new RootAtk();
            case SkillClasses.RootEnemiesWait: return new RootEnemiesWait();
            case SkillClasses.SicklyAtk: return new SicklyAtk();
            case SkillClasses.SkeleKill: return new SkeleKill();
            case SkillClasses.SnowAtk: return new SnowAtk();
            case SkillClasses.SnowMove: return new SnowMove();
            case SkillClasses.ThornDef: return new ThornDef();       
            case SkillClasses.VoidAtk: return new VoidAtk();  
            case SkillClasses.WallMove: return new WallMove();
            case SkillClasses.WarpAtk: return new WarpAtk();       
            default: return null;
        }
    }

    public static string ReturnBlurbByString(SkillGen sGen){
        switch(sGen){
            case SkillGen.Aegis:return "An (aegis) shield blocks a unit's next incoming attack.";
            case SkillGen.Bide:return "(Bide) raises the max health of effected units.";
            case SkillGen.Divine:return "A tile with (divine) will heal a unit for +1 at the end of the turn.";
            case SkillGen.Enfeeble:return "An (enfeebled) unit has 1 less attack on it's next turn.";
            case SkillGen.Fire:return "A tile on (fire) burns any unit on it for 1 damage a turn.";
            case SkillGen.Force:return "An ability with (force) pushes an enemy away from the unit.";
            case SkillGen.Heal:return "An ability with (heal) restores a unit's lost hit points.";
            case SkillGen.Hobble:return "(Hobble) lowers the attack of effected units down to 1.";
            case SkillGen.Nullify:return "(Nullify) prevents a unit's skills from activating for a turn.";
            case SkillGen.Quicken:return "(Quicken) gives a unit another movement this turn.";
            case SkillGen.Rage:return "(Rage) raises the attack of effected units.";
            case SkillGen.Root:return "A (root)ed unit loses 1 turn movement on their next turn.";
            case SkillGen.Sickly:return "(Sickly) lowers the max hp of effected units down to 1.";
            case SkillGen.SkeleKill:return "A (skele) ability summons a friendly skeleton unit when used.";
            case SkillGen.Snow:return "A tile with (snow) enfeebles and roots any unit stuck in it at the end of the turn.";
            case SkillGen.Thorn:return "(Thorn) damages enemy units in range by 1 when activated.";
            case SkillGen.Void:return "An ability with (void) pulls an enemy towards the unit.";
            case SkillGen.Wait:return "A unit successfully (wait)s when they end the turn with at least one attack and move left.";
            case SkillGen.Wall:return "A (tile) with wall will spawn an obstacle.";
            case SkillGen.Warp:return "An ability with (warp) teleports an enemy to a random open space away from the unit.";
            default: return "";
        }
    }

    public static string ReturnStackTypeByString(SkillStack sGen){
        switch(sGen){
            case SkillStack.rng:return "Multiple stacks increase range of effect by 1.";
            case SkillStack.buff:return "Multiple stacks increase buff by 1.";
            case SkillStack.turns:return "Multiple stacks increase turns of effect.";
            case SkillStack.nostack:return "Effect does not stack.";
            default: return "";
        }
    }

    public enum Actions{
        BeginGame, EndedTurn, DidAttack, DidMove, DidWait, DidKill, DidDefend, None
    }

    public enum SkillGen{
        Aegis, Bide, Divine, Enfeeble, Fire, Force, Heal, Hobble, Nullify, Quicken, Rage, Root, Sickly, SkeleKill, Snow, Thorn, Void, Wait, Wall, Warp, None
    }

    public enum SkillStack{
        rng, buff, turns, nostack, None
    }

    public enum SkillClasses{
        AegisAlliesAtk, AegisAtk, AegisBegin, AegisKill, AegisTurn, AegisWait, AoeAtk, BideAlliesWait, BideKill, BideWait, DivineMove,
        EnfeebleAtk, EnfeebleEnemiesWait, FireAtk, FireDef, FireKill, FireMove, ForceAtk, 
        HealAtk, HealAlliesAtk, HealAlliesWait, HealKill, HealTurn, HealWait, HobbleAtk, NullifyAtk, NullifyEnemiesWait, QuickenAlliesAtk, QuickenAlliesWait,
        RageAlliesWait, RageAtk, RageDef, RageKill, RageMove, RageWait, RootAtk, RootEnemiesWait, SicklyAtk, SkeleKill, SnowAtk, SnowMove, ThornDef, VoidAtk, 
        WallMove, WarpAtk, None
    }

    public enum SkillTypes{
        Offense, Defense, Utility, None
    }
}
