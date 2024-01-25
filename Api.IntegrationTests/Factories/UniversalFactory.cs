using AutoFixture.Dsl;

namespace AutoWarden.Api.IntegrationTests.Factories;

public class UniversalFactory<T> : BaseFactory<T>
    where T : class
{
    protected override IPostprocessComposer<T> Setup(ICustomizationComposer<T> customization)
    {
        return customization;
    }
}
