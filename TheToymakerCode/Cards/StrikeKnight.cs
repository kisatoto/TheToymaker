using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using TheToymaker.TheToymakerCode.Cards;
using TheToymaker.TheToymakerCode.Character;
using TheToymaker.TheToymakerCode.Powers;

namespace TheToymaker.TheToymakerCode.Cards;

public class StrikeKnight() : TheToymakerCard(1,
    CardType.Attack, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    
    protected override HashSet<CardTag> CanonicalTags => [ CardTag.Strike ];
   

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(6, ValueProp.Move)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Creature knight = (await MinionCmd.Summon<KnightBot>(Owner, 1)).Creature;
        PowerModel propPower = await PowerCmd.Apply<DamageBot>(choiceContext, knight, DynamicVars.Damage.BaseValue, Owner.Creature, this);
        propPower.Target = play.Target;
        // ArgumentNullException.ThrowIfNull(play.Target,"cardPlay.Target");
        // AttackCommand attack = DamageCmd.Attack(damage);
        // CreatureCmd.Damage(choiceContext, play.Target, DynamicVars.Damage, this, play);

        // MinionAttack.


        // AttackCommand(DynamicVars.Damage.BaseValue) (knight, this, play).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }
    
    

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3M);
}
