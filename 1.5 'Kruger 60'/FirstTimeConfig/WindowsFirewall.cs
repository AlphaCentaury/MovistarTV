// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using NetFwTypeLib;
using System;
using System.Runtime.InteropServices;

namespace IpTviewr.Tools.FirstTimeConfig
{
    internal class WindowsFirewall : IDisposable
    {
        private bool _disposed;
        private readonly INetFwPolicy2 _firewallPolicy;
        private readonly Type _netFwRuleType;
        private readonly bool _supportsIFwRule2;

        public WindowsFirewall()
        {
            var fwPolicy2Type = Type.GetTypeFromCLSID(new Guid("E2B3C97F-6AE1-41AC-817A-F6F92166D7DD"));
            _firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(fwPolicy2Type);

            _netFwRuleType = Type.GetTypeFromCLSID(new Guid("2C5BC43E-3369-4C33-AB0C-BE9469677AF4"));

            object rule = null;
            INetFwRule2 rule2 = null;
            try
            {
                rule = Activator.CreateInstance(_netFwRuleType);
                rule2 = rule as INetFwRule2;
                if (rule2 != null)
                {
                    _supportsIFwRule2 = true;
                } // if
            }
            finally
            {
                if (rule != null) Marshal.FinalReleaseComObject(rule);
                if (rule2 != null) Marshal.FinalReleaseComObject(rule2);
            } // try-finally
        } // constructor

        ~WindowsFirewall()
        {
            Dispose(false);
        } // destructor

        public void AllowProgram(string path, string name, string description)
        {
            if (_supportsIFwRule2)
            {
                AllowProgramWin7Plus(path, name, description);
            }
            else
            {
                AllowProgramWinVista(path, name, description);
            } // if-else
        } // AllowProgram

        public void Dispose()
        {
            if (_disposed) return;
            Dispose(true);
            GC.SuppressFinalize(this);
        } // Dispose

        private void Dispose(bool disposing)
        {
            Marshal.FinalReleaseComObject(_firewallPolicy);

            _disposed = true;
        } // Dispose

        private void AllowProgramWinVista(string path, string name, string description)
        {
            INetFwRule fwRuleTcp, fwRuleUdp;
            INetFwRules fwRules;

            fwRuleTcp = null;
            fwRuleUdp = null;
            fwRules = null;
            try
            {
                fwRuleTcp = (INetFwRule)Activator.CreateInstance(_netFwRuleType);
                fwRuleTcp.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                fwRuleTcp.ApplicationName = path;
                fwRuleTcp.Description = string.Format(description, "TCP");
                fwRuleTcp.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                fwRuleTcp.Enabled = true;
                fwRuleTcp.Name = name;
                fwRuleTcp.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;

                fwRuleUdp = (INetFwRule)Activator.CreateInstance(_netFwRuleType);
                fwRuleUdp.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                fwRuleUdp.ApplicationName = path;
                fwRuleUdp.Description = string.Format(description, "UDP");
                fwRuleUdp.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                fwRuleUdp.Enabled = true;
                fwRuleUdp.Name = name;
                fwRuleUdp.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;

                fwRules = _firewallPolicy.Rules;
                fwRules.Add(fwRuleTcp);
                fwRules.Add(fwRuleUdp);
            }
            finally
            {
                if (fwRules != null) Marshal.FinalReleaseComObject(fwRules);
                if (fwRuleTcp != null) Marshal.FinalReleaseComObject(fwRuleTcp);
                if (fwRuleUdp != null) Marshal.FinalReleaseComObject(fwRuleUdp);
            } // try-finally
        } // AllowProgramWinVista

        private void AllowProgramWin7Plus(string path, string name, string description)
        {
            INetFwRule2 fwRuleTcp, fwRuleUdp;
            INetFwRules fwRules;

            fwRuleTcp = null;
            fwRuleUdp = null;
            fwRules = null;
            try
            {
                fwRuleTcp = (INetFwRule2)Activator.CreateInstance(_netFwRuleType);
                fwRuleTcp.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                fwRuleTcp.ApplicationName = path;
                fwRuleTcp.Description = string.Format(description, "TCP");
                fwRuleTcp.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                fwRuleTcp.Enabled = true;
                fwRuleTcp.Name = name;
                fwRuleTcp.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                fwRuleTcp.Profiles = (int)(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE | NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN);

                fwRuleUdp = (INetFwRule2)Activator.CreateInstance(_netFwRuleType);
                fwRuleUdp.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                fwRuleUdp.ApplicationName = path;
                fwRuleUdp.Description = string.Format(description, "UDP");
                fwRuleUdp.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                fwRuleUdp.Enabled = true;
                fwRuleUdp.Name = name;
                fwRuleUdp.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                fwRuleUdp.Profiles = (int)(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE | NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN);

                fwRules = _firewallPolicy.Rules;
                fwRules.Add(fwRuleTcp);
                fwRules.Add(fwRuleUdp);
            }
            finally
            {
                if (fwRules != null) Marshal.FinalReleaseComObject(fwRules);
                if (fwRuleTcp != null) Marshal.FinalReleaseComObject(fwRuleTcp);
                if (fwRuleUdp != null) Marshal.FinalReleaseComObject(fwRuleUdp);
            } // try-finally
        } // AllowProgramWin7plus
    } // internal class WindowsFirewall
} // namespace
