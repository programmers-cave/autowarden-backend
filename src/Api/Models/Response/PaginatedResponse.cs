namespace AutoWarden.Api.Models.Response;

/// <summary>
/// Response model for collection data.
/// </summary>
public class PaginatedResponse<TModel> 
    where TModel : class
{
    /// <summary>Current page</summary>
    /// <example>1</example>
    public int Page { get; set; }
    
    /// <summary>Size of page</summary>
    /// <example>10</example>
    public int PageSize { get; set; }
    
    /// <summary>Total count of records</summary>
    /// <example>26</example>
    public int TotalCount { get; set; }
    
    /// <summary>Total count of pages</summary>
    /// <example>3</example>
    public int TotalPages { get; set; }
    
    /// <summary>Paginated data</summary>
    public List<TModel> Data { get; set; } = null!;
}
