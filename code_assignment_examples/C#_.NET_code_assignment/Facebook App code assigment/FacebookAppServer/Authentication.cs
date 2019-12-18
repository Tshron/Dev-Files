using System;
using FacebookWrapper;

namespace FacebookAppServer
{
    internal class Authentication
    {
        internal static LoginResult checkPermissions()
        {
            LoginResult result = new LoginResult();
            try
            {
                result = FacebookService.Login(ServerSettings.ServerSetting.ApplicationId, ServerSettings.ServerSetting.desiredFacebookPermissions);
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
