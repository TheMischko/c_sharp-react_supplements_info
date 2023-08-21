import {Card, Stack, Text} from "@chakra-ui/react";
import {Supplement} from "../../model/supplement";

export interface SupplementCardProps {
    supplement: Supplement
}
export default function SupplementCard(props: SupplementCardProps) {
    return(
      <Card m="2" p="1">
          <Stack align="center">
              <Text>{props.supplement.name}</Text>
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