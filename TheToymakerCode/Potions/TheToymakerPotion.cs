using BaseLib.Abstracts;
using BaseLib.Utils;
using TheToymaker.TheToymakerCode.Character;

namespace TheToymaker.TheToymakerCode.Potions;

[Pool(typeof(TheToymakerPotionPool))]
public abstract class TheToymakerPotion : CustomPotionModel;