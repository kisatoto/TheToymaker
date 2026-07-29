using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using TheToymaker.TheToymakerCode.Character;
using TheToymaker.TheToymakerCode.Powers;

namespace TheToymaker.TheToymakerCode;

public class MinionCmd
{
    public static async Task<SummonResult> Summon<T>(
    Player summoner,
    decimal amount
    ) where T : MinionModel
    {
      ArgumentNullException.ThrowIfNull(summoner.Creature.CombatState);
      if (ModelDb.Monster<T>().Type == MinionType.Single)
      {
        if (summoner.Creature.CombatState.Creatures.Any(c => c is { Monster: T, IsAlive: true }))
        {
          var minion = summoner.Creature.CombatState.Creatures.First(c => c is { Monster: T, IsAlive: true });
          await CreatureCmd.GainMaxHp(minion,amount);
          await PowerCmd.Apply<ConstructPower>(new BlockingPlayerChoiceContext(), minion, summoner.Creature.CombatState.Allies.Count(creature => creature.IsPet && creature.PetOwner == summoner)+1M, summoner.Creature, null);
          return new SummonResult(minion, amount);
        }
      }

      var instance = NCombatRoom.Instance ?? throw new InvalidOperationException();
      var pet = await PlayerCmd.AddPet<T>(summoner);
      await CreatureCmd.SetMaxHp(pet, amount); 
      await CreatureCmd.Heal(pet, amount);
    
    foreach (var c in summoner.Creature.CombatState.Creatures)
    {
      var node = instance.GetCreatureNode(c) ?? throw new InvalidOperationException();
      if (c.PetOwner == summoner && !node.IsInteractable)
      {
        node.ToggleIsInteractable(true);
      }
    }
    return new SummonResult(pet, amount);
    
    
    // ICombatState combatState = summoner.Creature.CombatState;
    // amount = Hook.ModifySummonAmount(combatState, summoner, amount, source);
    // if (amount == 0M)
    //   return new SummonResult(summoner.Osty, 0M);
    // if (CombatManager.Instance.IsInProgress)
    //   SfxCmd.Play("event:/sfx/characters/necrobinder/necrobinder_summon");
    // Creature osty = combatState.Allies.FirstOrDefault<Creature>((Func<Creature, bool>) (c => c.Monster is Osty && c.PetOwner == summoner));
    // if (summoner.IsOstyAlive)
    // {
    //   await CreatureCmd.GainMaxHp(summoner.Osty, amount);
    // }
    // else
    // {
    //   bool isReviving = osty != null;
    //   if (isReviving)
    //   {
    //     if (osty.IsAlive)
    //       throw new InvalidOperationException("We shouldn't make it here if Osty is still alive!");
    //     summoner.PlayerCombatState.AddPetInternal(osty);
    //   }
    //   else
    //   {
    //     osty = await PlayerCmd.AddPet<Osty>(summoner);
    //     NCreature ostyNode = NCombatRoom.Instance?.GetCreatureNode(osty);
    //     if (ostyNode != null && source is CardModel)
    //     {
    //       ostyNode.Modulate = Colors.Transparent;
    //       ostyNode.CreateTween().TweenProperty((GodotObject) ostyNode, (NodePath) "modulate", (Variant) Colors.White, 0.3499999940395355).SetDelay(0.10000000149011612);
    //       ostyNode.StartReviveAnim();
    //     }
    //     DieForYouPower dieForYouPower = await PowerCmd.Apply<DieForYouPower>(choiceContext, osty, 1M, (Creature) null, (CardModel) null);
    //     ostyNode?.TrackBlockStatus(summoner.Creature);
    //     ostyNode = (NCreature) null;
    //   }
    //   Decimal num = await CreatureCmd.SetMaxHp(osty, amount);
    //   await CreatureCmd.Heal(osty, amount, isReviving);
    //   if (isReviving)
    //     await Hook.AfterOstyRevived(combatState, osty);
    // }
    // if (osty != null)
    //   NCombatRoom.Instance?.GetCreatureNode(osty)?.OstyScaleToSize((float) osty.MaxHp, 0.75);
    // CombatManager.Instance.History.Summoned(combatState, (int) amount, summoner);
    // await Hook.AfterSummon(combatState, choiceContext, summoner, amount);


    
  }
}