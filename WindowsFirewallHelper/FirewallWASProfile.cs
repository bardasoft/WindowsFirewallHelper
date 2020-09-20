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
            get => _firewall.UnderlyingObject.BlockAllInboundTraffic[_profileType];
            set => _firewall.UnderlyingObject.BlockAllInboundTraffic[_profileType] = value;
        }

        /// <inheritdoc />
        public FirewallAction DefaultInboundAction
        {
            get => (FirewallAction)_firewall.UnderlyingObject.DefaultInboundAction[_profileType];
            set => _firewall.UnderlyingObject.DefaultInboundAction[_profileType] = (NET_FW_ACTION)value;
        }

        /// <inheritdoc />
        public FirewallAction DefaultOutboundAction
        {
            get => (FirewallAction)_firewall.UnderlyingObject.DefaultOutboundAction[_profileType];
            set => _firewall.UnderlyingObject.DefaultOutboundAction[_profileType] = (NET_FW_ACTION)value;
        }

        /// <inheritdoc />
        public bool Enable
        {
            get => _firewall.UnderlyingObject.FirewallEnabled[_profileType];
            set => _firewall.UnderlyingObject.FirewallEnabled[_profileType] = value;
        }

        /// <inheritdoc />
        public bool IsActive => _firewall.UnderlyingObject.CurrentProfileTypes == NET_FW_PROFILE_TYPE2.NET_FW_PROFILE2_ALL ||
                   // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                   (_firewall.UnderlyingObject.CurrentProfileTypes & _profileType) == _profileType;

        /// <inheritdoc />
        public bool ShowNotifications
        {
            get => !_firewall.UnderlyingObject.NotificationsDisabled[_profileType];
            set => _firewall.UnderlyingObject.NotificationsDisabled[_profileType] = !value;
        }

        /// <inheritdoc />
        public FirewallProfiles Type => _profileType != NET_FW_PROFILE_TYPE2.NET_FW_PROFILE2_ALL
                    ? (FirewallProfiles)_profileType
                    : throw new ArgumentOutOfRangeException();

        /// <inheritdoc />
        public bool UnicastResponsesToMulticastBroadcast
        {
            get => !_firewall.UnderlyingObject.UnicastResponsesToMulticastBroadcastDisabled[_profileType];
            set => _firewall.UnderlyingObject.UnicastResponsesToMulticastBroadcastDisabled[_profileType] = !value;
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