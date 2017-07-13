namespace Sitecore.Support.Security
{
  using System.Security.Principal;
  using LightLDAP;
  using Sitecore.Security;
  using System;
  using System.Web.Security;
  public class SwitchingMembershipProvider : Sitecore.Security.SwitchingMembershipProvider
  {
    #region Original code

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      foreach (MembershipProviderWrapper wrapper in Wrappers)
      {

        #endregion

        #region Added code

        // pass through only when providerUserKey type matches the provider to avoid type mismatch exception
        if ((wrapper.Provider is SqlMembershipProvider && providerUserKey is Guid)
            || (wrapper.Provider is SitecoreADMembershipProvider && providerUserKey is SecurityIdentifier))
        {

          #endregion

          #region Original code

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

  #endregion
}
