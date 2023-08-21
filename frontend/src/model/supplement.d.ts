export interface Supplement {
    id: number,
    name: string,
    altName: string,
    category: string,
    claimedImprovement: string,
    evidenceLevelScore: number,
    hasOTW: boolean,
    numCitation: number,
    numStudies: number,
    popularity: number,
    testedExercise: string
}