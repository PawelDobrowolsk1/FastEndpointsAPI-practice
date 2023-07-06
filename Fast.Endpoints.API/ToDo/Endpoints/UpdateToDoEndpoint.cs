using FastEndpoints;

namespace Fast.Endpoints.API.ToDo.Endpoints;

public class UpdateToDoEndpoint : Endpoint<ToDo>
{
    private readonly IToDoService _service;

    public UpdateToDoEndpoint(IToDoService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Put("todos/{id}");
        AllowAnonymous();
        Description(x => x
        .Produces(statusCode: 204)
        .Produces(statusCode: 404)
        );
    }

    public override async Task HandleAsync(ToDo req, CancellationToken ct)
    {
        Guid.TryParse(Route<string>("id"), out Guid id);

        var todo = _service.GetById(id);
        if (todo == null)
        {
            await SendNotFoundAsync(ct);
        }

        _service.Update(req);

        await SendNoContentAsync(ct);
    }
}
