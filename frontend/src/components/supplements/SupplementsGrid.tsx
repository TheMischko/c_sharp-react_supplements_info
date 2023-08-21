import {useEffect, useState} from "react";
import {getAllSupplements} from "../../service/supplementService/supplementService";
import {Supplement} from "../../model/supplement";
import {Grid, Stack} from "@chakra-ui/react";
import SupplementCard from "./SupplementCard";

export default function SupplementsGrid(){
    const [supplements, setSupplements] = useState<Supplement[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchData();
    }, [])

    const fetchData = async() => {
        const supplements = await getAllSupplements();
        setSupplements(supplements)
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