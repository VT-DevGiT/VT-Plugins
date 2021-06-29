using Scp914;
using Synapse.Api;
using Synapse.Api.Items;
using VT_Referance.Event.EventArguments;

namespace VT_Referance.Event
{
    public class VT_MapEvents
    {
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<WarHeadInteracteEventArgs> WarHeadStartEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<CassieAnnouncementEventArgs> CassieAnnouncementEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<GeneratorActivatedEventArgs> GeneratorActivatedEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<WarHeadInteracteEventArgs> ActivatingWarheadPanelEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<WarHeadInteracteEventArgs> WarheadStopEventEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<Change914KnobSettingEventArgs> Scp914changeSettingEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<ElevatorIneractEventArgs> ElevatorIneractEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<LockerInteractEventArgs> LockerInteractEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<Scp914ActivateEventArgs> Scp914ActivateEvent;

        #region Invoke
        internal void InvokeWarHeadStartEvent(Player player, ref bool allow)
        {
            var ev = new WarHeadInteracteEventArgs
            {
                Player = player,
                Allow = allow
            };

            WarHeadStartEvent?.Invoke(ev);

            allow = ev.Allow;
        }

        internal void InvokeCassieAnnouncementEvent(string worlds, bool makeHold, bool makeNoise, ref bool allow)
        {
            var ev = new CassieAnnouncementEventArgs
            {
                Words = worlds,
                MakeHold = makeHold,
                MakeNoise = makeNoise,
                Allow = allow
            };

            CassieAnnouncementEvent?.Invoke(ev);

            allow = ev.Allow;
        }

        internal void InvokeGeneratorActivatedEvent(Generator generator)
        {
            var ev = new GeneratorActivatedEventArgs
            {
                Generator = generator
            };

            GeneratorActivatedEvent?.Invoke(ev);
        }

        internal void InvokeActivatWarheadPanelEvent(Player player, SynapseItem item, ref bool allow)
        {
            var ev = new WarHeadInteracteEventArgs
            {
                Player = player,
                Allow = allow
            };

            ActivatingWarheadPanelEvent?.Invoke(ev);

            allow = ev.Allow;
        }

        internal void InvokeWarheadStopEvent(Player player, ref bool allow)
        {
            var ev = new WarHeadInteracteEventArgs
            {
                Player = player,
                Allow = allow,
            };

            WarheadStopEventEvent?.Invoke(ev);

            allow = ev.Allow;
        }

        internal void InvokeChange914KnobSettingEvent(Player player, Scp914Knob curSetting, ref Scp914Knob nextSetting, ref bool allow)
        {
            var ev = new Change914KnobSettingEventArgs
            {
                Player = player,
                Allow = allow,
                CurSetting = curSetting,
                NextSetting = nextSetting
            };

            Scp914changeSettingEvent?.Invoke(ev);

            allow = ev.Allow;
            nextSetting = ev.NextSetting;
        }

        internal void InvokeElevatorIneractEvent(Player player, Elevator elevator, ref bool allow)
        {
            var ev = new ElevatorIneractEventArgs
            {
                Player = player,
                Allow = allow,
                Elevator = elevator
            };

            ElevatorIneractEvent?.Invoke(ev);

            allow = ev.Allow;
        }


        internal void InvokeLockerIneractEvent(Player player, Locker locker, ref bool allow)
        {
            var ev = new LockerInteractEventArgs
            {
                Player = player,
                Allow = allow,
                Locker = locker
            };

            LockerInteractEvent?.Invoke(ev);

            allow = ev.Allow;
        }
        internal void InvokeScp914ActivateEvent(Player player, ref bool allow)
        {
            var ev = new Scp914ActivateEventArgs
            {
                Player = player,
                Allow = allow
            };

            Scp914ActivateEvent?.Invoke(ev);

            allow = ev.Allow;
        }
        #endregion
    }
}