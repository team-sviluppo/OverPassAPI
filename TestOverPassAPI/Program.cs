using OverPass;
using OverPass.Utility;

string geometry = "{\"type\": \"Polygon\",\"coordinates\": " +
                  "[[[7.321217387935907,45.71137875194013]," +
                  "[7.325801094114396,45.71137875194013],[7.325801094114396,45.71457268228047]," +
                  "[7.321217387935907,45.71457268228047],[7.321217387935907,45.71137875194013]]]}";

Dictionary<string, List<string>>? Query = new()
{
    { "highway", new List<string>() { "track", "residential", "tertiary", "path" } }
};

OverPassAPI OApi = new(OverPassUtility.DeSerializeGeometry(geometry), Query, true);
string geojson = OApi.GeoJSon;
Console.WriteLine(geojson);