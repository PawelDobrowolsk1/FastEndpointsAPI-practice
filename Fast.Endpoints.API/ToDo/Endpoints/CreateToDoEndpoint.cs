using FastEndpoints;

namespace Fast.Endpoints.API.ToDo.Endpoints;

public class CreateToDoEndpoint : Endpoint<ToDo>
{
    private readonly IToDoService _service;

    public CreateToDoEndpoint(IToDoService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Post("todos");
        AllowAnonymous();
        Description(x => x
        .Produces(statusCode: 201));
    }

    public override async Task HandleAsync(ToDo req, CancellationToken ct)
    {
        _service.Create(req);

        await SendCreatedAtAsync($"/todos/{req.Id}", req.Id, req);
    }
}
