using Xunit;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests;

public class HealthCheckTests
{
    [Fact]
    public void HealthCheck_ReturnsHealthyStatus()
    {
        var controller = new HealthCheckController();
        var result = controller.Get() as OkObjectResult;
        Assert.NotNull(result);
        dynamic value = result!.Value!;
        Assert.Equal("Healthy", (string)value.status);
    }
}