﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractUnitSelected : InteractMode
{
    private List<TileProxy> allTiles = new List<TileProxy>();
    private List<TileProxy> visitableTiles = new List<TileProxy>();
    private List<TileProxy> attackableTiles = new List<TileProxy>();

    private UnitProxy currentUnit;
    public override void OnTileSelected(TileProxy tile)
    {
        if (currentUnit != null && visitableTiles.Contains(tile))
        {
            TileProxy startTile = BoardProxy.instance.GetTileAtPosition(currentUnit.GetPosition());
            if (startTile != tile) {
                UnitProxy unit = startTile.GetUnit();
                if (unit.GetData().GetTurnActions().CanMove())
                {
                    unit.GetData().GetTurnActions().Move();
                    PanelControllerNew.SwitchChar(unit);
                    StartCoroutine(currentUnit.CreatePathToTileAndLerpToPosition(tile,
                    () =>
                    {
                        tile.ReceiveGridObjectProxy(currentUnit);
                        startTile.RemoveGridObjectProxy(currentUnit);
                        UnHighlightTiles();
                        InteractivityManager.instance.EnterDefaultMode();
                    }));
                }
                else
                {
                    Debug.Log("Out of actions. Send signal to player they can't move unit.");
                }
            }
            else
            {
                StartCoroutine(currentUnit.CreatePathToTileAndLerpToPosition(tile,
                () =>
                {
                    StartCoroutine(ResetTiles());
                }));
            }
        }
        else if (currentUnit != null)
        {
            //Select all the tiles with opp team in all tiles
            //List<TileProxy> visitableTiles = allTiles.Where(tl => tl.GetUnit().GetData().GetTeam() != currentUnit.GetData().GetTeam()).ToList<TileProxy>();
            if (!allTiles.Where(tl => tl.HasUnit() && (tl.GetUnit().GetData().GetTeam() != currentUnit.GetData().GetTeam())).ToList<TileProxy>().Contains(tile))
            {
                //BoardProxy.instance.FlushTiles();
                //PanelControllerNew.SwitchChar(null);

                StartCoroutine(ResetTiles());
            }
            ////Player clicked out of unit range, reset tiles/UI so player can click somewhere else instead
            //BoardProxy.instance.FlushTiles();
            //PanelController.SwitchChar(null);
        }
  }

  IEnumerator ResetTiles()
  {
      UnHighlightTiles();
      PanelControllerNew.SwitchChar(null);
      InteractivityManager.instance.EnterDefaultMode();
      yield return null;
  }

  //public void ResetAtThis()
  //{
  //    StartCoroutine(ResetTiles());
  //}

  public override void OnUnitSelected(UnitProxy obj)
    {
        if (currentUnit == null)
        {
            UnHighlightTiles();
            currentUnit = obj;
            //The maximum range in which a player has actions
            allTiles = BoardProxy.instance.GetAllVisitableNodes(obj, obj.GetMoveSpeed() > obj.GetAttackRange() ? obj.GetMoveSpeed() : obj.GetAttackRange(), true);
            //The attackable tiles
            attackableTiles = BoardProxy.instance.GetAllVisitableNodes(obj, obj.GetAttackRange(), true);
            //The visitable tiles
            visitableTiles = BoardProxy.instance.GetAllVisitableNodes(obj, obj.GetMoveSpeed());
            HighlightTiles(obj);
            PanelControllerNew.SwitchChar(obj);
        }
        else
        {
            if (obj.GetData().GetTeam() != currentUnit.GetData().GetTeam() 
              && allTiles.Contains(BoardProxy.instance.GetTileAtPosition(obj.GetPosition()))
              && currentUnit.GetData().GetTurnActions().CanAttack())
            {
                if (obj.IsAttacked(currentUnit))
                {
                    //If the unit has died, remove it from the board and destroy the gameobject
                    //BoardProxy.instance.GetTileAtPosition(obj.GetPosition()).RemoveGridObjectProxy(obj);
                    //Destroy(obj.gameObject);
                    currentUnit.AcceptAction(Skill.Actions.DidKill,obj);

                    ConditionTracker.instance.EvalDeath(obj);                     

                    //Turn off the tiles
                    StartCoroutine(ResetTiles());
                }

                //obj.AcceptAction(Skill.Actions.WasAttacked,currentUnit);
                if (currentUnit != null) {
                    currentUnit.AcceptAction(Skill.Actions.DidAttack,obj);
                }

                OnDisable();
                PanelControllerNew.SwitchChar(currentUnit);
            }
        }
    }

    private void HighlightTiles(UnitProxy obj)
    {
        foreach (var tile in allTiles)
        {
            tile.HighlightSelectedAdv(obj, visitableTiles, attackableTiles);
        }
    }

    private void UnHighlightTiles()
    {
        foreach (var tile in allTiles)
        {
            tile.UnHighlight();
        }
    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {
        UnHighlightTiles();
        currentUnit = null;
    }

    public override void OnTileHovered(TileProxy tile)
    {

    }

    public override void OnTileUnHovered(TileProxy tile)
    {

    }



    public void Update()
    {
        if (Input.GetMouseButton(1))//right click to exit
        {
            InteractivityManager.instance.EnterDefaultMode();
        }
    }

    public override void OnClear(TileProxy tile)
    {
        tile.UnHighlight();
        currentUnit = null;
    }
}
