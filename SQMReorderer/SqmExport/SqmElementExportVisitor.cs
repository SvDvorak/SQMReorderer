using System;
using System.Collections.Generic;
using System.Text;
using SQMReorderer.SqmParser.ResultObjects;
using System.Linq;

namespace SQMReorderer.SqmExport
{
    public class SqmElementExportVisitor : ISqmElementVisitor
    {
        private SqmPropertyVisitor _propertyVisitor = new SqmPropertyVisitor();

        public string Visit(string elementName, ParseResult parseResult)
        {
            var fileString = new StringBuilder();

            fileString.Append(_propertyVisitor.Visit("version", parseResult.Version));

            fileString.Append(Visit("Mission", parseResult.Mission));
            fileString.Append(Visit("Intro", parseResult.Intro));
            fileString.Append(Visit("OutroWin", parseResult.OutroWin));
            fileString.Append(Visit("OutroLoose", parseResult.OutroLose));

            return fileString.ToString();
        }

        public string Visit(string elementName, MissionState mission)
        {
            if(mission == null)
            {
                return "";
            }

            var missionString = new StringBuilder();

            missionString.Append("class ");
            missionString.Append(elementName);
            missionString.Append("\n");
            missionString.Append("{\n");
            missionString.Append(_propertyVisitor.Visit("addOns", mission.AddOns));
            missionString.Append(_propertyVisitor.Visit("addOnsAuto", mission.AddOnsAuto));
            missionString.Append(_propertyVisitor.Visit("randomSeed", mission.RandomSeed));
            missionString.Append(Visit("Intel", mission.Intel));
            missionString.Append(Visit("Groups", mission.Groups));
            missionString.Append(Visit("Vehicles", mission.Vehicles));
            missionString.Append(Visit("Markers", mission.Markers));
            missionString.Append(Visit("Sensors", mission.Sensors));
            missionString.Append("};\n");

            return missionString.ToString();
        }

        public string Visit(string elementName, Intel intel)
        {
            if(intel == null)
            {
                return "";
            }

            var intelString = new StringBuilder();

            intelString.Append("class " + elementName + "\n");
            intelString.Append("{\n");
            intelString.Append(_propertyVisitor.Visit("briefingName", intel.BriefingName));
            intelString.Append(_propertyVisitor.Visit("briefingDescription", intel.BriefingDescription));
            intelString.Append(_propertyVisitor.Visit("startWeather", intel.StartWeather));
            intelString.Append(_propertyVisitor.Visit("forecastWeather", intel.ForecastWeather));
            intelString.Append(_propertyVisitor.Visit("year", intel.Year));
            intelString.Append(_propertyVisitor.Visit("month", intel.Month));
            intelString.Append(_propertyVisitor.Visit("day", intel.Day));
            intelString.Append(_propertyVisitor.Visit("hour", intel.Hour));
            intelString.Append(_propertyVisitor.Visit("minute", intel.Minute));
            intelString.Append("};\n");

            return intelString.ToString();
        }

        private string Visit(
            string elementName,
            List<ItemBase> items,
            Func<string, ItemBase, string> getItemString)
        {
            if (items == null || items.Count == 0)
            {
                return "";
            }

            var itemsString = new StringBuilder();

            itemsString.Append("class " + elementName + "\n");
            itemsString.Append("{\n");
            itemsString.Append(_propertyVisitor.Visit("items", items.Count));

            foreach (var subItem in items)
            {
                itemsString.Append(getItemString("Item" + subItem.Number, subItem));
            }

            itemsString.Append("};\n");

            return itemsString.ToString();
        }

        private string Visit(string elementName, List<Vehicle> vehicles)
        {
            return Visit(elementName, vehicles.Cast<ItemBase>().ToList(), (itemName, item) => Visit(itemName, (Vehicle) item));
        }

        private string Visit(string elementName, List<Marker> markers)
        {
            return Visit(elementName, markers.Cast<ItemBase>().ToList(), (itemName, item) => Visit(itemName, (Marker)item));
        }

        private string Visit(string elementName, List<Sensor> sensors)
        {
            return Visit(elementName, sensors.Cast<ItemBase>().ToList(), (itemName, item) => Visit(itemName, (Sensor)item));
        }

        public string Visit(string elementName, Vehicle vehicle)
        {
            if(vehicle == null)
            {
                return "";
            }

            var vehicleString = new StringBuilder();

            vehicleString.Append("class " + elementName + "\n");
            vehicleString.Append("{\n");
            vehicleString.Append(_propertyVisitor.Visit("position", vehicle.Position));
            vehicleString.Append(_propertyVisitor.Visit("azimut", vehicle.Azimut));
            vehicleString.Append(_propertyVisitor.Visit("id", vehicle.Id));
            vehicleString.Append(_propertyVisitor.Visit("side", vehicle.Side));
            vehicleString.Append(_propertyVisitor.Visit("vehicle", vehicle.VehicleName));
            vehicleString.Append(_propertyVisitor.Visit("player", vehicle.Player));
            vehicleString.Append(_propertyVisitor.Visit("leader", vehicle.Leader));
            vehicleString.Append(_propertyVisitor.Visit("rank", vehicle.Rank));
            vehicleString.Append(_propertyVisitor.Visit("skill", vehicle.Skill));
            vehicleString.Append(_propertyVisitor.Visit("text", vehicle.Text));
            vehicleString.Append(_propertyVisitor.Visit("init", vehicle.Init));
            vehicleString.Append(_propertyVisitor.Visit("description", vehicle.Description));
            vehicleString.Append(_propertyVisitor.Visit("synchronizations", vehicle.Synchronizations));

            vehicleString.Append(Visit("Vehicles", vehicle.Vehicles));

            vehicleString.Append("};\n");


            return vehicleString.ToString();
        }

        public string Visit(string elementName, Marker marker)
        {
            if(marker == null)
            {
                return "";
            }

            var markerString = new StringBuilder();

            markerString.Append("class " + elementName + "\n");
            markerString.Append("{\n");
            markerString.Append(_propertyVisitor.Visit("position", marker.Position));
            markerString.Append(_propertyVisitor.Visit("name", marker.Name));
            markerString.Append(_propertyVisitor.Visit("text", marker.Text));
            markerString.Append(_propertyVisitor.Visit("markerType", marker.MarkerType));
            markerString.Append(_propertyVisitor.Visit("type", marker.Type));
            markerString.Append(_propertyVisitor.Visit("fillName", marker.FillName));
            markerString.Append(_propertyVisitor.Visit("a", marker.A));
            markerString.Append(_propertyVisitor.Visit("b", marker.B));
            markerString.Append(_propertyVisitor.Visit("drawBorder", marker.DrawBorder));
            markerString.Append(_propertyVisitor.Visit("angle", marker.Angle));
            markerString.Append("};\n");

            return markerString.ToString();
        }

        public string Visit(string elementName, Sensor sensor)
        {
            if(sensor == null)
            {
                return "";
            }

            var sensorString = new StringBuilder();

            sensorString.Append("class " + elementName + "\n");
            sensorString.Append("{\n");
            sensorString.Append(_propertyVisitor.Visit("position", sensor.Position));
            sensorString.Append(_propertyVisitor.Visit("a", sensor.A));
            sensorString.Append(_propertyVisitor.Visit("b", sensor.B));
            sensorString.Append(_propertyVisitor.Visit("activationBy", sensor.ActivationBy));
            sensorString.Append(_propertyVisitor.Visit("interruptable", sensor.Interruptable));
            sensorString.Append(_propertyVisitor.Visit("type", sensor.Type));
            sensorString.Append(_propertyVisitor.Visit("age", sensor.Age));
            sensorString.Append(_propertyVisitor.Visit("expCond", sensor.ExpCond));
            sensorString.Append(_propertyVisitor.Visit("expActiv", sensor.ExpActiv));
            //itemString.Append(Visit("Effects", item.Effects));
            sensorString.Append("};\n");

            return sensorString.ToString();
        }
    }
}
