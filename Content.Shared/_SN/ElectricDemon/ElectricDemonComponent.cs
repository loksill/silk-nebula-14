using Content.Shared.Damage;
using Content.Shared.Damage.Prototypes;
using Content.Shared.Dataset;
using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Polymorph;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Shared._SN.ElectricDemon;

public sealed partial class ElectricDemonComponent : Component
{
    [DataField]
    public List<EntProtoId> BaseDemonActions = new()
    {
        "ActionMoveToCable",
        "ActionDrainingElectricity",
        "ActionElectricDemonShop"
    };

    [DataField]
    public List<EntityUid>? ActionEntities;

    [DataField]
    public float ElecAmount = 0;

    [DataField]
    public float maxDraining = 10;

    [DataField]
    public int DemonKoins = 0;
}
