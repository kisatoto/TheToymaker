using MegaCrit.Sts2.Core.Commands;
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
        Creature knight = await PlayerCmd.AddPet<KnightBot>(Owner);
        NCreature knightNode = NCombatRoom.Instance?.GetCreatureNode(knight);
        knightNode.ToggleIsInteractable(true); /*reminder to make a function to check for all of your constructs to keep them interactable*/
        await CreatureCmd.SetMaxHp(knight, 10);
        await CreatureCmd.Heal(knight, 10);
        ArgumentNullException.ThrowIfNull(play.Target,"cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3M);
}
