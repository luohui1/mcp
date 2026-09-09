// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Mcp.Core.Areas.Server;
using Xunit;

namespace Azure.Mcp.Core.Tests.Areas.Server.Commands.Discovery;

public class ReleaseCommunicationsRegistryTests
{
    [Fact]
    public async Task DiscoverServersAsync_ReleaseCommunicationsServerIsDiscovered()
    {
        // Arrange
        var strategy = RegistryDiscoveryStrategyHelper.CreateStrategy();

        // Act
        var result = await strategy.DiscoverServersAsync(TestContext.Current.CancellationToken);
        var provider = result.FirstOrDefault(p => p.CreateMetadata().Name == "releasecommunications");

        // Assert
        Assert.NotNull(provider);

        var metadata = provider.CreateMetadata();
        Assert.Equal("releasecommunications", metadata.Id);
        Assert.Equal("releasecommunications", metadata.Name);
        Assert.NotEmpty(metadata.Description);
        Assert.Contains("azure", metadata.Description, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("release", metadata.Description, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task DiscoverServersAsync_ReleaseCommunicationsNamespaceCanBeSelected()
    {
        // Arrange
        var configuration = new ServerRuntimeConfiguration { Namespace = ["releasecommunications"] };
        var strategy = RegistryDiscoveryStrategyHelper.CreateStrategy(configuration);

        // Act
        var result = (await strategy.DiscoverServersAsync(TestContext.Current.CancellationToken)).ToList();

        // Assert
        var provider = Assert.Single(result);
        var metadata = provider.CreateMetadata();
        Assert.Equal("releasecommunications", metadata.Id);
        Assert.Equal("releasecommunications", metadata.Name);
    }
}
