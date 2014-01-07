using System.Collections.Generic;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmExport
{
    public interface ISqmElementVisitor
    {
        string Visit(string elementName, SqmContents sqmContents);
        string Visit(string elementName, MissionState mission);
        string Visit(string elementName, Intel intel);
        string Visit(string elementName, List<Vehicle> vehicles);
        string Visit(string elementName, List<Marker> markers);
        string Visit(string elementName, List<Sensor> sensors);
        string Visit(string elementName, Vehicle vehicle);
        string Visit(string elementName, Marker marker);
        string Visit(string elementName, Sensor sensor);
    }
}
