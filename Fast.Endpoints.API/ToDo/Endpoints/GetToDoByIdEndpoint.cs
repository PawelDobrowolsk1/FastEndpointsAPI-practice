using FastEndpoints;
using NJsonSchema;

namespace Fast.Endpoints.API.ToDo.Endpoints;

public class GetToDoByIdEndpoint : EndpointWithoutRequest
{
    private readonly IToDoService _service;

    public GetToDoByIdEndpoint(IToDoService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("todos/{id}");
        AllowAnonymous();
        Description(x => x
        .Produces<ToDo>()
        .Produces(statusCode: 404));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var idFromRoute = Route<string>("id");
        Guid.TryParse(idFromRoute, out Guid id);

        var todo = _service.GetById(id);

        if (todo is null)
        {
            await SendNotFoundAsync(ct);
        }

        await SendOkAsync(todo!,ct);
    }
}
