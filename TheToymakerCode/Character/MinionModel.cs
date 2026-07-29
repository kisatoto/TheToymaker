using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Godot;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Rooms;

namespace TheToymaker.TheToymakerCode.Character;

public abstract class MinionModel() : CustomMonsterModel
{
    protected MinionModel(Creature creature, MonsterModel model, MinionType type) : this()
    {
        this.Creature = creature;
        this.MonsterModel = model;
        this.Type = type;
    }
    
    public bool isMinion = true;

    public MinionType Type = MinionType.Single;

    public Creature Creature
    {
        get;
    }
    
    public MonsterModel MonsterModel
    {
        get;
    }
    
    

    public override int MinInitialHp => 1;

    public override int MaxInitialHp => 10;
    
    // public override int 

    // public override bool IsHealthBarVisible => this.Creature.IsAlive;
    
    protected override MonsterMoveStateMachine GenerateMoveStateMachine()
    {
        MoveState initialState = new MoveState("NOTHING_MOVE", _ => Task.CompletedTask);
        initialState.FollowUpState = initialState;
        return new MonsterMoveStateMachine([initialState], initialState);
    }

}