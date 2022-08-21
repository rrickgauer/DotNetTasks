

export class UpdatePasswordArgs
{
    constructor(currentPassword, newPassword)
    {
        this.current = currentPassword;
        this.new = newPassword;
    }
}