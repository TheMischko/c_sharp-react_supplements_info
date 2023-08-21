const ENDPOINT = "http://localhost:5000";

export const getEndpointUrl = (path:string) => {
    return `${ENDPOINT}${path}`;
}