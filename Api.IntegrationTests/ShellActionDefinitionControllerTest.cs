using AutoWarden.Api.IntegrationTests.Factories;
using AutoWarden.Api.IntegrationTests.Utilities;
using AutoWarden.Api.Models.Request;
using AutoWarden.Api.Models.Response;
using AutoWarden.Database.Entities.ActionDefinition;
using FluentAssertions;
using Xunit;

namespace AutoWarden.Api.IntegrationTests;

public class ShellActionDefinitionControllerTest : IntegrationTest
{
    public ShellActionDefinitionControllerTest(ApiWebApplicationFactory fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShellActionDefinition_Get_Collection_ApiResponse()
    {
        new UniversalFactory<ShellActionDefinitionEntity>()
            .Create(11)
            .Persist(_dbService);

        var response =
            await _client.GetAndDeserialize<PaginatedResponse<ActionDefinitionGetCollectionDto>>(
                "/api/action-definitions");

        response.AssertCollectionResponse(11);
    }

    [Fact]
    public async Task ShellActionDefinition_Get_Item_ApiResponse()
    {
        var entity = new UniversalFactory<ShellActionDefinitionEntity>()
            .Create()
            .Persist(_dbService)
            .Object();

        var response =
            await _client.GetAndDeserialize<ShellActionDefinitionGetDto>(
                $"/api/action-definitions/{entity.Id}");

        response.Id.Should().Be(entity.Id);
    }

    [Fact]
    public async Task ShellActionDefinition_Create()
    {
        var createDto = new UniversalFactory<ShellActionDefinitionCreateDto>()
            .Create()
            .Object();

        var response =
            await _client.PostAsJsonAsyncAndDeserialize<ShellActionDefinitionCreateDto, ShellActionDefinitionGetDto>(
                "/api/action-definitions/shell", createDto);

        var shellActionDefinitionEntity =
            new UniversalFactory<ShellActionDefinitionEntity>().Find(_dbService, x => x.Id == createDto.Id);

        shellActionDefinitionEntity.Should().NotBeNull();
        shellActionDefinitionEntity!.Id.Should().Be(createDto.Id);
        shellActionDefinitionEntity.Name.Should().Be(response.Name);
    }

    [Fact]
    public async Task ShellActionDefinition_Update()
    {
        var preUpdateEntity = new UniversalFactory<ShellActionDefinitionEntity>()
            .Create()
            .Persist(_dbService)
            .Object();

        var updateDto = new UniversalFactory<ShellActionDefinitionUpdateDto>()
            .Create()
            .Object();

        var response =
            await _client.PatchAsJsonAsyncAndDeserialize<ShellActionDefinitionUpdateDto, ShellActionDefinitionGetDto>(
                $"/api/action-definitions/shell/{preUpdateEntity.Id}", updateDto);

        var postUpdateEntity = new UniversalFactory<ShellActionDefinitionEntity>()
            .Find(_dbService, x => x.Id == response.Id);

        response.Should().BeEquivalentTo(updateDto);
        response.Should().BeEquivalentTo(postUpdateEntity);

        response.Should().NotBeEquivalentTo(preUpdateEntity);
        response.Id.Should().Be(preUpdateEntity.Id);
    }

    [Fact]
    public async Task ShellActionDefinition_Delete()
    {
        var entity = new UniversalFactory<ShellActionDefinitionEntity>()
            .Create()
            .Persist(_dbService)
            .Object();

        await _client.DeleteAsync($"/api/action-definitions/shell/{entity.Id}");

        var postDeleteEntity = new UniversalFactory<ShellActionDefinitionEntity>()
            .Find(_dbService, x => x.Id == entity.Id);

        postDeleteEntity.Should().BeNull();
    }
}
