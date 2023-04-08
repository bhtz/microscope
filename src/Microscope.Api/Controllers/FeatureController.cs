using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace Microscope.Api.Controllers;

public class FeaturesController : ControllerBase
{
    private readonly IFeatureManager _featureManager;

    public FeaturesController(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    [HttpGet]
    [Route("api/[controller]")]
    public async Task<Dictionary<string, bool>> ListFeatures()
    {
        return await _featureManager.GetFeatureNamesAsync()
            .ToDictionaryAsync(
                feature => feature, 
                feature => _featureManager.IsEnabledAsync(feature).GetAwaiter().GetResult());
    }
}