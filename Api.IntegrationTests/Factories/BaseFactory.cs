using System.Linq.Expressions;
using AutoFixture;
using AutoFixture.Dsl;
using AutoWarden.Database;
using MongoDB.Driver;

namespace AutoWarden.Api.IntegrationTests.Factories;

public abstract class BaseFactory<T>
{
    private IEnumerable<T>? _objects = null;
    private IPostprocessComposer<T> _postprocessComposer;

    protected abstract IPostprocessComposer<T> Setup(ICustomizationComposer<T> customization);

    public BaseFactory<T> Customize(Expression<Func<IPostprocessComposer<T>, IPostprocessComposer<T>>> expression)
    {
        _postprocessComposer = expression.Compile()(_postprocessComposer);

        return this;
    }

    public BaseFactory<T> Create(int count = 1)
    {
        var fixture = new Fixture();
        var customization = fixture.Build<T>();

        _postprocessComposer = Setup(customization);
        _objects = _postprocessComposer.CreateMany(count);

        return this;
    }

    private IMongoCollection<T> Collection(MongoDbService dbService) =>
        dbService.GetCollection<T>();

    public BaseFactory<T> Persist(MongoDbService dbService)
    {
        if (_objects is null)
            throw new InvalidOperationException("Cannot persist objects before calling Create() method.");

        Collection(dbService).InsertMany(_objects);

        return this;
    }

    public T Object()
    {
        if (_objects is null)
            throw new InvalidOperationException("Cannot get object before calling Create() method.");

        return _objects.First();
    }

    public T? Find(MongoDbService dbService, Expression<Func<T, bool>> filter)
    {
        return Collection(dbService).Find(filter).FirstOrDefault();
    }

    public IEnumerable<T> Objects()
    {
        if (_objects is null)
            throw new InvalidOperationException("Cannot get objects before calling Create() method.");

        return _objects;
    }
}

