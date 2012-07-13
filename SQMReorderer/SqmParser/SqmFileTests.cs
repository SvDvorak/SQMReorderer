﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SQMReorderer.SqmParser
{
    [TestFixture]
    public class SqmFileTests
    {
        string[] testFileText = new[]
        {
            @"version=11;\n",
            @"class Mission\n",
            @"{\n",
                @"addOns[]=\n",
                @"{\n",
                    @"""cacharacters_e"",\n",
                    @"""zargabad"",\n",
                    @"""ca_highcommand"",\n",
                    @"""ca_modules_marta"",\n",
                    @"""ca_missions_firstaidsystem"",\n",
                    @"""ca_modules_functions"",\n",
                    @"""CACharacters_BAF"",\n",
                    @"""cacharacters2"",\n",
                    @"""caweapons_e"",\n",
                    @"""caweapons_e_ammoboxes"",\n",
                    @"""CAWheeled_E""\n",
                @"};\n",
                @"addOnsAuto[]=\n",
                @"{\n",
                    @"""ca_modules_functions"",\n",
                    @"""cacharacters_e"",\n",
                    @"""CAWheeled_E"",\n",
                    @"""caweapons_e_ammoboxes"",\n",
                    @"""zargabad""\n",
                @"};\n",
                @"randomSeed=4931020;\n",
                @"class Intel\n",
                @"{\n",
                    @"briefingName=""[co04]local_hostility_v2_oa"";\n",
                    @"briefingDescription=""Destroy stolen ammocrates and truck"";\n",
                    @"startWeather=0.19207704;\n",
                    @"forecastWeather=0.25;\n",
                    @"year=2008;\n",
                    @"month=10;\n",
                    @"day=11;\n",
                    @"hour=16;\n",
                    @"minute=0;\n",
                @"};\n",
                @"class Groups\n",
                @"{\n",
                    @"items=9;\n",
                    @"class Item0\n",
                    @"{\n",
                        @"side=""LOGIC"";\n",
                        @"class Vehicles\n",
                        @"{\n",
                            @"items=1;\n",
                            @"class Item0\n",
                            @"{\n",
                                @"position[]={4878.6553,18.448904,4201.6475};\n",
                                @"id=0;\n",
                                @"side=""LOGIC"";\n",
                                @"vehicle=""FunctionsManager"";\n",
                                @"leader=1;\n",
                                @"skill=0.60000002;\n",
                            @"};\n",
                        @"};\n",
                    @"};\n",
                    @"class Item1\n",
                    @"{\n",
                        @"side=""LOGIC"";\n",
                        @"class Vehicles\n",
                        @"{\n",
                            @"items=1;\n",
                            @"class Item0\n",
                            @"{\n",
                                @"position[]={4868.4067,18.852989,4323.0015};\n",
                                @"id=1;\n",
                                @"side=""LOGIC"";\n",
                                @"vehicle=""Logic"";\n",
                                @"leader=1;\n",
                                @"skill=0.60000002;\n",
                                @"text=""server"";\n",
                            @"};\n",
                        @"};\n",
                    @"};\n",
                    @"class Item2\n",
                    @"{\n",
                        @"side=""WEST"";\n",
                        @"class Vehicles\n",
                        @"{\n",
                            @"items=4;\n",
                            @"class Item0\n",
                            @"{\n",
                                @"position[]={4782.1846,28.662737,4513.792};\n",
                                @"azimut=-133.16071;\n",
                                @"id=2;\n",
                                @"side=""WEST"";\n",
                                @"vehicle=""US_Soldier_TL_EP1"";\n",
                                @"player=""PLAY CDG"";\n",
                                @"leader=1;\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.60000002;\n",
                                @"text=""UnitUS_Delta_FTL"";\n",
                                @"init=""GrpUS_Delta = group this; nul = [""ftl"",this] execVM ""f\common\folk_assignGear.sqf"";"";\n",
                                @"description=""US Army Delta Fireteam Leader"";\n",
                            @"};\n",
                            @"class Item1\n",
                            @"{\n",
                                @"position[]={4782.6973,28.779594,4517.5967};\n",
                                @"azimut=-133.16071;\n",
                                @"id=3;\n",
                                @"side=""WEST"";\n",
                                @"vehicle=""US_Soldier_AR_EP1"";\n",
                                @"player=""PLAY CDG"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.60000002;\n",
                                @"text=""UnitUS_Delta_AR"";\n",
                                @"init=""GrpUS_Delta = group this; nul = [""ar"",this] execVM ""f\common\folk_assignGear.sqf"";"";\n",
                                @"description=""US Army Delta Automatic Rifleman"";\n",
                            @"};\n",
                            @"class Item2\n",
                            @"{\n",
                                @"position[]={4781.1333,28.774311,4520.2017};\n",
                                @"azimut=-133.16071;\n",
                                @"id=4;\n",
                                @"side=""WEST"";\n",
                                @"vehicle=""US_Soldier_EP1"";\n",
                                @"player=""PLAY CDG"";\n",
                                @"skill=0.60000002;\n",
                                @"text=""UnitUS_Delta_AAR"";\n",
                                @"init=""GrpUS_Delta = group this; nul = [""aar"",this] execVM ""f\common\folk_assignGear.sqf"";"";\n",
                                @"description=""US Army Delta Assistant Automatic Rifleman"";\n",
                            @"};\n",
                            @"class Item3\n",
                            @"{\n",
                                @"position[]={4778.5933,28.645361,4522.2256};\n",
                                @"azimut=-133.16071;\n",
                                @"id=5;\n",
                                @"side=""WEST"";\n",
                                @"vehicle=""US_Soldier_LAT_EP1"";\n",
                                @"player=""PLAY CDG"";\n",
                                @"skill=0.60000002;\n",
                                @"text=""UnitUS_Delta_AT"";\n",
                                @"init=""GrpUS_Delta = group this; nul = [""rat"",this] execVM ""f\common\folk_assignGear.sqf"";"";\n",
                                @"description=""US Army Delta Rifleman (AT)"";\n",
                            @"};\n",
                        @"};\n",
                    @"};\n",
                    @"class Item3\n",
                    @"{\n",
                        @"side=""EAST"";\n",
                        @"class Vehicles\n",
                        @"{\n",
                            @"items=5;\n",
                            @"class Item0\n",
                            @"{\n",
                                @"position[]={4120.0903,16.782446,4156.9185};\n",
                                @"id=6;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_TL_EP1"";\n",
                                @"leader=1;\n",
                                @"rank=""SERGEANT"";\n",
                                @"skill=0.46666664;\n",
                                @"init=""nul=[this,""TargetAreaCenter"",""nomove"",""delete:"",300] execVM ""scripts\upsmon.sqf"";"";\n",
                            @"};\n",
                            @"class Item1\n",
                            @"{\n",
                                @"position[]={4123.0903,16.759304,4151.9185};\n",
                                @"id=7;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_MG_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item2\n",
                            @"{\n",
                                @"position[]={4125.0903,16.773489,4151.9185};\n",
                                @"id=8;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_2_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item3\n",
                            @"{\n",
                                @"position[]={4127.0903,16.788488,4151.9185};\n",
                                @"id=9;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_AT_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item4\n",
                            @"{\n",
                                @"position[]={4129.0903,16.798851,4151.9185};\n",
                                @"id=10;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                        @"};\n",
                    @"};\n",
                    @"class Item4\n",
                    @"{\n",
                        @"side=""EAST"";\n",
                        @"class Vehicles\n",
                        @"{\n",
                            @"items=5;\n",
                            @"class Item0\n",
                            @"{\n",
                                @"position[]={4283.1426,17.532404,4095.6787};\n",
                                @"id=11;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_TL_EP1"";\n",
                                @"leader=1;\n",
                                @"rank=""SERGEANT"";\n",
                                @"skill=0.46666664;\n",
                                @"init=""nul=[this,""TargetAreaLarge"",""random"",""delete:"",300] execVM ""scripts\upsmon.sqf"";"";\n",
                            @"};\n",
                            @"class Item1\n",
                            @"{\n",
                                @"position[]={4286.1426,17.65732,4090.6787};\n",
                                @"id=12;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_MG_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item2\n",
                            @"{\n",
                                @"position[]={4288.1426,17.666964,4090.6787};\n",
                                @"id=13;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_2_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item3\n",
                            @"{\n",
                                @"position[]={4290.1426,17.671963,4090.6787};\n",
                                @"id=14;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_AT_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item4\n",
                            @"{\n",
                                @"position[]={4292.1426,17.676964,4090.6787};\n",
                                @"id=15;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                        @"};\n",
                    @"};\n",
                    @"class Item5\n",
                    @"{\n",
                        @"side=""EAST"";\n",
                        @"class Vehicles\n",
                        @"{\n",
                            @"items=5;\n",
                            @"class Item0\n",
                            @"{\n",
                                @"position[]={4284.9419,17.030449,4217.7046};\n",
                                @"id=16;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_TL_EP1"";\n",
                                @"leader=1;\n",
                                @"rank=""SERGEANT"";\n",
                                @"skill=0.46666664;\n",
                                @"init=""nul=[this,""TargetAreaLarge"",""random"",""delete:"",300] execVM ""scripts\upsmon.sqf"";"";\n",
                            @"};\n",
                            @"class Item1\n",
                            @"{\n",
                                @"position[]={4287.9419,17.033234,4212.7046};\n",
                                @"id=17;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_MG_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item2\n",
                            @"{\n",
                                @"position[]={4289.9419,17.052942,4212.7046};\n",
                                @"id=18;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_2_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item3\n",
                            @"{\n",
                                @"position[]={4291.9419,17.066477,4212.7046};\n",
                                @"id=19;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_AT_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item4\n",
                            @"{\n",
                                @"position[]={4293.9419,17.027639,4212.7046};\n",
                                @"id=20;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                        @"};\n",
                    @"};\n",
                    @"class Item6\n",
                    @"{\n",
                        @"side=""EAST"";\n",
                        @"class Vehicles\n",
                        @"{\n",
                            @"items=5;\n",
                            @"class Item0\n",
                            @"{\n",
                                @"position[]={4044.6289,16.343838,4224.5786};\n",
                                @"id=24;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_TL_EP1"";\n",
                                @"leader=1;\n",
                                @"rank=""SERGEANT"";\n",
                                @"skill=0.46666664;\n",
                                @"init=""nul=[this,""TargetAreaLarge"",""random"",""delete:"",300] execVM ""scripts\upsmon.sqf"";"";\n",
                            @"};\n",
                            @"class Item1\n",
                            @"{\n",
                                @"position[]={4047.6289,16.387091,4219.5786};\n",
                                @"id=25;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_MG_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item2\n",
                            @"{\n",
                                @"position[]={4049.6289,16.400251,4219.5786};\n",
                                @"id=26;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_2_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item3\n",
                            @"{\n",
                                @"position[]={4051.6289,16.410252,4219.5786};\n",
                                @"id=27;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_AT_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item4\n",
                            @"{\n",
                                @"position[]={4053.6289,16.422216,4219.5786};\n",
                                @"id=28;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                        @"};\n",
                    @"};\n",
                    @"class Item7\n",
                    @"{\n",
                        @"side=""EAST"";\n",
                        @"class Vehicles\n",
                        @"{\n",
                            @"items=5;\n",
                            @"class Item0\n",
                            @"{\n",
                                @"position[]={4039.8225,16.476171,4092.6729};\n",
                                @"id=29;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_TL_EP1"";\n",
                                @"leader=1;\n",
                                @"rank=""SERGEANT"";\n",
                                @"skill=0.46666664;\n",
                                @"init=""nul=[this,""TargetAreaLarge"",""random"",""delete:"",300] execVM ""scripts\upsmon.sqf"";"";\n",
                            @"};\n",
                            @"class Item1\n",
                            @"{\n",
                                @"position[]={4042.8225,16.542454,4087.6729};\n",
                                @"id=30;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_MG_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item2\n",
                            @"{\n",
                                @"position[]={4044.8225,16.549463,4087.6729};\n",
                                @"id=31;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_2_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item3\n",
                            @"{\n",
                                @"position[]={4046.8225,16.574463,4087.6729};\n",
                                @"id=32;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_AT_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item4\n",
                            @"{\n",
                                @"position[]={4048.8225,16.594954,4087.6729};\n",
                                @"id=33;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                        @"};\n",
                    @"};\n",
                    @"class Item8\n",
                    @"{\n",
                        @"side=""EAST"";\n",
                        @"class Vehicles\n",
                        @"{\n",
                            @"items=5;\n",
                            @"class Item0\n",
                            @"{\n",
                                @"position[]={4148.5723,19.834879,4070.292};\n",
                                @"id=34;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_TL_EP1"";\n",
                                @"leader=1;\n",
                                @"rank=""SERGEANT"";\n",
                                @"skill=0.46666664;\n",
                                @"init=""nul=[this,""TargetAreaLarge"",""random"",""delete:"",300] execVM ""scripts\upsmon.sqf"";"";\n",
                            @"};\n",
                            @"class Item1\n",
                            @"{\n",
                                @"position[]={4151.5723,16.919485,4065.292};\n",
                                @"id=35;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_MG_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item2\n",
                            @"{\n",
                                @"position[]={4153.5723,16.914862,4065.292};\n",
                                @"id=36;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_2_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item3\n",
                            @"{\n",
                                @"position[]={4155.5723,16.922022,4065.292};\n",
                                @"id=37;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_AT_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                            @"class Item4\n",
                            @"{\n",
                                @"position[]={4157.5723,16.920231,4065.292};\n",
                                @"id=38;\n",
                                @"side=""EAST"";\n",
                                @"vehicle=""TK_INS_Soldier_EP1"";\n",
                                @"rank=""CORPORAL"";\n",
                                @"skill=0.33333331;\n",
                            @"};\n",
                        @"};\n",
                    @"};\n",
                @"};\n",
                @"class Vehicles\n",
                @"{\n",
                    @"items=3;\n",
                    @"class Item0\n",
                    @"{\n",
                        @"position[]={4144.3379,16.708448,4124.7598};\n",
                        @"id=21;\n",
                        @"side=""EMPTY"";\n",
                        @"vehicle=""MtvrSupply_DES_EP1"";\n",
                        @"skill=0.60000002;\n",
                        @"text=""SupplyTruck"";\n",
                    @"};\n",
                    @"class Item1\n",
                    @"{\n",
                        @"position[]={4140.9097,16.680185,4125.9814};\n",
                        @"id=22;\n",
                        @"side=""EMPTY"";\n",
                        @"vehicle=""USOrdnanceBox_EP1"";\n",
                        @"skill=0.60000002;\n",
                        @"text=""AmmoBox1"";\n",
                    @"};\n",
                    @"class Item2\n",
                    @"{\n",
                        @"position[]={4140.8755,16.696251,4124.375};\n",
                        @"id=23;\n",
                        @"side=""EMPTY"";\n",
                        @"vehicle=""USSpecialWeapons_EP1"";\n",
                        @"skill=0.60000002;\n",
                        @"text=""AmmoBox2"";\n",
                    @"};\n",
                @"};\n",
                @"class Markers\n",
                @"{\n",
                    @"items=5;\n",
                    @"class Item0\n",
                    @"{\n",
                        @"position[]={4146.9932,16.729437,4126.1416};\n",
                        @"name=""TargetAreaCenter"";\n",
                        @"markerType=""ELLIPSE"";\n",
                        @"type=""Empty"";\n",
                        @"fillName=""Border"";\n",
                        @"a=40;\n",
                        @"b=40;\n",
                        @"drawBorder=1;\n",
                    @"};\n",
                    @"class Item1\n",
                    @"{\n",
                        @"position[]={4146.1675,16.655014,4143.5054};\n",
                        @"name=""TargetAreaLarge"";\n",
                        @"markerType=""RECTANGLE"";\n",
                        @"type=""Empty"";\n",
                        @"fillName=""Border"";\n",
                        @"a=300;\n",
                        @"b=200;\n",
                        @"drawBorder=1;\n",
                    @"};\n",
                    @"class Item2\n",
                    @"{\n",
                        @"position[]={2803.5479,5.8619881,1565.0396};\n",
                        @"name=""Boot_Hill"";\n",
                        @"type=""mil_unknown"";\n",
                    @"};\n",
                    @"class Item3\n",
                    @"{\n",
                        @"position[]={4559.0542,18.885059,4012.731};\n",
                        @"name=""center"";\n",
                        @"type=""hd_unknown"";\n",
                    @"};\n",
                    @"class Item4\n",
                    @"{\n",
                        @"position[]={4142.1401,16.684631,4128.2583};\n",
                        @"name=""TargetLocation"";\n",
                        @"text=""Destroy equipment"";\n",
                        @"type=""mil_objective"";\n",
                    @"};\n",
                @"};\n",
                @"class Sensors\n",
                @"{\n",
                    @"items=1;\n",
                    @"class Item0\n",
                    @"{\n",
                        @"position[]={4147.3975,16.660114,4133.627};\n",
                        @"a=0;\n",
                        @"b=0;\n",
                        @"activationBy=""ANY"";\n",
                        @"interruptable=1;\n",
                        @"type=""SWITCH"";\n",
                        @"age=""UNKNOWN"";\n",
                        @"expCond=""!alive SupplyTruck && ((getDammage AmmoBox1) > 0.5) && ((getDammage AmmoBox2) > 0.5)"";\n",
                        @"expActiv=""myEnd = [1] execVM ""f\server\f_mpEndBroadcast.sqf"";"";\n",
                        @"class Effects\n",
                        @"{\n",
                        @"};\n",
                    @"};\n",
                @"};\n",
            @"};\n",
            @"class Intro\n",
            @"{\n",
                @"addOns[]=\n",
                @"{\n",
                    @"""zargabad""\n",
                @"};\n",
                @"addOnsAuto[]=\n",
                @"{\n",
                    @"""zargabad""\n",
                @"};\n",
                @"randomSeed=5875250;\n",
                @"class Intel\n",
                @"{\n",
                    @"startWeather=0.25;\n",
                    @"forecastWeather=0.25;\n",
                    @"year=2008;\n",
                    @"month=10;\n",
                    @"day=11;\n",
                    @"hour=9;\n",
                    @"minute=20;\n",
                @"};\n",
            @"};\n",
            @"class OutroWin\n",
            @"{\n",
                @"addOns[]=\n",
                @"{\n",
                    @"""zargabad""\n",
                @"};\n",
                @"addOnsAuto[]=\n",
                @"{\n",
                    @"""zargabad""\n",
                @"};\n",
                @"randomSeed=15434763;\n",
                @"class Intel\n",
                @"{\n",
                    @"startWeather=0.25;\n",
                    @"forecastWeather=0.25;\n",
                    @"year=2008;\n",
                    @"month=10;\n",
                    @"day=11;\n",
                    @"hour=9;\n",
                    @"minute=20;\n",
                @"};\n",
            @"};\n",
            @"class OutroLoose\n",
            @"{\n",
                @"addOns[]=\n",
                @"{\n",
                    @"""zargabad""\n",
                @"};\n",
                @"addOnsAuto[]=\n",
                @"{\n",
                    @"""zargabad""\n",
                @"};\n",
                    @"randomSeed=477795;\n",
                    @"class Intel\n",
                    @"{\n",
                    @"startWeather=0.25;\n",
                    @"forecastWeather=0.25;\n",
                    @"year=2008;\n",
                    @"month=10;\n",
                    @"day=11;\n",
                    @"hour=9;\n",
                    @"minute=20;\n",
                @"};\n",
            @"};\n"
        };

        [Test]
        public void Expect_SqmParser_to_successfully_parse_testFile()
        {
            var parser = new SqmParser();

            var parseResult = parser.Parse(testFileText);

            Assert.AreEqual(11, parseResult.Version);
        }
    }
}
