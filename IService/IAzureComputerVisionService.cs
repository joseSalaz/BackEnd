using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IAzureComputerVisionService
    {
        Task<ImageAnalysis> AnalyzeImageAsync(Stream imageStream);
    }
}
