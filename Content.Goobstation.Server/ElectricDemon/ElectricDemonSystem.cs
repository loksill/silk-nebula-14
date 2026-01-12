using Content.Goobstation.Shared.ElectricDemon;
using Content.Goobstation.Shared.ElectricDemon.Actions;
using Content.Goobstation.Shared.Supermatter.Components;
using Content.Server.Actions;
using Content.Server.Antag.Components;
using Content.Server.Atmos.Components;
using Content.Server.EntityEffects.Effects;
using Content.Server.Mind;
using Content.Server.Polymorph.Systems;
using Content.Server.Popups;
using Content.Server.Power.Components;
using Content.Server.Power.EntitySystems;
using Content.Server.PowerCell;
using Content.Server.Stunnable;
using Content.Server.Zombies;
using Content.Shared._Shitmed.Body.Components;
using Content.Shared.Actions;
using Content.Shared.Actions.Events;
using Content.Shared.Decals;
using Content.Shared.Item.ItemToggle.Components;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Systems;
using Content.Shared.Ninja.Components;
using Robust.Server.Containers;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Goobstation.Server.ElectricDemon;

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
            _actions.AddAction(demon, actionId);
    }

    private void OnDrainingElectricity(Entity<ElectricDemonComponent> demon, Entity<BatteryComponent> comp, ref DrainingElectricityEvent ev)
    {
        if (!HasComp<ApcComponent>(ev.Target.Id) & comp.Comp.CurrentCharge <= 0)
            return;
    }

    private void OnElecAmountChanged(Entity<ElectricDemonComponent> demon, ref ElecAmountChangedEvent args)
    {
        if (!_mind.TryGetMind(args.User, out var mindId, out var mind))
            return;

        demon.Comp.Elec += args.Amount;
    }
}
