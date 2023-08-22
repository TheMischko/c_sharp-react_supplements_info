import React, { useState } from 'react';
import SupplementsGrid from "./components/supplements/SupplementsGrid";
import {Heading} from "@chakra-ui/react";
import SupplementContext from './providers/SupplementsContext'
import { Supplement } from './model/supplement';
import SupplementContextWrapper from './providers/SupplementsContext';
import SearchBar from './components/searchBar/SearchBar';

export default function App() {
  


  return (
    <div>
        <SupplementContextWrapper>
          <Heading size="2xl" w="100%" textAlign="center">Supplements</Heading>
          <SearchBar />
          <SupplementsGrid/>
        </SupplementContextWrapper>
    </div>
  );
}
