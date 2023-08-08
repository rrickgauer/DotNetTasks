


export class CreateChecklistItemForm
{
    constructor(content, position=0)
    {
        this.content = content;
        this.position = position
        this.isComplete = false;
    }
}


export class UpdateChecklistItemForm
{
    constructor(content, position, isComplete)
    {
        this.content = content;
        this.position = position;
        this.isComplete = isComplete;
    }
}

