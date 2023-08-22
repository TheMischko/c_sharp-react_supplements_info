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

export const getUniqueCategories = (supplements:Supplement[]):string[] => {
    const categories:string[] = [];
    const catCounts = {};
    for(const s of supplements){
        const parsedCats = getSupplementsCategories(s);
        for(const cat of parsedCats){
            if(!categories.includes(cat)){
                categories.push(cat);
            }
            if(!Object.keys(catCounts).includes(cat)){
                catCounts[cat] = 0;
            }
            catCounts[cat]++;
        }
    }
    return categories;
}

export const getSupplementsCategories = (supplement:Supplement):string[] => {
    const pattern = "\\–|(,\W)";
    const regex = new RegExp(pattern);
    return supplement.category.split(regex);
}

export const findSupplements = async (searchString: string):Promise<Supplement[]> => {
    if(searchString === ""){
        return await getAllSupplements();
    }

    const url = getEndpointUrl(`/supplements/find/${searchString}`);
    try{
        const response = await fetch(url);
        if(response.status === 404){
            return [];
        }
        const data = await response.json();
        return data
    } catch (err){
        console.error(err)
    }
    return [];
}