using System;
using static Vanara.PInvoke.FirewallApi;

namespace WindowsFirewallHelper
{
    /// <summary>
    ///     Contains properties of a Windows Firewall with Advanced Security profile
    /// </summary>
    public class FirewallWASProfile : IFirewallProfile
    {
        private readonly FirewallWAS _firewall;
        private readonly NET_FW_PROFILE_TYPE2 _profileType;

        internal FirewallWASProfile(FirewallWAS firewall, NET_FW_PROFILE_TYPE2 profileType)
        {
            _profileType = profileType;
            _firewall = firewall;
        }


        /// <inheritdoc />
        public bool BlockAllInboundTraffic
        {
            get => _firewall.UnderlyingObject.get_BlockAllInboundTraffic(_profileType);
            set => _firewall.UnderlyingObject.set_BlockAllInboundTraffic(_profileType, value);
        }

        /// <inheritdoc />
        public FirewallAction DefaultInboundAction
        {
            get => _firewall.UnderlyingObject.get_DefaultInboundAction(_profileType) == NET_FW_ACTION.NET_FW_ACTION_ALLOW
                ? FirewallAction.Allow
                : FirewallAction.Block;
            set => _firewall.UnderlyingObject.set_DefaultInboundAction(
                _profileType,
                value == FirewallAction.Allow
                    ? NET_FW_ACTION.NET_FW_ACTION_ALLOW
                    : NET_FW_ACTION.NET_FW_ACTION_BLOCK
            );
        }

        /// <inheritdoc />
        public FirewallAction DefaultOutboundAction
        {
            get => _firewall.UnderlyingObject.get_DefaultOutboundAction(_profileType) == NET_FW_ACTION.NET_FW_ACTION_ALLOW
                ? FirewallAction.Allow
                : FirewallAction.Block;
            set => _firewall.UnderlyingObject.set_DefaultOutboundAction(
                _profileType,
                value == FirewallAction.Allow
                    ? NET_FW_ACTION.NET_FW_ACTION_ALLOW
                    : NET_FW_ACTION.NET_FW_ACTION_BLOCK
            );
        }

        /// <inheritdoc />
        public bool Enable
        {
            get => _firewall.UnderlyingObject.get_FirewallEnabled(_profileType);
            set => _firewall.UnderlyingObject.set_FirewallEnabled(_profileType, value);
        }

        /// <inheritdoc />
        public bool IsActive
        {
            get => (NET_FW_PROFILE_TYPE2) _firewall.UnderlyingObject.CurrentProfileTypes ==
                   NET_FW_PROFILE_TYPE2.NET_FW_PROFILE2_ALL ||
                   // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                   ((NET_FW_PROFILE_TYPE2) _firewall.UnderlyingObject.CurrentProfileTypes & _profileType) == _profileType;
        }

        /// <inheritdoc />
        public bool ShowNotifications
        {
            get => !_firewall.UnderlyingObject.get_NotificationsDisabled(_profileType);
            set => _firewall.UnderlyingObject.set_NotificationsDisabled(_profileType, !value);
        }

        /// <inheritdoc />
        public FirewallProfiles Type
        {
            get
            {
                if (_profileType == NET_FW_PROFILE_TYPE2.NET_FW_PROFILE2_ALL)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return (FirewallProfiles) _profileType;
            }
        }


        /// <inheritdoc />
        public bool UnicastResponsesToMulticastBroadcast
        {
            get => !_firewall.UnderlyingObject.get_UnicastResponsesToMulticastBroadcastDisabled(_profileType);
            set => _firewall.UnderlyingObject.set_UnicastResponsesToMulticastBroadcastDisabled(_profileType, !value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            try
            {
                return Type.ToString();
            }
            // ReSharper disable once CatchAllClause
            catch (Exception)
            {
                return base.ToString();
            }
        }
    }
}