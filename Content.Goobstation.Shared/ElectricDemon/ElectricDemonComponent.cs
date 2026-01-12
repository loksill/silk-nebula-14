using Content.Shared.Damage;
using Content.Shared.Damage.Prototypes;
using Content.Shared.Dataset;
using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Polymorph;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.ElectricDemon;

public sealed partial class ElectricDemonComponent : Component
{
    [DataField]
    public List<EntProtoId> BaseDemonActions = new()
    {
        "ActionElectricDemonShop",
        "ActionMoveToCable",
        "ActionDrainingElectricity"
    };

    [DataField]
    public List<EntityUid>? ActionEntities;

    [DataField]
    public float Elec = 0;
}
