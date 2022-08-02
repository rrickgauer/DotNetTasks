import { ApiEndpoints } from "./base";
import { HttpMethods } from "./base";
import { HttpRequestMapper } from "../mappers/http-request-mapper";


export class ApiLogin
{
    constructor(email, password) {
        this.email = email;
        this.password = password;
    }

    login = async () => {

        const data = HttpRequestMapper.toFormData({
            email: this.email,
            password: this.password,
        });

        return await fetch(ApiEndpoints.LOGIN, {
            method: HttpMethods.POST,
            body: data,
        });
    }
}

