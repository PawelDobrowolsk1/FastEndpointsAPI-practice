using FastEndpoints;

namespace Fast.Endpoints.API.ToDo.Endpoints;

public class GetAllToDoEndpoint : EndpointWithoutRequest<List<ToDo>>
{
    private readonly IToDoService _service;

    public GetAllToDoEndpoint(IToDoService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("/todos");
        AllowAnonymous();
        Description(x => x
        .Produces<List<ToDo>>());
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var todos = _service.GetAll();

        await SendAsync(todos);
    }
}
