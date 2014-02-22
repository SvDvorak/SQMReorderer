﻿using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class SqmContentCombinerTests_arma_3
    {
        private SqmContentCombiner _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new SqmContentCombiner();
        }

        [Test]
        public void Sets_sqm_contents_to_correct_arma_3_values()
        {
            var arma3Contents = new SqmContents
                {
                    Version = 11,
                    Mission = new MissionState { RandomSeed = 1 },
                    Intro = new MissionState { RandomSeed = 2 },
                    OutroWin = new MissionState { RandomSeed = 3 },
                    OutroLose = new MissionState { RandomSeed = 4 }
                };

            var sqmContents = _sut.Combine(arma3Contents);

            Assert.AreEqual(11, sqmContents.Version);
            Assert.AreEqual(1, sqmContents.Mission.RandomSeed);
            Assert.AreEqual(2, sqmContents.Intro.RandomSeed);
            Assert.AreEqual(3, sqmContents.OutroWin.RandomSeed);
            Assert.AreEqual(4, sqmContents.OutroLose.RandomSeed);
        }

        [Test]
        public void Contents_are_null_if_not_set_in_arma_3_contents()
        {
            var sqmContents = _sut.Combine(new SqmContents());

            Assert.AreEqual(null, sqmContents.Version);
            Assert.IsNull(sqmContents.Mission);
            Assert.IsNull(sqmContents.Intro);
            Assert.IsNull(sqmContents.OutroWin);
            Assert.IsNull(sqmContents.OutroLose);
        }

        [Test]
        public void Sets_mission_state_to_correct_arma_3_values()
        {
            var missionState = new MissionState
                {
                    AddOns = { "addon" },
                    AddOnsAuto = { "auto" },
                    RandomSeed = 11,
                    Intel = new Intel { BriefingName = "name" },
                    Groups = new List<Vehicle> { new Vehicle { VehicleName = "groupName" } },
                    Vehicles = new List<Vehicle> { new Vehicle { VehicleName = "vehicleName" } },
                    Markers = new List<Marker> { new Marker { A = 1 } },
                    Sensors = new List<Sensor> { new Sensor { A = 2 } }
                };

            var sqmContents = _sut.Combine(CreateContents(missionState));

            Assert.AreEqual("addon", sqmContents.Mission.AddOns[0]);
            Assert.AreEqual("auto", sqmContents.Mission.AddOnsAuto[0]);
            Assert.AreEqual(11, sqmContents.Mission.RandomSeed);
            Assert.AreEqual("name", sqmContents.Mission.Intel.BriefingName);
            Assert.AreEqual("groupName", sqmContents.Mission.Groups[0].VehicleName);
            Assert.AreEqual("vehicleName", sqmContents.Mission.Vehicles[0].VehicleName);
            Assert.AreEqual(1, sqmContents.Mission.Markers[0].A);
            Assert.AreEqual(2, sqmContents.Mission.Sensors[0].A);
        }

        [Test]
        public void Mission_state_contents_are_null_if_not_set_in_arma_3_mission_state()
        {
            var sqmContents = _sut.Combine(CreateContents(new MissionState()));

            var newMission = sqmContents.Mission;
            Assert.AreEqual(null, newMission.RandomSeed);
            Assert.IsEmpty(newMission.AddOns);
            Assert.IsEmpty(newMission.AddOnsAuto);
            Assert.IsNull(newMission.Intel);
            Assert.IsEmpty(newMission.Groups);
            Assert.IsEmpty(newMission.Vehicles);
            Assert.IsEmpty(newMission.Markers);
            Assert.IsEmpty(newMission.Sensors);
        }

        [Test]
        public void Sets_intel_to_correct_arma_3_values()
        {
            var intel = new Intel
                {
                    BriefingName = "name",
                    OverviewText = "overview",
                    TimeOfChanges = 1,
                    StartWeather = 10,
                    StartWind = 2,
                    StartWaves = 3,
                    ForecastWeather = 11,
                    ForecastWind = 4,
                    ForecastWaves = 5,
                    ForecastLightnings = 6,
                    RainForced = 7,
                    LightningsForced = 8,
                    WavesForced = 9,
                    WindForced = 10,
                    Year = 1,
                    Month = 2,
                    Day = 3,
                    Hour = 4,
                    Minute = 5,
                    StartFogDecay = 11,
                    ForecastFogDecay = 12
                };

            var sqmContents = _sut.Combine(CreateContents(intel));

            var newIntel = sqmContents.Mission.Intel;
            Assert.AreEqual("name", newIntel.BriefingName);
            Assert.AreEqual("overview", newIntel.OverviewText);
            Assert.AreEqual(1, newIntel.TimeOfChanges);
            Assert.AreEqual(10, newIntel.StartWeather);
            Assert.AreEqual(2, newIntel.StartWind);
            Assert.AreEqual(3, newIntel.StartWaves);
            Assert.AreEqual(11, newIntel.ForecastWeather);
            Assert.AreEqual(4, newIntel.ForecastWind);
            Assert.AreEqual(5, newIntel.ForecastWaves);
            Assert.AreEqual(6, newIntel.ForecastLightnings);
            Assert.AreEqual(7, newIntel.RainForced);
            Assert.AreEqual(8, newIntel.LightningsForced);
            Assert.AreEqual(9, newIntel.WavesForced);
            Assert.AreEqual(10, newIntel.WindForced);
            Assert.AreEqual(1, newIntel.Year);
            Assert.AreEqual(2, newIntel.Month);
            Assert.AreEqual(3, newIntel.Day);
            Assert.AreEqual(4, newIntel.Hour);
            Assert.AreEqual(5, newIntel.Minute);
            Assert.AreEqual(11, newIntel.StartFogDecay);
            Assert.AreEqual(12, newIntel.ForecastFogDecay);
        }

        [Test]
        public void Intel_contents_are_null_if_not_set_in_arma_3_intel()
        {
            var sqmContents = _sut.Combine(CreateContents(new Intel()));

            var newIntel = sqmContents.Mission.Intel;
            Assert.IsNull(newIntel.BriefingName);
            Assert.IsNull(newIntel.OverviewText);
            Assert.IsNull(newIntel.BriefingDescription);
            Assert.IsNull(newIntel.StartWeather);
            Assert.IsNull(newIntel.ForecastWeather);
            Assert.IsNull(newIntel.Year);
            Assert.IsNull(newIntel.Month);
            Assert.IsNull(newIntel.Day);
            Assert.IsNull(newIntel.Hour);
            Assert.IsNull(newIntel.Minute);
        }

        [Test]
        public void Sets_vehicle_to_correct_arma_3_values()
        {
            var vehicle = new Vehicle
                {
                    Azimut = 1,
                    Id = 2,
                    Side = "side",
                    VehicleName = "name",
                    Player = "player",
                    Leader = 3,
                    Rank = "rank",
                    Lock = "lock",
                    Skill = 4,
                    Health = 4,
                    Text = "text",
                    Init = "init",
                    Description = "desc",
                    Synchronizations = { 5 },
                    Vehicles = new List<Vehicle> { new Vehicle { Azimut = 6 } },
                    Number = 7,
                    Position = new Vector(8, 9, 10)
                };

            var sqmContents = _sut.Combine(CreateContents(vehicle));

            var newVehicle = sqmContents.Mission.Groups[0];
            Assert.AreEqual(1, newVehicle.Azimut);
            Assert.AreEqual(2, newVehicle.Id);
            Assert.AreEqual("side", newVehicle.Side);
            Assert.AreEqual("name", newVehicle.VehicleName);
            Assert.AreEqual("player", newVehicle.Player);
            Assert.AreEqual(3, newVehicle.Leader);
            Assert.AreEqual("rank", newVehicle.Rank);
            Assert.AreEqual("lock", newVehicle.Lock);
            Assert.AreEqual(4, newVehicle.Skill);
            Assert.AreEqual(4, newVehicle.Health);
            Assert.AreEqual("text", newVehicle.Text);
            Assert.AreEqual("init", newVehicle.Init);
            Assert.AreEqual("desc", newVehicle.Description);
            Assert.AreEqual(5, newVehicle.Synchronizations[0]);
            Assert.AreEqual(6, newVehicle.Vehicles[0].Azimut);
            Assert.AreEqual(7, newVehicle.Number);
            Assert.AreEqual(new Vector(8, 9, 10), newVehicle.Position);
        }

        [Test]
        public void Vehicle_contents_are_null_if_not_set_in_arma_3_vehicle()
        {
            var sqmContents = _sut.Combine(CreateContents(new Vehicle()));

            var newVehicle = sqmContents.Mission.Groups[0];
            Assert.IsNull(newVehicle.Azimut);
            Assert.IsNull(newVehicle.Id);
            Assert.IsNull(newVehicle.Side);
            Assert.IsNull(newVehicle.VehicleName);
            Assert.IsNull(newVehicle.Player);
            Assert.IsNull(newVehicle.Leader);
            Assert.IsNull(newVehicle.Rank);
            Assert.IsNull(newVehicle.Lock);
            Assert.IsNull(newVehicle.Skill);
            Assert.IsNull(newVehicle.Text);
            Assert.IsNull(newVehicle.Init);
            Assert.IsNull(newVehicle.Description);
            Assert.IsEmpty(newVehicle.Synchronizations);
            Assert.IsEmpty(newVehicle.Vehicles);
            Assert.AreEqual(0, newVehicle.Number);
            Assert.IsNull(newVehicle.Position);
        }

        [Test]
        public void Sets_marker_to_correct_arma_3_values()
        {
            var marker = new Marker
                {
                    Text = "text",
                    Name = "name",
                    MarkerType = "markerType",
                    Type = "type",
                    FillName = "fillName",
                    A = 1,
                    B = 2,
                    DrawBorder = 3,
                    Angle = 4,
                    Number = 5,
                    Position = new Vector(6, 7, 8)
                };

            var sqmContents = _sut.Combine(CreateContents(marker));

            var newMarker = sqmContents.Mission.Markers[0];
            Assert.AreEqual("text", newMarker.Text);
            Assert.AreEqual("name", newMarker.Name);
            Assert.AreEqual("markerType", newMarker.MarkerType);
            Assert.AreEqual("type", newMarker.Type);
            Assert.AreEqual("fillName", newMarker.FillName);
            Assert.AreEqual(1, newMarker.A);
            Assert.AreEqual(2, newMarker.B);
            Assert.AreEqual(3, newMarker.DrawBorder);
            Assert.AreEqual(4, newMarker.Angle);
            Assert.AreEqual(5, newMarker.Number);
            Assert.AreEqual(new Vector(6, 7, 8), newMarker.Position);
        }

        [Test]
        public void Marker_contents_are_null_if_not_set_in_arma_3_marker()
        {
            var sqmContents = _sut.Combine(CreateContents(new Marker()));

            var newMarker = sqmContents.Mission.Markers[0];
            Assert.IsNull(newMarker.Text);
            Assert.IsNull(newMarker.Name);
            Assert.IsNull(newMarker.MarkerType);
            Assert.IsNull(newMarker.Type);
            Assert.IsNull(newMarker.FillName);
            Assert.IsNull(newMarker.A);
            Assert.IsNull(newMarker.B);
            Assert.IsNull(newMarker.DrawBorder);
            Assert.IsNull(newMarker.Angle);
            Assert.AreEqual(0, newMarker.Number);
            Assert.IsNull(newMarker.Position);
        }

        [Test]
        public void Sets_sensor_to_correct_arma_3_values()
        {
            var sensor = new Sensor
                {
                    A = 1,
                    B = 2,
                    Type = "type",
                    ActivationBy = "act",
                    Interruptable = 3,
                    Age = "age",
                    ExpCond = "cond",
                    ExpActiv = "activ",
                    Number = 4,
                    Position = new Vector(5, 6, 7)
                };

            var sqmContents = _sut.Combine(CreateContents(sensor));

            var newSensor = sqmContents.Mission.Sensors[0];
            Assert.AreEqual(1, newSensor.A);
            Assert.AreEqual(2, newSensor.B);
            Assert.AreEqual("type", newSensor.Type);
            Assert.AreEqual("act", newSensor.ActivationBy);
            Assert.AreEqual(3, newSensor.Interruptable);
            Assert.AreEqual("age", newSensor.Age);
            Assert.AreEqual("cond", newSensor.ExpCond);
            Assert.AreEqual("activ", newSensor.ExpActiv);
            Assert.AreEqual(4, newSensor.Number);
            Assert.AreEqual(new Vector(5, 6, 7), newSensor.Position);
        }

        [Test]
        public void Sensor_contents_are_null_if_not_set_in_arma_3_sensor()
        {
            var sqmContents = _sut.Combine(CreateContents(new Sensor()));

            var newSensor = sqmContents.Mission.Sensors[0];
            Assert.IsNull(newSensor.A);
            Assert.IsNull(newSensor.B);
            Assert.IsNull(newSensor.Type);
            Assert.IsNull(newSensor.ActivationBy);
            Assert.IsNull(newSensor.Interruptable);
            Assert.IsNull(newSensor.Age);
            Assert.IsNull(newSensor.ExpCond);
            Assert.IsNull(newSensor.ExpActiv);
            Assert.AreEqual(0, newSensor.Number);
            Assert.IsNull(newSensor.Position);
        }

        private SqmContents CreateContents(MissionState missionState)
        {
            return new SqmContents
                {
                    Mission = missionState
                };
        }

        private SqmContents CreateContents(Intel intel)
        {
            return new SqmContents
                {
                    Mission = new MissionState
                        {
                            Intel = intel
                        }
                };
        }

        private SqmContents CreateContents(Vehicle vehicle)
        {
            return new SqmContents
                {
                    Mission = new MissionState
                        {
                            Groups = new List<Vehicle>
                                {
                                    vehicle
                                }
                        }
                };
        }

        private SqmContents CreateContents(Marker marker)
        {
            return new SqmContents
                {
                    Mission = new MissionState
                        {
                            Markers = new List<Marker>
                                {
                                    marker
                                }
                        }
                };
        }

        private SqmContents CreateContents(Sensor sensor)
        {
            return new SqmContents
                {
                    Mission = new MissionState
                        {
                            Sensors = new List<Sensor>
                                {
                                    sensor
                                }
                        }
                };
        }
    }
}