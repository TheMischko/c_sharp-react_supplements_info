import React, { useState } from "react";
import {Supplement} from "../model/supplement";

export interface SupplementContextUseStateType{
  value: Supplement[],
  setValue: React.Dispatch<React.SetStateAction<Supplement[]>>
}

export const SupplementContext = React.createContext<SupplementContextUseStateType | undefined>(undefined);

export default function SupplementContextWrapper({ children }){
  const [value, setValue] = useState<Supplement[]>([]);

  return (
    <SupplementContext.Provider value={{value, setValue}}>
      {children}
    </SupplementContext.Provider>
  )
}