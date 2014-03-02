﻿using System.Collections.Generic;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Core.Export.ArmA3
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
