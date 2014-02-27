using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Import
{
    public class SqmContentCombiner : ISqmContentCombiner
    {
        public SqmContents Combine(ArmA2.ResultObjects.SqmContents arma2Contents)
        {
            return new SqmContents
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

            return new MissionState
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

            return new Intel
                {
                    BriefingName = arma2Intel.BriefingName,
                    BriefingDescription = arma2Intel.BriefingDescription,
                    ResistanceWest = arma2Intel.ResistanceWest,
                    ResistanceEast = arma2Intel.ResistanceEast,
                    StartWeather = arma2Intel.StartWeather,
                    StartFog = arma2Intel.StartFog,
                    ForecastWeather = arma2Intel.ForecastWeather,
                    ForecastFog = arma2Intel.ForecastFog,
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
            return new Vehicle
                {
                    Presence = vehicle.Presence,
                    PresenceCondition = vehicle.PresenceCondition,
                    Number = vehicle.Number,
                    Position = vehicle.Position,
                    Placement = vehicle.Placement,
                    Azimut = vehicle.Azimut,
                    Special = vehicle.Special,
                    Age = vehicle.Age,
                    Id = vehicle.Id,
                    Side = vehicle.Side,
                    VehicleName = vehicle.VehicleName,
                    Player = vehicle.Player,
                    Leader = vehicle.Leader,
                    Rank = vehicle.Rank,
                    Lock = vehicle.Lock,
                    Skill = vehicle.Skill,
                    Health = vehicle.Health,
                    Fuel = vehicle.Fuel,
                    Ammo = vehicle.Ammo,
                    Text = vehicle.Text,
                    Markers = vehicle.Markers,
                    IsMarkersSingleLine = vehicle.IsMarkersSingleLine,
                    Init = vehicle.Init,
                    Description = vehicle.Description,
                    Synchronizations = vehicle.Synchronizations,
                    Vehicles = Combine(vehicle.Vehicles),
                    Waypoints = Combine(vehicle.Waypoints)
                };
        }

        private List<Marker> Combine(List<ArmA2.ResultObjects.Marker> markers)
        {
            return markers.Select(Combine).ToList();
        }

        private List<Waypoint> Combine(List<ArmA2.ResultObjects.Waypoint> waypoints)
        {
            return waypoints.Select(Combine).ToList();
        }

        private Waypoint Combine(ArmA2.ResultObjects.Waypoint waypoint)
        {
            return new Waypoint
                {
                    Number = waypoint.Number,
                    Position = waypoint.Position,
                    Id = waypoint.Id,
                    IdStatic = waypoint.IdStatic,
                    IdObject = waypoint.IdObject,
                    HousePos = waypoint.HousePos,
                    Placement = waypoint.Placement,
                    CompletitionRadius = waypoint.CompletitionRadius,
                    Type = waypoint.Type,
                    CombatMode = waypoint.CombatMode,
                    Formation = waypoint.Formation,
                    Speed = waypoint.Speed,
                    Combat = waypoint.Combat,
                    Description = waypoint.Description,
                    Visible = waypoint.Visible,
                    ExpCond = waypoint.ExpCond,
                    ExpActiv = waypoint.ExpActiv,
                    Synchronizations = waypoint.Synchronizations,
                    Effects = waypoint.Effects,
                    TimeoutMin = waypoint.TimeoutMin,
                    TimeoutMid = waypoint.TimeoutMid,
                    TimeoutMax = waypoint.TimeoutMax,
                    ShowWp = waypoint.ShowWp
                };
        }

        private Marker Combine(ArmA2.ResultObjects.Marker marker)
        {
            return new Marker
                {
                    Number = marker.Number,
                    Position = marker.Position,
                    Text = marker.Text,
                    Name = marker.Name,
                    MarkerType = marker.MarkerType,
                    Type = marker.Type,
                    ColorName = marker.ColorName,
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
            return new Sensor
                {
                    Number = sensor.Number,
                    Position = sensor.Position,
                    A = sensor.A,
                    B = sensor.B,
                    Angle = sensor.Angle,
                    Rectangular = sensor.Rectangular,
                    Type = sensor.Type,
                    ActivationBy = sensor.ActivationBy,
                    ActivationType = sensor.ActivationType,
                    Repeating = sensor.Repeating,
                    TimeoutMin = sensor.TimeoutMin,
                    TimeoutMid = sensor.TimeoutMid,
                    TimeoutMax = sensor.TimeoutMax,
                    Interruptable = sensor.Interruptable,
                    Age = sensor.Age,
                    Text = sensor.Text,
                    Name = sensor.Name,
                    IdStatic = sensor.IdStatic,
                    IdVehicle = sensor.IdVehicle,
                    IdObject = sensor.IdObject,
                    ExpCond = sensor.ExpCond,
                    ExpActiv = sensor.ExpActiv,
                    ExpDesactiv = sensor.ExpDesactiv,
                    Effects = sensor.Effects,
                    Synchronizations = sensor.Synchronizations
                };
        }

        public SqmContents Combine(ArmA3.ResultObjects.SqmContents arma3Contents)
        {
            return new SqmContents
                {
                    Version = arma3Contents.Version,
                    Mission = Combine(arma3Contents.Mission),
                    Intro = Combine(arma3Contents.Intro),
                    OutroWin = Combine(arma3Contents.OutroWin),
                    OutroLose = Combine(arma3Contents.OutroLose)
                };
        }

        private MissionState Combine(ArmA3.ResultObjects.MissionState arma3MissionState)
        {
            if (arma3MissionState == null)
            {
                return null;
            }

            return new MissionState
                {
                    AddOns = arma3MissionState.AddOns,
                    AddOnsAuto = arma3MissionState.AddOnsAuto,
                    RandomSeed = arma3MissionState.RandomSeed,
                    Intel = Combine(arma3MissionState.Intel),
                    Groups = Combine(arma3MissionState.Groups),
                    Vehicles = Combine(arma3MissionState.Vehicles),
                    Markers = Combine(arma3MissionState.Markers),
                    Sensors = Combine(arma3MissionState.Sensors)
                };
        }

        private Intel Combine(ArmA3.ResultObjects.Intel arma3Intel)
        {
            if (arma3Intel == null)
            {
                return null;
            }

            return new Intel
                {
                    BriefingName = arma3Intel.BriefingName,
                    OverviewText = arma3Intel.OverviewText,
                    TimeOfChanges = arma3Intel.TimeOfChanges,
                    StartWeather = arma3Intel.StartWeather,
                    StartWind = arma3Intel.StartWind,
                    StartWaves = arma3Intel.StartWaves,
                    ForecastWeather = arma3Intel.ForecastWeather,
                    ForecastWind = arma3Intel.ForecastWind,
                    ForecastWaves = arma3Intel.ForecastWaves,
                    ForecastLightnings = arma3Intel.ForecastLightnings,
                    RainForced = arma3Intel.RainForced,
                    LightningsForced = arma3Intel.LightningsForced,
                    WavesForced = arma3Intel.WavesForced,
                    WindForced = arma3Intel.WindForced,
                    Year = arma3Intel.Year,
                    Month = arma3Intel.Month,
                    Day = arma3Intel.Day,
                    Hour = arma3Intel.Hour,
                    Minute = arma3Intel.Minute,
                    StartFogBase = arma3Intel.StartFogBase,
                    ForecastFogBase = arma3Intel.ForecastFogBase,
                    StartFogDecay = arma3Intel.StartFogDecay,
                    ForecastFogDecay = arma3Intel.ForecastFogDecay
                };
        }

        private List<Vehicle> Combine(List<ArmA3.ResultObjects.Vehicle> arma3Vehicles)
        {
            return arma3Vehicles.Select(Combine).ToList();
        }

        private Vehicle Combine(ArmA3.ResultObjects.Vehicle vehicle)
        {
            return new Vehicle
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
                    Health = vehicle.Health,
                    Ammo = vehicle.Ammo,
                    Text = vehicle.Text,
                    Init = vehicle.Init,
                    Description = vehicle.Description,
                    Synchronizations = vehicle.Synchronizations,
                    Vehicles = Combine(vehicle.Vehicles),
                    Waypoints = Combine(vehicle.Waypoints)
                };
        }

        private List<Waypoint> Combine(List<ArmA3.ResultObjects.Waypoint> waypoints)
        {
            return waypoints.Select(Combine).ToList();
        }

        private Waypoint Combine(ArmA3.ResultObjects.Waypoint waypoint)
        {
            return new Waypoint
            {
                Number = waypoint.Number,
                Position = waypoint.Position,
                ExpActiv = waypoint.ExpActiv,
                Effects = waypoint.Effects,
                ShowWp = waypoint.ShowWp
            };
        }

        private List<Marker> Combine(List<ArmA3.ResultObjects.Marker> markers)
        {
            return markers.Select(Combine).ToList();
        }

        private Marker Combine(ArmA3.ResultObjects.Marker marker)
        {
            return new Marker
                {
                    Number = marker.Number,
                    Position = marker.Position,
                    Text = marker.Text,
                    Name = marker.Name,
                    MarkerType = marker.MarkerType,
                    Type = marker.Type,
                    ColorName = marker.ColorName,
                    FillName = marker.FillName,
                    A = marker.A,
                    B = marker.B,
                    DrawBorder = marker.DrawBorder,
                    Angle = marker.Angle,
                };
        }

        private List<Sensor> Combine(List<ArmA3.ResultObjects.Sensor> sensors)
        {
            return sensors.Select(Combine).ToList();
        }

        private Sensor Combine(ArmA3.ResultObjects.Sensor sensor)
        {
            return new Sensor
                {
                    Number = sensor.Number,
                    Position = sensor.Position,
                    A = sensor.A,
                    B = sensor.B,
                    Type = sensor.Type,
                    ActivationBy = sensor.ActivationBy,
                    ActivationType = sensor.ActivationType,
                    Interruptable = sensor.Interruptable,
                    Age = sensor.Age,
                    ExpCond = sensor.ExpCond,
                    ExpActiv = sensor.ExpActiv,
                    Effects = sensor.Effects
                };
        }
    }
}