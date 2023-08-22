import {useEffect, useState, useContext} from "react";
import {getAllSupplements, getUniqueCategories} from "../../service/supplementService/supplementService";
import {Supplement} from "../../model/supplement";
import {Grid, Stack} from "@chakra-ui/react";
import SupplementCard from "./SupplementCard";
import { SupplementContext } from "../../providers/SupplementsContext";

export default function SupplementsGrid(){
    const {value: supplements, setValue: setSupplements} = useContext(SupplementContext);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchData();
    }, [])

    const fetchData = async() => {
        const supplements = await getAllSupplements();
        setSupplements(supplements);
        const cats = getUniqueCategories(supplements);
        console.log(cats);
        setLoading(false);
    }

    return (
        <>
            {loading
            ? <></>
            : <Grid templateColumns={{base:"repeat(1, 1fr)", sm:"repeat(2, 1fr)", md:"repeat(3, 1fr)", lg:"repeat(4, 1fr)"}}
                    gap="4" m="10">
                {supplements.map((s) => (
                    <SupplementCard key={s.id} supplement={s} />
                ))}
            </Grid>}
        </>

    )
}