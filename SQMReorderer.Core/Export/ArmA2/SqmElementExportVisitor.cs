using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Export.ArmA2
{
    public class SqmElementExportVisitor : ISqmElementVisitor
    {
        private readonly SqmPropertyVisitor _propertyVisitor = new SqmPropertyVisitor();

        public string Visit(string elementName, SqmContents sqmContents)
        {
            var fileString = new StringBuilder();

            fileString.Append(_propertyVisitor.Visit("version", sqmContents.Version));

            fileString.Append(Visit("Mission", sqmContents.Mission));
            fileString.Append(Visit("Intro", sqmContents.Intro));
            fileString.Append(Visit("OutroWin", sqmContents.OutroWin));
            fileString.Append(Visit("OutroLoose", sqmContents.OutroLose));

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
            intelString.Append(_propertyVisitor.Visit("resistanceWest", intel.ResistanceWest));
            intelString.Append(_propertyVisitor.Visit("resistanceEast", intel.ResistanceEast));
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
                itemsString.Append(getItemString("Item", subItem));
            }

            itemsString.Append("};\n");

            return itemsString.ToString();
        }

        public string Visit(string elementName, List<Vehicle> vehicles)
        {
            return Visit(elementName, vehicles.Cast<ItemBase>().ToList(), (itemName, item) => Visit(itemName, (Vehicle) item));
        }

        public string Visit(string elementName, List<Marker> markers)
        {
            return Visit(elementName, markers.Cast<ItemBase>().ToList(), (itemName, item) => Visit(itemName, (Marker) item));
        }

        public string Visit(string elementName, List<Sensor> sensors)
        {
            return Visit(elementName, sensors.Cast<ItemBase>().ToList(), (itemName, item) => Visit(itemName, (Sensor) item));
        }

        public string Visit(string elementName, Vehicle vehicle)
        {
            if(vehicle == null)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("class " + elementName + vehicle.Number + "\n");
            stringBuilder.Append("{\n");
            stringBuilder.Append(_propertyVisitor.Visit("position", vehicle.Position));
            stringBuilder.Append(_propertyVisitor.Visit("azimut", vehicle.Azimut));
            stringBuilder.Append(_propertyVisitor.Visit("special", vehicle.Special));
            stringBuilder.Append(_propertyVisitor.Visit("id", vehicle.Id));
            stringBuilder.Append(_propertyVisitor.Visit("side", vehicle.Side));
            stringBuilder.Append(_propertyVisitor.Visit("vehicle", vehicle.VehicleName));
            stringBuilder.Append(_propertyVisitor.Visit("player", vehicle.Player));
            stringBuilder.Append(_propertyVisitor.Visit("leader", vehicle.Leader));
            stringBuilder.Append(_propertyVisitor.Visit("rank", vehicle.Rank));
            stringBuilder.Append(_propertyVisitor.Visit("lock", vehicle.Lock));
            stringBuilder.Append(_propertyVisitor.Visit("skill", vehicle.Skill));
            stringBuilder.Append(_propertyVisitor.Visit("health", vehicle.Health));
            stringBuilder.Append(_propertyVisitor.Visit("fuel", vehicle.Fuel));
            stringBuilder.Append(_propertyVisitor.Visit("text", vehicle.Text));
            stringBuilder.Append(_propertyVisitor.Visit("init", vehicle.Init));
            stringBuilder.Append(_propertyVisitor.Visit("description", vehicle.Description));
            stringBuilder.Append(_propertyVisitor.Visit("synchronizations", vehicle.Synchronizations));

            stringBuilder.Append(Visit("Vehicles", vehicle.Vehicles));

            stringBuilder.Append("};\n");

            return stringBuilder.ToString();
        }

        public string Visit(string elementName, Marker marker)
        {
            if(marker == null)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("class " + elementName + marker.Number + "\n");
            stringBuilder.Append("{\n");
            stringBuilder.Append(_propertyVisitor.Visit("position", marker.Position));
            stringBuilder.Append(_propertyVisitor.Visit("name", marker.Name));
            stringBuilder.Append(_propertyVisitor.Visit("text", marker.Text));
            stringBuilder.Append(_propertyVisitor.Visit("markerType", marker.MarkerType));
            stringBuilder.Append(_propertyVisitor.Visit("type", marker.Type));
            stringBuilder.Append(_propertyVisitor.Visit("colorName", marker.ColorName));
            stringBuilder.Append(_propertyVisitor.Visit("fillName", marker.FillName));
            stringBuilder.Append(_propertyVisitor.Visit("a", marker.A));
            stringBuilder.Append(_propertyVisitor.Visit("b", marker.B));
            stringBuilder.Append(_propertyVisitor.Visit("drawBorder", marker.DrawBorder));
            stringBuilder.Append(_propertyVisitor.Visit("angle", marker.Angle));
            stringBuilder.Append("};\n");

            return stringBuilder.ToString();
        }

        public string Visit(string elementName, Sensor sensor)
        {
            if(sensor == null)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("class " + elementName + sensor.Number + "\n");
            stringBuilder.Append("{\n");
            stringBuilder.Append(_propertyVisitor.Visit("position", sensor.Position));
            stringBuilder.Append(_propertyVisitor.Visit("a", sensor.A));
            stringBuilder.Append(_propertyVisitor.Visit("b", sensor.B));
            stringBuilder.Append(_propertyVisitor.Visit("activationBy", sensor.ActivationBy));
            stringBuilder.Append(_propertyVisitor.Visit("interruptable", sensor.Interruptable));
            stringBuilder.Append(_propertyVisitor.Visit("type", sensor.Type));
            stringBuilder.Append(_propertyVisitor.Visit("age", sensor.Age));
            stringBuilder.Append(_propertyVisitor.Visit("expCond", sensor.ExpCond));
            stringBuilder.Append(_propertyVisitor.Visit("expActiv", sensor.ExpActiv));
            //itemString.Append(Visit("Effects", item.Effects));
            stringBuilder.Append("};\n");

            return stringBuilder.ToString();
        }
    }
}
