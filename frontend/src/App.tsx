import React from 'react';
import SupplementsGrid from "./components/supplements/SupplementsGrid";
import {Heading} from "@chakra-ui/react";

export default function App() {
  return (
    <div>
        <Heading w="100%" textAlign="center">Supplements</Heading>
      <SupplementsGrid/>
    </div>
  );
}
