using Content.Goobstation.Shared.Supermatter.Components;
using Content.Server.Actions;
using Content.Server.Antag.Components;
using Content.Server.Atmos.Components;
using Content.Server.Mind;
using Content.Server.Polymorph.Systems;
using Content.Server.Popups;
using Content.Server.PowerCell;
using Content.Server.Store.Systems;
using Content.Server.Stunnable;
using Content.Server.Zombies;
using Content.Shared._Shitmed.Body.Components;
using Content.Shared._SN.ElectricDemon;
using Content.Shared._SN.ElectricDemon.Actions;
using Content.Shared.Mobs.Systems;
using Content.Shared.Store.Components;
using Robust.Server.Containers;
using Robust.Server.GameObjects;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Timing;
using System.Runtime.CompilerServices;

namespace Content.Server._SilkNebula.ElectricDemon;

public sealed partial class ElectricDemonSystem : EntitySystem
{
    [Dependency] private readonly ActionsSystem _actions = default!;
    [Dependency] private readonly PolymorphSystem _polymorph = default!;
    [Dependency] private readonly StunSystem _stun = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly MobStateSystem _state = default!;
    [Dependency] private readonly ContainerSystem _container = default!;
    [Dependency] private readonly MindSystem _mind = default!;
    [Dependency] private readonly PowerCellSystem _powerCell = default!;
    [Dependency] private readonly StoreSystem _store = default!;
    [Dependency] private readonly UserInterfaceSystem _ui = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly IGameTiming _timing = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<ElectricDemonComponent, MapInitEvent>(OnStarting);
        SubscribeLocalEvent<ElectricDemonComponent, DrainingElectricityEvent>(OnDrainingElectricity);
    }

    private void OnStarting(Entity<ElectricDemonComponent> demon, ref MapInitEvent args)
    {
        // Adjust stats
        EnsureComp<ZombieImmuneComponent>(demon);
        EnsureComp<BreathingImmunityComponent>(demon);
        EnsureComp<PressureImmunityComponent>(demon);
        EnsureComp<AntagImmuneComponent>(demon);
        EnsureComp<SupermatterImmuneComponent>(demon);

        // Add base actions
        foreach (var actionId in demon.Comp.BaseDemonActions)
        {
            _actions.AddAction(demon, actionId);
        }
    }

    private void OnDrainingElectricity(Entity<ElectricDemonComponent> ent, ref DrainingElectricityEvent args)
    {
        var EC = ent.Comp;
        EC.ElecAmount += EC.maxDraining;
        EC.DemonKoins += (int)(EC.maxDraining / 2);
    }

    private void OnOpenShop(Entity<ElectricDemonComponent> ent, ref ElectricDemonShopActionEvent args)
    {
        if (!TryComp<StoreComponent>(ent, out var store))
            return;
        _store.ToggleUi(ent, ent, store);
    }
}
