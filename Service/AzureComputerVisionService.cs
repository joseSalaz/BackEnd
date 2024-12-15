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

        public async Task<ImageAnalysis> AnalyzeImageAsync(Stream imageStream)
        {
            // Seleccionamos las características que queremos analizar
            var features = new List<VisualFeatureTypes?>
            {
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Tags,
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Objects,
                VisualFeatureTypes.Color,
                VisualFeatureTypes.Adult
            };

            // Realizar el análisis completo de la imagen
            return await _client.AnalyzeImageInStreamAsync(imageStream, features);
        }
    }    
}

