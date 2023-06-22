using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;

namespace Session4_BoilerPlate.Configurations
{
    public class DataProtectionConfig
    {
        public static void ConfigureDataProtection(IServiceCollection services)
        {
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("path/to/keys")) // Specify the directory to persist the keys
                .SetDefaultKeyLifetime(TimeSpan.FromDays(30))
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });
        }

       


    }
}
