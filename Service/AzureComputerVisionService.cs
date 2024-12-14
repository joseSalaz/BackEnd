using IService;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Options;
using Models.Comon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AzureComputerVisionService : IAzureComputerVisionService
    {
        private readonly ComputerVisionClient _client;

        public AzureComputerVisionService(IOptions<AzureCognitiveServicesSettings> settings)
        {
            // Inicializar el cliente usando las configuraciones inyectadas
            var config = settings.Value;
            _client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(config.SubscriptionKey))
            {
                Endpoint = config.Endpoint
            };
        }

        public async Task<bool> IsBookOrAgendaAsync(Stream imageStream)
        {
            // Definir las características que quieres analizar
            var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Categories, VisualFeatureTypes.Tags };

            // Analizar la imagen
            var analysis = await _client.AnalyzeImageInStreamAsync(imageStream, features);

            // Obtener las tags en minúsculas
            var tags = analysis.Tags.Select(t => t.Name.ToLowerInvariant()).ToList();

            // Verificar si contiene alguna de las palabras clave
            return tags.Contains("book") || tags.Contains("notebook") || tags.Contains("agenda");
        }
    }
}
