using System.Security.Principal;
using LightLDAP;
using Sitecore.Security;
using System;
using System.Web.Security;

namespace Sitecore.Support.Security
{
    public class SwitchingMembershipProvider : Sitecore.Security.SwitchingMembershipProvider
    {
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            foreach (MembershipProviderWrapper wrapper in Wrappers)
            {
                if ((wrapper.Provider is SqlMembershipProvider && providerUserKey is Guid)
                    || (wrapper.Provider is SitecoreADMembershipProvider && providerUserKey is SecurityIdentifier))
                {
                    MembershipUser user = wrapper.Provider.GetUser(providerUserKey, userIsOnline);
                    if (user != null)
                    {
                        return wrapper.Globalize(user);
                    }
                }
            }
            return null;
        }
    }
}
