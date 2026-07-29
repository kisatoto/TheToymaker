using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using TheToymaker.TheToymakerCode.Powers;

namespace TheToymaker.TheToymakerCode.Powers;

public class ConstructPower() : TheToymakerPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    public override Creature ModifyUnblockedDamageTarget(
        Creature target,
        Decimal _,
        ValueProp props,
        Creature? __)
    {
        Creature lowest = target.CombatState.Allies.OrderBy(c => c.GetPowerAmount<ConstructPower>()).First();
        return target != Owner.PetOwner?.Creature || Owner.IsDead || !props.IsPoweredAttack() ? target : lowest;
    }

    public async Task BeforeDeath (PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
    {
        if (!creature.HasPower<ConstructPower>())
            return;
        if (creature.IsPet)
            foreach (var minion in creature.CombatState.GetTeammatesOf(creature).Where(c=> c.PetOwner == creature.PetOwner))
            {
                await PowerCmd.Apply<ConstructPower>(choiceContext, minion, -1M, creature, null);
            }
        
    }
}