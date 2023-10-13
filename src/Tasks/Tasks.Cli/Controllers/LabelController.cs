using Tasks.Service.Domain.CliArgs.Cli.Label;

namespace Tasks.Cli.Controllers;

public class LabelController :
    IRoute<NewLabelArgs>,
    IRoute<EditLabelArgs>,
    IRoute<DeleteLabelArgs>,
    IRoute<ViewAllLabelArgs>
{
    public void Route(NewLabelArgs args)
    {
        Console.WriteLine("new label");
    }

    public void Route(EditLabelArgs args)
    {
        Console.WriteLine("edit label");
    }

    public void Route(DeleteLabelArgs args)
    {
        Console.WriteLine("delete label");
    }

    public void Route(ViewAllLabelArgs args)
    {
        Console.WriteLine("view all labels");
    }
}
