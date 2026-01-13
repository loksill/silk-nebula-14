using Content.Shared.Inventory;
using Robust.Shared.Serialization;

namespace Content.Shared._SN.ElectricDemon;

[ByRefEvent]
public record struct ElecAmountChangedEvent(EntityUid User, float Amount, EntityUid Target);
