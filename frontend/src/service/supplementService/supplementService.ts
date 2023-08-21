import {getEndpointUrl} from "../fetchService/fetchService";
import {Supplement} from "../../model/supplement";

export const getAllSupplements = async ():Promise<Supplement[]> => {
    const url = getEndpointUrl("/Supplements");
    try{
        const response = await fetch(url);
        const data = await response.json();
        return data
    } catch (err){
        console.error(err)
    }
    return [];
}