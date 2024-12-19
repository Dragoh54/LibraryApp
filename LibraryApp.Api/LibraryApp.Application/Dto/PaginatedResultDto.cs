using LibraryApp.Application.Filters;

namespace LibraryApp.Application.Dto;

public record PaginatedResultDto<TFilter>
{
    public TFilter Filters { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public PaginatedResultDto()
    {
    }

    public PaginatedResultDto(TFilter filters, int pageNumber = 1, int pageSize = 10)
    {
        Filters = filters;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}