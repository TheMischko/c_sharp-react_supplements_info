import { Colors } from "@chakra-ui/react";
import { Supplement } from "../../model/supplement";
import { getSupplementsCategories } from "./supplementService";

const colorMap = {
  "aerobic": "purple.400",
  "anaerobic": "orange.400",
  "endurance": "red.500",
  "fat burning": "yellow.600",
  "general fitness": "green.400",
  "high-intensity": "yellow.300",
  "injury prevention": "teal.500",
  "muscle building": "pink.400",
  "recovery": "teal.300",
  "sports psychology": "blue.400",
  "strength": "cyan.500"
};

const categoryPriority = {
  "aerobic": 28,
  "anaerobic": 7,
  "endurance": 27,
  "fat burning": 26,
  "general fitness": 10,
  "high-intensity": 13,
  "injury prevention": 9,
  "muscle building": 16,
  "recovery": 10,
  "sports psychology": 7,
  "strength": 7
}

export const getCategoryColor = (category:string) => {
  let color = "";
  if(Object.keys(colorMap).includes(category)){
    color = colorMap[category];
  }
  return color;
}

export const getColorForSupplement = (supplement:Supplement) => {
  const categories = getSupplementsCategories(supplement);
  categories.sort(categorySortingFunction);
  let color = "yellow.50";
  for(const cat of categories){
    const catColor = getCategoryColor(cat);
    if(catColor !== ""){
      color = catColor;
      break;
    }
  }

  return color;
}

const categorySortingFunction = (cat1:string, cat2:string) => {
  const cat1Val = Object.keys(categoryPriority).includes(cat1) ? categoryPriority[cat1] : 0;
  const cat2Val = Object.keys(categoryPriority).includes(cat2) ? categoryPriority[cat2] : 0;
  return cat1Val - cat2Val;
}

