using Content.Goobstation.Shared.ElectricDemon;
using Content.Shared.Inventory;
using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.ElectricDemon;

[ByRefEvent]
public record struct ElecAmountChangedEvent(EntityUid User, float Amount);
