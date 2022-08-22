

export class UpdatePasswordFormValues
{

    constructor(currentPassword, newPassword, confirm)
    {
        this.current = currentPassword;
        this.new = newPassword;
        this.confirm = confirm;
    }
}