using static Vanara.PInvoke.FirewallApi;

namespace WindowsFirewallHelper
{
    /// <summary>
    ///     Third-party firewall rule categories to have ownership from Windows Firewall
    /// </summary>
    public enum FirewallRuleCategory
    {
        /// <summary>
        ///     Boot category
        /// </summary>
        Boot = NET_FW_RULE_CATEGORY.NET_FW_RULE_CATEGORY_BOOT,

        /// <summary>
        ///     Stealth category
        /// </summary>
        Stealth = NET_FW_RULE_CATEGORY.NET_FW_RULE_CATEGORY_STEALTH,

        /// <summary>
        ///     Firewall rules
        /// </summary>
        Firewall = NET_FW_RULE_CATEGORY.NET_FW_RULE_CATEGORY_FIREWALL,

        /// <summary>
        ///     IPSec rules
        /// </summary>
        ConnectionSecurity = NET_FW_RULE_CATEGORY.NET_FW_RULE_CATEGORY_CONSEC
    }
}