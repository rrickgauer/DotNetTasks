using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Controllers;

public class ChecklistController : 
    IRoute<CloneChecklistArgs>,
    IRoute<DeleteChecklistArgs>,
    IRoute<EditChecklistArgs>,
    IRoute<ViewChecklistArgs>,
    IRoute<NewChecklistArgs>
{
    public void Route(CloneChecklistArgs args)
    {
        Console.WriteLine("CloneChecklistArgs");
    }

    public void Route(DeleteChecklistArgs args)
    {
        Console.WriteLine("DeleteChecklistArgs");
    }

    public void Route(EditChecklistArgs args)
    {
        Console.WriteLine("EditChecklistArgs");
    }

    public void Route(NewChecklistArgs args)
    {
        Console.WriteLine("NewChecklistArgs");
    }

    public void Route(ViewChecklistArgs args)
    {
        Console.WriteLine("ViewAllChecklistArgs");
    }
}
