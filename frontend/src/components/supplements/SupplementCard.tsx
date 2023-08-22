import {Card, Heading, Stack, Text} from "@chakra-ui/react";
import {Supplement} from "../../model/supplement";
import { getColorForSupplement } from "../../service/supplementService/supplementCategoryColorMap";

export interface SupplementCardProps {
    supplement: Supplement
}
export default function SupplementCard(props: SupplementCardProps) {
    const bgColor = getColorForSupplement(props.supplement);


    return(
      <Card m="2" p="1" backgroundColor={bgColor} textColor="white">
          <Stack align="center">
              <Heading size="md">{props.supplement.name}</Heading>
              <Text>Category: {props.supplement.category}</Text>
              <Text>Improves: {props.supplement.claimedImprovement}</Text>
              <Stack direction="row">
                  <Text>Evidence score: {props.supplement.evidenceLevelScore}</Text>
                  <Text>Popularity: {props.supplement.popularity}</Text>
              </Stack>
          </Stack>
      </Card>
    );
}