using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Http;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMapApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async Task Initialize()
        {
            try
            {

                AuthenticationManager.Current.Persistence = await CredentialPersistence.CreateDefaultAsync(); // needs packaged app

                // Call a function to set up the AuthenticationManager for OAuth.
                ArcGISLoginPrompt.SetChallengeHandler();
                
                // Connect to the portal (ArcGIS Online, for example).
                ArcGISPortal arcgisPortal = await ArcGISPortal.CreateAsync();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error starting sample");
            }
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            /* Authentication for ArcGIS location services:
             * Use of ArcGIS location services, including basemaps and geocoding, requires either:
             * 1) ArcGIS identity (formerly "named user"): An account that is a member of an organization in ArcGIS Online or ArcGIS Enterprise
             *    giving your application permission to access the content and location services authorized to an existing ArcGIS user's account.
             *    You'll get an identity by signing into the ArcGIS Portal.
             * 2) API key: A permanent token that grants your application access to ArcGIS location services.
             *    Create a new API key or access existing API keys from your ArcGIS for Developers
             *    dashboard (https://links.esri.com/arcgis-api-keys) then call .UseApiKey("[Your ArcGIS location services API Key]")
             *    in the initialize call below. */

            /* Licensing:
             * Production deployment of applications built with the ArcGIS Maps SDK requires you to license ArcGIS functionality.
             * For more information see https://links.esri.com/arcgis-runtime-license-and-deploy.
             * You can set the license string by calling .UseLicense(licenseString) in the initialize call below 
             * or retrieve a license dynamically after signing into a portal:
             * ArcGISRuntimeEnvironment.SetLicense(await myArcGISPortal.GetLicenseInfoAsync()); */
            try
            {
                // Initialize the ArcGIS Maps SDK runtime before any components are created.
                ArcGISRuntimeEnvironment.Initialize(config => config
                // .UseLicense("[Your ArcGIS Maps SDK License key]")
                // .UseApiKey("[Your ArcGIS location services API Key]")
                  .ConfigureAuthentication(auth => auth
                     .UseDefaultChallengeHandler() // Use the default authentication dialog
                  // .UseOAuthAuthorizeHandler(myOauthAuthorizationHandler) // Configure a custom OAuth dialog
                   )
                );

                _ = Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ArcGIS Maps SDK runtime initialization failed.");

                // Exit application
                this.Shutdown();
            }
        }
    }
}
