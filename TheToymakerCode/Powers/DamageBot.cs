using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheToymaker.TheToymakerCode.Powers;

namespace TheToymaker.TheToymakerCode.Powers;

public class DamageBot() : TheToymakerPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(0, ValueProp.Move),
    ];

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        DynamicVars.Damage.BaseValue = Amount;
        await CreatureCmd.Damage(new BlockingPlayerChoiceContext(), Target ?? throw new InvalidOperationException(), DynamicVars.Damage, null, null);
    }
}