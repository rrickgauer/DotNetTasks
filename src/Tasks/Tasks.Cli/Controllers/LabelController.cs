using Tasks.Service.Domain.CliArgs.Cli.Label;

namespace Tasks.Cli.Controllers;

public class LabelController :
    IRouteAsync<NewLabelArgs>,
    IRouteAsync<EditLabelArgs>,
    IRouteAsync<DeleteLabelArgs>,
    IRouteAsync<ListLabelArgs>
{
    public async Task RouteAsync(NewLabelArgs args)
    {
        Console.WriteLine("new label");
    }

    public async Task RouteAsync(EditLabelArgs args)
    {
        Console.WriteLine("edit label");
    }

    public async Task RouteAsync(DeleteLabelArgs args)
    {
        Console.WriteLine("delete label");
    }

    public async Task RouteAsync(ListLabelArgs args)
    {
        Console.WriteLine("view all labels");
    }
}
