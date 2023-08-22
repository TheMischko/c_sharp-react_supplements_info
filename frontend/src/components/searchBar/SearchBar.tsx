import { useContext } from 'react';
import { Box, Input, Stack } from "@chakra-ui/react";
import { SupplementContext } from '../../providers/SupplementsContext';
import { findSupplements } from '../../service/supplementService/supplementService';

export default function SearchBar(){
  const {value, setValue} = useContext(SupplementContext);

  const handleValueChanged = async (event) => {
    const newVal = event.target.value;
    const supplements = await findSupplements(newVal);
    setValue(supplements);
  }

  return (
    <Stack w="auto" align="center" ml="10%" mr="10%" mt="2em">
      <Input placeholder="Type supplement name..." onChange={handleValueChanged}/>
    </Stack>
  )
}