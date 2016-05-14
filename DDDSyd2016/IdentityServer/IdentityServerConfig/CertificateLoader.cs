using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DDDSyd2016.IdentityServer.IdentityServerConfig
{
    public static class CertificateLoader
    {
        public static X509Certificate2 LoadCertificate()
        {
#if DEBUG
            return new X509Certificate2(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\IdentityServer\\SomeCert.pfx", "idsrv3test");
#endif

            var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCollection = certStore.Certificates.Find(
                                       X509FindType.FindBySubjectName,"localhost", false);  // typically get this cert identifier from config
            // Get the first cert with the thumbprint
            if (certCollection.Count > 0)
            {
                X509Certificate2 cert = certCollection[0];
                certStore.Close();
                // Use certificate
                return cert;
            }
            certStore.Close();
            throw new System.Security.SecurityException("No certificate specified or found for IdentityServer");
        }
    }
}
