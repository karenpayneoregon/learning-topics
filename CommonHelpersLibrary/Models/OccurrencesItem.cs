namespace CommonHelpersLibrary.Models
{
    /// <summary>
    /// Represents an item that tracks the occurrences of a specific character, 
    /// along with its associated code and other related data.
    /// </summary>
    public class OccurrencesItem
    {
        public char Character { get; set; }
        public int Occurrences { get; set; }
        public int Code { get; set; }
        public override string ToString() => $"{Character} - {Occurrences}";

        public void Deconstruct(out char letter, out int occurrences, out int code)
        {
            letter = Character;
            occurrences = Occurrences;
            code = Code;
        }
        public void Deconstruct(out char character, out int occurrences)
        {
            character = Character;
            occurrences = Occurrences;
        }
    }
}
