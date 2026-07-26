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

public class KnightBot() : CustomMonsterModel
{
    public static Vector2 MinOffset => new Vector2(150f, -75f);

    public static Vector2 MaxOffset => new Vector2(250f, -75f);

    public static Vector2 ScaleRange => new Vector2(1f, 2f);

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

    public override string? CustomVisualPath => "res://TheToymaker/scenes/creature_visuals/knightbot.tscn";

}