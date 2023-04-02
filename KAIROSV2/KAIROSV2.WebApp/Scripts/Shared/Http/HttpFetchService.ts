// Base class for Http operations GET, PUT, POST, DELETE, PATCH!!
export class HttpFetchService {

    public Post<U>(url: string, payload: any, jsonResponse = true): Promise<U> {
        const promise = new Promise<U>((resolve, reject) => {
            fetch(url, {
                method: 'post',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            })
                .then(response => (jsonResponse) ? response.json() : response.text())
                .then(data => {
                    resolve(data);
                })
                .catch((error) => {
                    console.log(error);
                    reject(error);
                })
        });

        return promise;
    }

    public PostForm<U>(url: string, payload: any, token: string, jsonResponse = true): Promise<U> {
        const promise = new Promise<U>((resolve, reject) => {
            fetch(url, {
                method: 'post',
                headers: { 'RequestVerificationToken': token },
                body: payload
            })
                .then(response => (jsonResponse) ? response.json() : response.text())
                .then(data => {
                    resolve(data);
                })
                .catch((error) => {
                    console.log(error);
                    reject(error);
                })
        });

        return promise;
    }

    public PostFormURL<U>(url: string, payload: any, jsonResponse = true): Promise<U> {
        const promise = new Promise<U>((resolve, reject) => {
            fetch(url, {
                method: 'post',
                headers: { 'Content-Type': 'application/json' },
                body: payload
            })
                .then(response => (jsonResponse) ? response.json() : response.text())
                .then(data => {
                    resolve(data);
                })
                .catch((error) => {
                    console.log(error);
                    reject(error);
                })
        });

        return promise;
    }
}