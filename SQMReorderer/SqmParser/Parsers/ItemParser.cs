using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class ItemParser
    {
        private readonly string decimalPattern = @"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?";
        private readonly NumberFormatInfo _doubleFormatInfo;

        private readonly Regex _itemNumberRegex = new Regex(@"class Item(?<number>\d+)", RegexOptions.Compiled);

        private readonly Regex _positionRegex;
        private readonly Regex _azimutRegex;
        private readonly Regex _idRegex;
        private readonly Regex _sideRegex;
        private readonly Regex _vehicleRegex;
        private readonly Regex _playerRegex;
        private readonly Regex _leaderRegex;
        private readonly Regex _rankRegex;
        private readonly Regex _skillRegex;
        private readonly Regex _textRegex;
        private readonly Regex _initRegex;
        private readonly Regex _descriptionRegex;
        private readonly Regex _synchronizationHeaderRegex;
        private readonly Regex _synchronizationItemRegex;

        private Item _currentItem;

        public ItemParser()
        {
            _doubleFormatInfo = new NumberFormatInfo();
            _doubleFormatInfo.CurrencyDecimalSeparator = ".";

            _azimutRegex = new Regex(@"azimut\=(?<azimut>" + decimalPattern + @")");
            _idRegex = new Regex(@"id\=(?<id>\d+)", RegexOptions.Compiled);
            _sideRegex = new Regex(@"side\=""(?<side>\w+)""", RegexOptions.Compiled);
            _vehicleRegex = new Regex(@"vehicle\=""(?<vehicle>\w+)""", RegexOptions.Compiled);
            _playerRegex = new Regex(@"player\=""(?<player>[\w\s]+)""", RegexOptions.Compiled);
            _leaderRegex = new Regex(@"leader\=(?<leader>\d)", RegexOptions.Compiled);
            _rankRegex = new Regex(@"rank\=""(?<rank>\w+)""", RegexOptions.Compiled);
            _skillRegex = new Regex(@"skill\=(?<skill>" + decimalPattern + @")", RegexOptions.Compiled);
            _initRegex = new Regex(@"init\=""(?<init>.+)""", RegexOptions.Compiled);
            _textRegex = new Regex(@"text\=""(?<text>\w+)""", RegexOptions.Compiled);
            _descriptionRegex = new Regex(@"description\=""(?<description>.+)""", RegexOptions.Compiled);
            _synchronizationHeaderRegex = new Regex(@"synchronizations\[\]\=", RegexOptions.Compiled);
            _synchronizationItemRegex = new Regex(@"(?<synchronization>\d+)", RegexOptions.Compiled);

            var xPosPattern = string.Format(@"(?<xpos>{0})", decimalPattern);
            var yPosPattern = string.Format(@"(?<ypos>{0})", decimalPattern);
            var zPosPattern = string.Format(@"(?<zpos>{0})", decimalPattern);
            var positionPattern = @"position\[\]\=\{" + xPosPattern + @"\," + yPosPattern + @"\," + zPosPattern + @"}";
            _positionRegex = new Regex(positionPattern, RegexOptions.Compiled);
        }

        public bool IsItemElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_itemNumberRegex);
        }

        public Item ParseItemElement(SqmStream stream)
        {
            _currentItem = new Item();

            var vehiclesParser = new ItemListParser();

            stream.MatchHeader(_itemNumberRegex, SetItemNumber);

            while(!stream.IsAtEndOfContext)
            {
                if(vehiclesParser.IsListElement("Vehicles", stream))
                {
                    stream.StepIntoInnerContext();
                    var items = vehiclesParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                    _currentItem.Items = items;

                    continue;
                }

                if (stream.IsCurrentLineMatch(_positionRegex))
                {
                    stream.MatchCurrentLine(_positionRegex, SetPosition);
                }
                else if (stream.IsCurrentLineMatch(_azimutRegex))
                {
                    stream.MatchCurrentLine(_azimutRegex, SetAzimut);
                }
                else if (stream.IsCurrentLineMatch(_idRegex))
                {
                    stream.MatchCurrentLine(_idRegex, SetId);
                }
                else if (stream.IsCurrentLineMatch(_sideRegex))
                {
                    stream.MatchCurrentLine(_sideRegex, SetSide);
                }
                else if(stream.IsCurrentLineMatch(_vehicleRegex))
                {
                    stream.MatchCurrentLine(_vehicleRegex, SetVehicle);
                }
                else if (stream.IsCurrentLineMatch(_playerRegex))
                {
                    stream.MatchCurrentLine(_playerRegex, SetPlayer);
                }
                else if (stream.IsCurrentLineMatch(_leaderRegex))
                {
                    stream.MatchCurrentLine(_leaderRegex, SetLeader);
                }
                else if (stream.IsCurrentLineMatch(_rankRegex))
                {
                    stream.MatchCurrentLine(_rankRegex, SetRank);
                }
                else if (stream.IsCurrentLineMatch(_skillRegex))
                {
                    stream.MatchCurrentLine(_skillRegex, SetSkill);
                }
                else if (stream.IsCurrentLineMatch(_textRegex))
                {
                    stream.MatchCurrentLine(_textRegex, SetText);
                }
                else if (stream.IsCurrentLineMatch(_initRegex))
                {
                    stream.MatchCurrentLine(_initRegex, SetInit);
                }
                else if (stream.IsCurrentLineMatch(_descriptionRegex))
                {
                    stream.MatchCurrentLine(_descriptionRegex, SetDescription);
                }
                else if (stream.IsCurrentLineMatch(_synchronizationHeaderRegex))
                {
                    stream.MatchCurrentLine(_synchronizationItemRegex, SetSynchronizations);
                }
                else
                {
                    throw new SqmParseException("Unknown property: " + stream.CurrentLine);
                }

                stream.NextLineInContext();
            }

            return _currentItem;
        }

        private void SetPosition(Match match)
        {
            var xPosGroup = match.Groups["xpos"];
            var yPosGroup = match.Groups["ypos"];
            var zPosGroup = match.Groups["zpos"];

            double xPos = double.Parse(xPosGroup.Value, _doubleFormatInfo);
            double yPos = double.Parse(yPosGroup.Value, _doubleFormatInfo);
            double zPos = double.Parse(zPosGroup.Value, _doubleFormatInfo);

            _currentItem.Position = new Vector(xPos, yPos, zPos);
        }

        private void SetAzimut(Match match)
        {
            var numberGroup = match.Groups["azimut"];
            _currentItem.Azimut = double.Parse(numberGroup.Value, _doubleFormatInfo);
        }

        private void SetItemNumber(Match match)
        {
            var numberGroup = match.Groups["number"];
            _currentItem.Number = Convert.ToInt32(numberGroup.Value);
        }

        private void SetId(Match match)
        {
            var idGroup = match.Groups["id"];
            _currentItem.Id = Convert.ToInt32(idGroup.Value);
        }

        private void SetSide(Match match)
        {
            var sideGroup = match.Groups["side"];
            _currentItem.Side = sideGroup.Value;
        }

        private void SetVehicle(Match match)
        {
            var vehicleGroup = match.Groups["vehicle"];
            _currentItem.Vehicle = vehicleGroup.Value;
        }

        private void SetPlayer(Match match)
        {
            var playerGroup = match.Groups["player"];
            _currentItem.Player = playerGroup.Value;
        }

        private void SetLeader(Match match)
        {
            var leaderGroup = match.Groups["leader"];
            _currentItem.Leader = Convert.ToInt32(leaderGroup.Value);
        }

        private void SetRank(Match match)
        {
            var rankGroup = match.Groups["rank"];
            _currentItem.Rank = rankGroup.Value;
        }

        private void SetSkill(Match match)
        {
            var skillGroup = match.Groups["skill"];
            _currentItem.Skill = double.Parse(skillGroup.Value, _doubleFormatInfo);
        }

        private void SetText(Match match)
        {
            var textGroup = match.Groups["text"];
            _currentItem.Text = textGroup.Value;
        }

        private void SetInit(Match match)
        {
            var initGroup = match.Groups["init"];
            _currentItem.Init = initGroup.Value;
        }

        private void SetDescription(Match match)
        {
            var descriptionGroup = match.Groups["description"];
            _currentItem.Description = descriptionGroup.Value;
        }

        private void SetSynchronizations(Match match)
        {
            while(match.Success)
            {
                var synchronizationNumber = Convert.ToInt32(match.Value);
                _currentItem.Synchronizations.Add(synchronizationNumber);

                match = match.NextMatch();
            }
        }
    }
}
