using FastEndpoints;
using YamlDotNet.Serialization;

namespace Fast.Endpoints.API.ToDo.Endpoints;

public class DeleteToDoEndpoint : EndpointWithoutRequest
{
    private readonly IToDoService _service;

    public DeleteToDoEndpoint(IToDoService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Delete("todos/{id}");
        AllowAnonymous();
        Description(x => x
        .Produces(statusCode: 204)
        .Produces(statusCode: 404));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Guid.TryParse(Route<string>("id"), out Guid id);

        var todo = _service.GetById(id);

        if (todo == null)
        {
            await SendNotFoundAsync(ct);
        }

        _service.Delete(id);

        await SendNoContentAsync(ct);
    }
}
