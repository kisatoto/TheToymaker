using BaseLib.Abstracts;
using TheToymaker.TheToymakerCode.Extensions;
using Godot;

namespace TheToymaker.TheToymakerCode.Character;

public class TheToymakerRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => TheToymaker.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}