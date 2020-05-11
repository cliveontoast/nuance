using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuanceWebApp.Dto
{
    public class MatchesDto
    {
        public MatchesDto(MatchResult model)
        {
            CharacterPositions = model.CharacterPositions;
            ErrorType = (MatchesErrorType?)model.Error;
        }
        public IEnumerable<int> CharacterPositions { get; }
        public MatchesErrorType? ErrorType { get; }
    }

    public enum MatchesErrorType
    {
        SubtextNullOrEmpty = 1,
    }
}
