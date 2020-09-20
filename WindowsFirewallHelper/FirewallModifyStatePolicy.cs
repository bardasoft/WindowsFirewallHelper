using static Vanara.PInvoke.FirewallApi;

namespace WindowsFirewallHelper
{
    /// <summary>
    ///     Firewall modify state policies
    /// </summary>
    public enum FirewallModifyStatePolicy
    {
        /// <summary>
        ///     All modifications take effects immediately
        /// </summary>
        Ok = NET_FW_MODIFY_STATE.NET_FW_MODIFY_STATE_OK,

        /// <summary>
        ///     Firewall is controlled by group policy
        /// </summary>
        OverrodeByGroupPolicy = NET_FW_MODIFY_STATE.NET_FW_MODIFY_STATE_GP_OVERRIDE,

        /// <summary>
        ///     All inbound traffic is blocked regardless of registered rules
        /// </summary>
        InboundBlocked = NET_FW_MODIFY_STATE.NET_FW_MODIFY_STATE_INBOUND_BLOCKED
    }
}