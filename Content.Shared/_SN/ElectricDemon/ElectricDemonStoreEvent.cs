using Content.Shared.Actions;
using Content.Shared.DoAfter;
using Robust.Shared.Serialization;

namespace Content.Shared._SN.ElectricDemon;

public enum ElectricDemonStoreType
{
    Nome,
    NoSlip,
    Sprint,
    Regen,
    exalted
}

[Serializable, NetSerializable]
[ImplicitDataDefinitionForInheritors]
public sealed partial class ElectricDemonStoreEvent : EntityEventArgs
{
    [DataField("buyType")]
    public ElectricDemonStoreType BuyType;
}

public sealed partial class ElectricDemonShopActionEvent : InstantActionEvent
{
}
