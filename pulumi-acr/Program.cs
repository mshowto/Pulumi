using System.Collections.Generic;
using System.Threading.Tasks;

using Pulumi;
using Pulumi.Azure.ContainerService;
using Pulumi.Docker;

class Program
{
    static Task<int> Main()
    {
        return Pulumi.Deployment.RunAsync(() =>
        {
            var containerRegistry = new Registry("mshowto",
                new RegistryArgs()
                {
                    ResourceGroupName = "mshowto-rg",
                    Sku = "Basic",
                    Name = "mshowto",
                    AdminEnabled = true
                },
                new CustomResourceOptions()
                {
                    ImportId = "/subscriptions/<subscription-id>/resourceGroups/mshowto-rg/providers/Microsoft.ContainerRegistry/registries/mshowto"
                }
            );

            var image = new Image("mshowtoapp",
                new ImageArgs
                {
                    ImageName = "mshowto.azurecr.io/mshowtoapp",
                    Registry = new ImageRegistry()
                    {
                        Server = "mshowto.azurecr.io",
                        Username = containerRegistry.AdminUsername,
                        Password = containerRegistry.AdminPassword,

                    },
                    Build = "./mshowtoapp"
                }
            );

            return new Dictionary<string, object?>
            {
                { "name", image.ImageName },
            };
        });
    }
}
