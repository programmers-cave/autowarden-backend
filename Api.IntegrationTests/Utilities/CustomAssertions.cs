using AutoWarden.Api.Models.Response;
using FluentAssertions;

namespace AutoWarden.Api.IntegrationTests.Utilities;

public static class CustomAssertions
{
    public static void AssertCollectionResponse<T>(this PaginatedResponse<T> response,
        int totalCount, int page = 1,
        int pageSize = 10, int? totalPages = null)
        where T : class
    {
        totalPages ??= totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1);

        response.Page.Should().Be(page);
        response.PageSize.Should().Be(pageSize);
        response.TotalCount.Should().Be(totalCount);
        response.TotalPages.Should().Be(totalPages);

        var countOnLastPage = totalCount - (totalPages - 1) * pageSize;
        var countOnPage = page == totalPages ? (int) countOnLastPage! : pageSize;
        response.Data.Should().HaveCount(countOnPage);
    }
}
