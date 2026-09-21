using JsonSampleConverterLibrary.Classes;

namespace JsonConverterSamplesApp.Models;

public class DataClass
{
    [DoubleSerializationStringFormat("N2")]
    public double PropTwoPlaces { get; set; }

    [DoubleSerializationStringFormat("N5")]
    public double PropFivePlaces { get; set; }
}