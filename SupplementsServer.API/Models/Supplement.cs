namespace SupplementsServer.API.Models; 

public class Supplement {
    public int Id { get; set; }
    public string Name { get; set; }
    public string AltName { get; set; }
    public float EvidenceLevelScore { get; set; }
    public string ClaimedImprovement { get; set; }
    public string Category { get; set; }
    public string TestedExercise { get; set; }
    public bool HasOTW { get; set; }
    public int Popularity { get; set; }
    public int NumStudies { get; set; }
    public int NumCitations { get; set; }
}