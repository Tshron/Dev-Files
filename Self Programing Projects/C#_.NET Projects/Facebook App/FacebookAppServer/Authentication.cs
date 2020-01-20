using System;
using FacebookWrapper;

namespace FacebookAppServer
{
    internal class Authentication
    {
        internal static LoginResult Login()
        {
            LoginResult result = new LoginResult();
            try
            {
                result = FacebookService.Login(Server.m_Settings.ApplicationId, Server.m_Settings.desiredFacebookPermissions);
            }
            catch(Exception ex)
            {
            }

            return result;
        }

        internal static LoginResult Connect(string i_AccessToken)
        {
            return FacebookService.Connect(i_AccessToken);
        }
    }
}
