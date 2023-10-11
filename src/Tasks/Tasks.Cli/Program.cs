
using System.CommandLine;
using System.CommandLine.Invocation;

var titleOption = new Option<string?>("--title", "Checklist title");

var checklistReferenceArgument = new Argument<int>("checklistReference", "The checklist index");


Command cloneCommand = new("clone", "Clone the checklist")
{
    titleOption,
    checklistReferenceArgument
};

cloneCommand.SetHandler(Callback1, titleOption, checklistReferenceArgument);


Command checklistCommand = new("checklist")
{
    cloneCommand,
};

var rootCommand = new RootCommand("Tasks CLI")
{
    checklistCommand,
};


return await rootCommand.InvokeAsync(args);




static void Callback1(string? title, int checklistReference)
{
    int x = 10;

    Console.WriteLine("in here bitch");
}

