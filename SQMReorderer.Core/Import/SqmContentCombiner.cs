using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Import
{
    public class SqmContentCombiner : ISqmContentCombiner
    {
        public SqmContents Combine(ArmA2.ResultObjects.SqmContents arma2Contents)
        {
            return new SqmContents()
                {
                    Version = arma2Contents.Version,
                    Mission = Combine(arma2Contents.Mission),
                    Intro = Combine(arma2Contents.Intro),
                    OutroWin = Combine(arma2Contents.OutroWin),
                    OutroLose = Combine(arma2Contents.OutroLose)
                };
        }

        private MissionState Combine(ArmA2.ResultObjects.MissionState arma2MissionState)
        {
            if (arma2MissionState == null)
            {
                return null;
            }

            return new MissionState()
                {
                    AddOns = arma2MissionState.AddOns,
                    AddOnsAuto = arma2MissionState.AddOnsAuto,
                    RandomSeed = arma2MissionState.RandomSeed,
                    Intel = Combine(arma2MissionState.Intel),
                    Groups = Combine(arma2MissionState.Groups),
                    Vehicles = Combine(arma2MissionState.Vehicles),
                    Markers = Combine(arma2MissionState.Markers),
                    Sensors = Combine(arma2MissionState.Sensors)
                };
        }

        private Intel Combine(ArmA2.ResultObjects.Intel arma2Intel)
        {
            if (arma2Intel == null)
            {
                return null;
            }

            return new Intel()
                {
                    BriefingName = arma2Intel.BriefingName,
                    BriefingDescription = arma2Intel.BriefingDescription,
                    StartWeather = arma2Intel.StartWeather,
                    ForecastWeather = arma2Intel.ForecastWeather,
                    Year = arma2Intel.Year,
                    Month = arma2Intel.Month,
                    Day = arma2Intel.Day,
                    Hour = arma2Intel.Hour,
                    Minute = arma2Intel.Minute
                };
        }

        private List<Vehicle> Combine(List<ArmA2.ResultObjects.Vehicle> arma2Vehicles)
        {
            return arma2Vehicles.Select(Combine).ToList();
        }

        private Vehicle Combine(ArmA2.ResultObjects.Vehicle vehicle)
        {
            return new Vehicle()
                {
                    Number = vehicle.Number,
                    Position = vehicle.Position,
                    Azimut = vehicle.Azimut,
                    Id = vehicle.Id,
                    Side = vehicle.Side,
                    VehicleName = vehicle.VehicleName,
                    Player = vehicle.Player,
                    Leader = vehicle.Leader,
                    Rank = vehicle.Rank,
                    Lock = vehicle.Lock,
                    Skill = vehicle.Skill,
                    Text = vehicle.Text,
                    Init = vehicle.Init,
                    Description = vehicle.Description,
                    Synchronizations = vehicle.Synchronizations,
                    Vehicles = Combine(vehicle.Vehicles),
                };
        }

        private List<Marker> Combine(List<ArmA2.ResultObjects.Marker> markers)
        {
            return markers.Select(Combine).ToList();
        }

        private Marker Combine(ArmA2.ResultObjects.Marker marker)
        {
            return new Marker()
                {
                    Number = marker.Number,
                    Position = marker.Position,
                    Text = marker.Text,
                    Name = marker.Name,
                    MarkerType = marker.MarkerType,
                    Type = marker.Type,
                    FillName = marker.FillName,
                    A = marker.A,
                    B = marker.B,
                    DrawBorder = marker.DrawBorder,
                    Angle = marker.Angle,
                };
        }

        private List<Sensor> Combine(List<ArmA2.ResultObjects.Sensor> sensors)
        {
            return sensors.Select(Combine).ToList();
        }

        private Sensor Combine(ArmA2.ResultObjects.Sensor sensor)
        {
            return new Sensor()
                {
                    Number = sensor.Number,
                    Position = sensor.Position,
                    A = sensor.A,
                    B = sensor.B,
                    Type = sensor.Type,
                    ActivationBy = sensor.ActivationBy,
                    Interruptable = sensor.Interruptable,
                    Age = sensor.Age,
                    ExpCond = sensor.ExpCond,
                    ExpActiv = sensor.ExpActiv,
                };
        }

        public SqmContents Combine(ArmA3.ResultObjects.SqmContents arma3Contents)
        {
            throw new System.NotImplementedException();
        }
    }
}