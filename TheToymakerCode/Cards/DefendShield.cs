using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheToymaker.TheToymakerCode.Cards;
using TheToymaker.TheToymakerCode.Character;

namespace TheToymaker.TheToymakerCode.Cards;

public class DefendShield() : TheToymakerCard(1,
    CardType.Attack, CardRarity.Basic,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
        Creature knight = (await MinionCmd.Summon<ShieldBot>(Owner, 10)).Creature;
    }

    protected override void OnUpgrade()
    {

    }
}